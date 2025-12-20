using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class audio_controller : MonoBehaviour, IPointerClickHandler
{
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
        // Basic defaults / safety
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            if (audioClip != null) audioSource.clip = audioClip;
        }

        if (progressSlider != null)
        {
            progressSlider.minValue = 0f;
            progressSlider.maxValue = 1f;
            progressSlider.wholeNumbers = false;
            progressSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        SetPlayingUI(false);
    }

    private void OnDestroy()
    {
        if (progressSlider != null)
            progressSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TogglePlayPause();
    }

    private void Update()
    {
        if (audioSource == null || audioSource.clip == null || progressSlider == null) return;

        // If ended, reset UI
        if (!audioSource.isPlaying && !isScrubbing && audioSource.time > 0f)
        {
            // Consider "finished" when very close to end
            if (audioSource.time >= audioSource.clip.length - 0.02f)
            {
                audioSource.Stop();
                audioSource.time = 0f;
                progressSlider.value = 0f;
                SetPlayingUI(false);
                return;
            }
        }

        // Update slider while playing (unless user is scrubbing)
        if (audioSource.isPlaying && !isScrubbing)
        {
            float t = audioSource.time / audioSource.clip.length;
            progressSlider.value = Mathf.Clamp01(t);
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
        if (!isScrubbing) return;
        if (audioSource == null || audioSource.clip == null) return;

        float newTime = value01 * audioSource.clip.length;
        audioSource.time = Mathf.Clamp(newTime, 0f, audioSource.clip.length);
    }

    // Hook these up via EventTrigger on the Slider (Pointer Down / Pointer Up)
    public void BeginScrub()
    {
        isScrubbing = true;
    }

    public void EndScrub()
    {
        isScrubbing = false;

        // Optional: if you want scrubbing while paused to stay paused, do nothing.
        // If you want scrubbing to resume playback automatically when it was playing, add logic here.
    }

    private void Start()
    {
        ForceUIState();
    }

    private void ForceUIState()
    {
        // Always start hidden unless currently playing
        bool playing = audioSource != null && audioSource.isPlaying;
        SetPlayingUI(playing);

        if (progressSlider != null && audioSource != null && audioSource.clip != null)
        {
            float t = audioSource.clip.length > 0 ? audioSource.time / audioSource.clip.length : 0f;
            progressSlider.value = Mathf.Clamp01(t);
        }
    }

}
