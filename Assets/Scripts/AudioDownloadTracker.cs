using System.IO;
using TMPro;
using UnityEngine;

public class AudioDownloadTracker : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private AudioManager audioManager;

    [Tooltip("How often (in seconds) to recount the files on disk.")]
    [SerializeField] private float checkInterval = 1f;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateCount), 0f, checkInterval);
    }

    private void UpdateCount()
    {
        if (statusText == null || audioManager == null) return;

        string localRoot = Path.Combine(Application.persistentDataPath, audioManager.localRootFolderName);

        int count = Directory.Exists(localRoot)
            ? Directory.GetFiles(localRoot, "*.mp3", SearchOption.AllDirectories).Length
            : 0;

        int total = audioManager.audios != null ? audioManager.audios.Count : 0;

        statusText.text = $"{count}/{total}";
        statusText.color = (count >= total && total > 0) ? Color.green : Color.red;
    }
}
