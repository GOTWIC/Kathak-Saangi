using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class audio_controller : MonoBehaviour
{
    [Header("UI")]
    [Tooltip("Child to receive play/pause clicks (e.g. audio_element). EventTrigger is added at runtime.")]
    [SerializeField] private GameObject clickTargetChild;
    [SerializeField] private Image playPauseImage;
    [SerializeField] private Sprite playSprite;
    [SerializeField] private Sprite pauseSprite;

    [Tooltip("The GameObject that contains the progress bar UI (enable/disable this).")]
    [SerializeField] private GameObject progressBarRoot;

    [Tooltip("Slider used as the progress bar (min 0, max 1).")]
    [SerializeField] private Slider progressSlider;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    [Header("Fallback Load")]
    [SerializeField] private string trackName;
    [SerializeField] private string trackFolderName = "Assets/Audio/tracks_app";

    private AudioManager _audioManager;
    private bool _clipLoadRequested;
    private float _retryAfterTime;

    private bool isScrubbing;

    private static readonly System.Collections.Generic.List<audio_controller> s_allInstances = new System.Collections.Generic.List<audio_controller>();

    private void OnEnable()
    {
        if (!s_allInstances.Contains(this))
            s_allInstances.Add(this);
    }

    private void OnDisable()
    {
        s_allInstances.Remove(this);
    }

    /// <summary>Stops playback and updates UI to paused state. Used when another track starts so only one plays at a time.</summary>
    public void StopPlayback()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            SetPlayingUI(false);
        }
    }

    public void SetTrack(string fileName, string folderName, AudioManager manager)
    {
        trackName = fileName;
        trackFolderName = string.IsNullOrWhiteSpace(folderName) ? "Assets/Audio/tracks_app" : folderName.Trim();
        _audioManager = manager;
        _clipLoadRequested = false;
        _retryAfterTime = 0f;
    }

    private void Awake()
    {
        // Audio defaults
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.ignoreListenerPause = true;
            if (audioClip != null) audioSource.clip = audioClip;
        }

        // Slider defaults
        if (progressSlider != null)
        {
            progressSlider.minValue = 0f;
            progressSlider.maxValue = 1f;
            progressSlider.wholeNumbers = false;
            progressSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        // Wire pointer click on the click-target child only (e.g. audio_element)
        if (clickTargetChild != null)
        {
            var trigger = clickTargetChild.GetComponent<EventTrigger>() ?? clickTargetChild.AddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
            entry.callback.AddListener(_ => TogglePlayPause());
            trigger.triggers.Add(entry);
        }

        SetPlayingUI(false);
    }

    private void Start()
    {
        ForceUIState();
    }

    private void OnDestroy()
    {
        if (progressSlider != null)
            progressSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    private void Update()
    {
        // Fallback: if the clip was never assigned, use trackName to load it.
        // Retries every 5 seconds in case the file wasn't on disk yet (still downloading).
        if (audioClip == null && !_clipLoadRequested
            && !string.IsNullOrWhiteSpace(trackName)
            && _audioManager != null
            && Time.time >= _retryAfterTime)
        {
            _clipLoadRequested = true;
            _audioManager.LoadClip(trackName, trackFolderName, clip =>
            {
                if (clip != null)
                {
                    audioClip = clip;
                }
                else
                {
                    // File not available yet; allow another attempt after a delay.
                    _clipLoadRequested = false;
                    _retryAfterTime = Time.time + 5f;
                }
            });
        }

        if (audioSource == null || audioSource.clip == null || progressSlider == null) return;

        // Finished -> reset UI
        if (!audioSource.isPlaying && !isScrubbing && audioSource.time > 0f)
        {
            if (audioSource.time >= audioSource.clip.length - 0.02f)
            {
                audioSource.Stop();
                audioSource.time = 0f;
                progressSlider.SetValueWithoutNotify(0f);
                SetPlayingUI(false);
                return;
            }
        }

        // Update slider while playing (unless user is scrubbing)
        if (audioSource.isPlaying && !isScrubbing)
        {
            float t = audioSource.time / audioSource.clip.length;
            progressSlider.SetValueWithoutNotify(Mathf.Clamp01(t));
        }
    }

    private void TogglePlayPause()
    {
        if (audioSource == null) return;

        // Ensure clip set
        if (audioClip != null && audioSource.clip != audioClip)
            audioSource.clip = audioClip;

        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            SetPlayingUI(false);
        }
        else
        {
            if (audioSource.clip == null) return;
            // Stop all other audio controllers so only this one plays
            for (int i = s_allInstances.Count - 1; i >= 0; i--)
            {
                var other = s_allInstances[i];
                if (other != null && other != this)
                    other.StopPlayback();
            }
            audioSource.Play();
            SetPlayingUI(true);
        }
    }

    private void SetPlayingUI(bool playing)
    {
        if (playPauseImage != null)
            playPauseImage.sprite = playing ? pauseSprite : playSprite;

        if (progressBarRoot != null)
            progressBarRoot.SetActive(playing);
    }

    private void OnSliderValueChanged(float value01)
    {
        if (audioSource == null || audioSource.clip == null) return;

        // Seek: programmatic updates use SetValueWithoutNotify, so value changes here are from user interaction (drag/touch).
        float newTime = Mathf.Clamp01(value01) * audioSource.clip.length;
        audioSource.time = Mathf.Clamp(newTime, 0f, audioSource.clip.length);
    }

    // Optional: use these with an EventTrigger on the Slider (Pointer Down / Pointer Up)
    public void BeginScrub() => isScrubbing = true;
    public void EndScrub() => isScrubbing = false;

    private void ForceUIState()
    {
        bool playing = audioSource != null && audioSource.isPlaying;
        SetPlayingUI(playing);

        if (progressSlider != null && audioSource != null && audioSource.clip != null)
        {
            float t = audioSource.clip.length > 0 ? audioSource.time / audioSource.clip.length : 0f;
            progressSlider.SetValueWithoutNotify(Mathf.Clamp01(t));
        }
    }
}
