using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("UI")]
    [Tooltip("Previously used for download progress. Can be removed from the scene.")]
    public GameObject download_dialogue;

    [Tooltip("Previously used for download progress. Can be removed from the scene.")]
    public ProgressUI progressUI;

    [Serializable]
    public class ProgressUI
    {
        [Tooltip("Optional Slider used as a progress bar. Set Min=0, Max=1.")]
        public Slider progressBar;

        [Tooltip("Optional TMP text for status.")]
        public TMP_Text statusText;

        public void SetProgress(float normalized01, string status)
        {
            if (progressBar != null)
                progressBar.value = Mathf.Clamp01(normalized01);

            if (statusText != null)
                statusText.text = status ?? "";
        }
    }
}
