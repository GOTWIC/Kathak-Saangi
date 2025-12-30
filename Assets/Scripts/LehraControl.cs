using UnityEngine;
using UnityEngine.UI;

public class LehraControl : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button statusButton;     // the clickable button (add Button component)
    [SerializeField] private Image statusImage;       // the Image whose sprite will be swapped
    [SerializeField] private Sprite playSprite;       // shown when NOT playing
    [SerializeField] private Sprite pauseSprite;      // shown when playing

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource; // put AudioSource on slider_container
    [SerializeField] private AudioClip clip;          // optional for now

    private bool _isPlaying;

    private void Awake()
    {
        // Auto-wire if you forgot
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        if (statusButton != null)
            statusButton.onClick.AddListener(TogglePlayPause);

        // Default state: not playing (show play)
        SetPlaying(false);
    }

    private void OnDestroy()
    {
        if (statusButton != null)
            statusButton.onClick.RemoveListener(TogglePlayPause);
    }

    private void TogglePlayPause()
    {
        SetPlaying(!_isPlaying);
    }

    private void SetPlaying(bool play)
    {
        _isPlaying = play;

        // Swap sprite
        if (statusImage != null)
            statusImage.sprite = _isPlaying ? pauseSprite : playSprite;

        // Start/stop audio
        if (audioSource == null) return;

        if (clip != null) audioSource.clip = clip;

        if (_isPlaying)
        {
            if (audioSource.clip != null) audioSource.Play();
            else Debug.LogWarning("[LehraControl] No AudioClip assigned yet.");
        }
        else
        {
            audioSource.Stop();
        }
    }
}
