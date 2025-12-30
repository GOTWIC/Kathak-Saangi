using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class LehraControls : MonoBehaviour
{
    [Serializable]
    public class Container
    {
        [Header("UI")]
        public Button statusButton;
        public Image statusImage;
        public Sprite playSprite;   // shown when NOT playing
        public Sprite pauseSprite;  // shown when playing
        public Slider tempoSlider;

        [Header("BPM Settings")]
        public int min_bpm = 60;
        public int max_bpm = 180;
        public int base_bpm = 120;

        [Header("Read Only")]
        [ReadOnly] public int selected_bpm;
        [ReadOnly] public float speed_multiplier;

        [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip clip;

        [Header("Pitch Compensation (Pitch Shifter Pitch MULTIPLIER 0.5..2.0)")]
        public AudioMixer audioMixer;
        public string pitchShiftParam; // e.g. "LehraA_PitchComp"
    }

    [Header("Containers")]
    [SerializeField] private List<Container> containers = new List<Container>(3);

    [Header("Overall BPM UI")]
    [SerializeField] private TMP_Text overallBpmText;
    [SerializeField] private int defaultOverallBpm = 50;

    private int _currentlyPlayingIndex = -1; // -1 = none playing
    private int _lastOverallBpm;

    private void Awake()
    {
        _lastOverallBpm = defaultOverallBpm;
        UpdateOverallBpmText(_lastOverallBpm);

        for (int i = 0; i < containers.Count; i++)
        {
            int idx = i;
            var c = containers[idx];

            // Configure slider range + start at MIN BPM
            if (c.tempoSlider != null)
            {
                int lo = Mathf.Min(c.min_bpm, c.max_bpm);
                int hi = Mathf.Max(c.min_bpm, c.max_bpm);

                c.tempoSlider.minValue = lo;
                c.tempoSlider.maxValue = hi;
                c.tempoSlider.wholeNumbers = true;

                c.tempoSlider.value = lo; // <-- start at minimum

                c.tempoSlider.onValueChanged.AddListener(_ => OnSliderChanged(idx));
            }
            else
            {
                Debug.LogWarning($"[LehraControls] Container {idx}: tempoSlider not assigned.");
            }

            // Wire button
            if (c.statusButton != null)
                c.statusButton.onClick.AddListener(() => OnStatusButtonClicked(idx));
            else
                Debug.LogWarning($"[LehraControls] Container {idx}: statusButton not assigned.");

            // Default: not playing
            SetContainerPlaying(idx, playing: false, stopAudio: true);

            // Init internal bpm/speed state
            SyncBpmFromSlider(idx);
            ApplySpeedWithConstantPitch(idx);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < containers.Count; i++)
        {
            var c = containers[i];
            if (c.statusButton != null) c.statusButton.onClick.RemoveAllListeners();
            if (c.tempoSlider != null) c.tempoSlider.onValueChanged.RemoveAllListeners();
        }
    }

    private void OnStatusButtonClicked(int idx)
    {
        // Clicking the currently playing one => stop it (overall BPM stays as-is)
        if (_currentlyPlayingIndex == idx)
        {
            SetContainerPlaying(idx, playing: false, stopAudio: true);
            _currentlyPlayingIndex = -1;
            return;
        }

        // Stop whoever was playing
        if (_currentlyPlayingIndex >= 0)
            SetContainerPlaying(_currentlyPlayingIndex, playing: false, stopAudio: true);

        // Start this one
        SetContainerPlaying(idx, playing: true, stopAudio: false);
        _currentlyPlayingIndex = idx;

        // When a container becomes active, lock overall BPM to it immediately
        SyncBpmFromSlider(idx);
        _lastOverallBpm = containers[idx].selected_bpm;
        UpdateOverallBpmText(_lastOverallBpm);
    }

    private void OnSliderChanged(int idx)
    {
        // Always update that container's internal bpm/speed (even if not playing)
        SyncBpmFromSlider(idx);
        ApplySpeedWithConstantPitch(idx);

        // Overall BPM update rules:
        // - If none playing: any slider can change it
        // - If one is playing: only that container's slider can change it
        if (_currentlyPlayingIndex == -1 || _currentlyPlayingIndex == idx)
        {
            _lastOverallBpm = containers[idx].selected_bpm;
            UpdateOverallBpmText(_lastOverallBpm);
        }
    }

    private void UpdateOverallBpmText(int bpm)
    {
        if (overallBpmText != null)
            overallBpmText.text = $"{bpm} BPM";
    }

    private void SetContainerPlaying(int idx, bool playing, bool stopAudio)
    {
        if (idx < 0 || idx >= containers.Count) return;
        var c = containers[idx];

        // Icon swap
        if (c.statusImage != null)
            c.statusImage.sprite = playing ? c.pauseSprite : c.playSprite;

        if (c.audioSource == null) return;

        if (c.clip != null)
            c.audioSource.clip = c.clip;

        // Apply speed & compensation before play
        SyncBpmFromSlider(idx);
        ApplySpeedWithConstantPitch(idx);

        if (playing)
        {
            if (c.audioSource.clip != null) c.audioSource.Play();
            else Debug.LogWarning($"[LehraControls] Container {idx}: No AudioClip assigned.");
        }
        else if (stopAudio)
        {
            c.audioSource.Stop();
        }
    }

    private void SyncBpmFromSlider(int idx)
    {
        var c = containers[idx];

        if (c.tempoSlider == null)
        {
            c.selected_bpm = Mathf.Max(1, c.base_bpm);
            return;
        }

        c.selected_bpm = Mathf.RoundToInt(c.tempoSlider.value);
    }

    private void ApplySpeedWithConstantPitch(int idx)
    {
        var c = containers[idx];

        int denom = Mathf.Max(1, c.base_bpm);
        c.speed_multiplier = c.selected_bpm / (float)denom;

        // Must stay within what Unity's pitch shifter can compensate (0.5..2.0)
        float speed = Mathf.Clamp(c.speed_multiplier, 0.5f, 2.0f);
        c.speed_multiplier = speed;

        if (c.audioSource != null)
            c.audioSource.pitch = speed;

        // Compensation uses Pitch Shifter "Pitch" multiplier (0.5..2.0)
        if (c.audioMixer != null && !string.IsNullOrWhiteSpace(c.pitchShiftParam))
        {
            float compensation = 1f / Mathf.Max(0.0001f, speed);
            compensation = Mathf.Clamp(compensation, 0.5f, 2.0f);
            c.audioMixer.SetFloat(c.pitchShiftParam, compensation);
        }
    }
}

// ReadOnly attribute + drawer (single file)
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
