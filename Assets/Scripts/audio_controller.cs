using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class audio_controller : MonoBehaviour
{
    [Header("Click Area (audio_element)")]
    [SerializeField] private Button audioElementButton;   // Put this Button on audio_element

    [Header("UI")]
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

    private bool isScrubbing;

    private void Awake()
    {
        // Hook click ONLY on audio_element's Button
        if (audioElementButton != null)
            audioElementButton.onClick.AddListener(TogglePlayPause);

        // Audio defaults
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
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

        SetPlayingUI(false);
    }

    private void Start()
    {
        ForceUIState();
    }

    private void OnDestroy()
    {
        if (audioElementButton != null)
            audioElementButton.onClick.RemoveListener(TogglePlayPause);

        if (progressSlider != null)
            progressSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    private void Update()
    {
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

        // Seek only when the user is interacting (drag/touch),
        // or when BeginScrub/EndScrub is used.
        bool treatAsUserScrub = isScrubbing || Input.GetMouseButton(0) || Input.touchCount > 0;
        if (!treatAsUserScrub) return;

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
