using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class LehraControl : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button statusButton;
    [SerializeField] private Image statusImage;
    [SerializeField] private Sprite playSprite;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Slider tempoSlider;

    [Header("BPM Settings")]
    [SerializeField] private int min_bpm = 60;
    [SerializeField] private int max_bpm = 180;
    [SerializeField] private int base_bpm = 120;

    [Header("Read Only")]
    [ReadOnly, SerializeField] private int selected_bpm;
    [ReadOnly, SerializeField] private float speed_multiplier;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    [Header("Pitch-constant time stretch (AudioMixer Pitch Shifter)")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string pitchShiftSemitonesParam = "LehraPitchShiftSemitones";

    private bool _isPlaying;

    private void Awake()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        if (tempoSlider == null) tempoSlider = GetComponentInChildren<Slider>(true);

        if (statusButton != null)
            statusButton.onClick.AddListener(TogglePlayPause);

        if (tempoSlider != null)
        {
            int lo = Mathf.Min(min_bpm, max_bpm);
            int hi = Mathf.Max(min_bpm, max_bpm);

            tempoSlider.minValue = lo;
            tempoSlider.maxValue = hi;
            tempoSlider.wholeNumbers = true;

            tempoSlider.value = Mathf.Clamp(base_bpm, lo, hi);
            tempoSlider.onValueChanged.AddListener(OnSliderChanged);
        }

        SetPlaying(false);

        SyncBpmFromSlider();
        ApplySpeedWithConstantPitch();
    }

    private void OnDestroy()
    {
        if (statusButton != null)
            statusButton.onClick.RemoveListener(TogglePlayPause);

        if (tempoSlider != null)
            tempoSlider.onValueChanged.RemoveListener(OnSliderChanged);
    }

    private void TogglePlayPause() => SetPlaying(!_isPlaying);

    private void SetPlaying(bool play)
    {
        _isPlaying = play;

        if (statusImage != null)
            statusImage.sprite = _isPlaying ? pauseSprite : playSprite;

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

    private void OnSliderChanged(float _)
    {
        SyncBpmFromSlider();
        ApplySpeedWithConstantPitch();
    }

    private void SyncBpmFromSlider()
    {
        if (tempoSlider == null)
        {
            selected_bpm = Mathf.Max(1, base_bpm);
            return;
        }

        selected_bpm = Mathf.RoundToInt(tempoSlider.value);
    }

    private void ApplySpeedWithConstantPitch()
    {
        if (audioSource == null) return;

        int denom = Mathf.Max(1, base_bpm);
        speed_multiplier = selected_bpm / (float)denom;

        // AudioSource.pitch is both speed + pitch. Clamp to what you can reasonably correct.
        speed_multiplier = Mathf.Clamp(speed_multiplier, 0.5f, 2.0f);
        audioSource.pitch = speed_multiplier;

        // Pitch Shifter "Pitch" is a multiplier too (0.5x..2.0x). Use inverse to cancel pitch change.
        float pitchCompensation = 1f / Mathf.Max(0.0001f, speed_multiplier);
        pitchCompensation = Mathf.Clamp(pitchCompensation, 0.5f, 2.0f);

        if (audioMixer != null && !string.IsNullOrWhiteSpace(pitchShiftSemitonesParam))
        {
            audioMixer.SetFloat(pitchShiftSemitonesParam, pitchCompensation);
        }
    }

    private static float Log2(float x)
    {
        x = Mathf.Max(0.0001f, x);
        return Mathf.Log(x) / Mathf.Log(2f);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (tempoSlider != null)
        {
            int lo = Mathf.Min(min_bpm, max_bpm);
            int hi = Mathf.Max(min_bpm, max_bpm);
            tempoSlider.minValue = lo;
            tempoSlider.maxValue = hi;
            tempoSlider.wholeNumbers = true;
        }

        SyncBpmFromSlider();
        ApplySpeedWithConstantPitch();
    }
#endif
}

// ReadOnly attribute (single-file)
public class ReadOnlyAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[UnityEditor.CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : UnityEditor.PropertyDrawer
{
    public override float GetPropertyHeight(UnityEditor.SerializedProperty property, GUIContent label)
        => UnityEditor.EditorGUI.GetPropertyHeight(property, label, true);

    public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
    {
        bool old = GUI.enabled;
        GUI.enabled = false;
        UnityEditor.EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = old;
    }
}
#endif
