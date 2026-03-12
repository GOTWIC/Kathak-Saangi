using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Download UI")]
    [Tooltip("Shown when something is missing. Hidden after all downloads finish.")]
    public GameObject download_dialogue;

    [Tooltip("Serializable progress UI (optional).")]
    public ProgressUI progressUI;

    [Header("Remote Source")]
    private string baseUrl = "https://pub-6ac8e01331fb4368a4d78b0912387db8.r2.dev/tracks_cloudfare/";
    private string fallbackBaseUrl = "https://raw.githubusercontent.com/GOTWIC/Kathak-Saangi/main/Assets/Audio/tracks/";

    [Header("Audio List")]
    public List<AudioEntry> audios = new List<AudioEntry>
    {
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "kramalaya_3" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "kramalaya_5" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "kramalaya_6" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "kramalaya_7" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "kramalaya_1_8_T" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "kramalaya_1_8_T_tabla" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "kramalaya_1_4_8_T_tabla" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "kramalaya_1_2_4_T_tabla" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "lu_upaj_1" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "lu_upaj_2" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "lu_lari_comp1_rec" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "lu_lari_comp2_rec" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "lu_lari_comp1_tabla" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "lu_lari_comp2_tabla" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "octapad_comp1" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "octapad_comp2" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "octapad_comp3" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "parhant_01" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "parhant_02" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "parhant_03" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "parhant_04" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "parhant_05" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "parhant_06" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "parhant_07" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "parhant_08" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "parhant_09" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "parhant_10" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_chakkar_1_beat_70_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_chakkar_1_beat_80_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_chakkar_1_beat_90_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_chakkar_2_beat_70_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_chakkar_2_beat_80_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_chakkar_3_beat" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_chakkar_5_beat" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_hastak_2_beat" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_hastak_3_beat" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_hastak_4_beat" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_taatkar_100_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_taatkar_150_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_taatkar_200_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_taatkar_220_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_taatkar_260_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_taatkar_300_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_taatkar_350_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_taatkar_400_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_taatkar400bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_taatkar_440_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_taatkar_520_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_takita_takita_dhin_90_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_takita_takita_dhin_100_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_takita_takita_dhin_110_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_takita_takita_dhin_130_bpm" },
        new AudioEntry { folderName = "Assets/Audio/tracks_app", fileName = "riyaz_takita_takita_dhin_175_bpm" },
    };

    [Header("Local Storage")]
    [Tooltip("Subfolder inside Application.persistentDataPath, e.g. AudioCache")]
    public string localRootFolderName = "AudioCache";

    [Tooltip("If a file exists but is smaller than this, treat it as corrupted and re-download.")]
    public long minValidBytes = 1024; // 1 KB safety check (tweak or set to 0)

    [Tooltip("Optional: seconds before a request times out (0 = Unity default).")]
    public int timeoutSeconds = 30;

    private bool _isDownloading;

    private readonly List<AudioEntry> _laterList = new List<AudioEntry>();
    private Coroutine _backgroundRetryCoroutine;

    /// <summary>Cache of loaded clips by key "folderName|fileName".</summary>
    private readonly Dictionary<string, AudioClip> _clipCache = new Dictionary<string, AudioClip>();

    // ---------------------------
    // Public API (as requested)
    // ---------------------------

    public void download_audios()
    {
        if (_isDownloading) return;
        StartCoroutine(DownloadAudiosCoroutine());
    }

    public void check_audio_integrity()
    {
        bool allPresent = AreAllAudiosPresent();

        if (!allPresent)
        {
            if (download_dialogue != null)
                download_dialogue.SetActive(true);

            download_audios();
        }
        // If all present, do nothing.
    }

    /// <summary>Returns a cached AudioClip for the given track, or null if not yet loaded.</summary>
    public AudioClip GetClip(string fileName, string folderName = null)
    {
        string key = MakeClipCacheKey(folderName, fileName);
        return _clipCache.TryGetValue(key, out var clip) ? clip : null;
    }

    /// <summary>Loads the track from disk (or returns cached). Calls onLoaded with the clip when ready.</summary>
    public void LoadClip(string fileName, string folderName, Action<AudioClip> onLoaded)
    {
        if (onLoaded == null) return;

        AudioClip cached = GetClip(fileName, folderName);
        if (cached != null)
        {
            onLoaded(cached);
            return;
        }

        StartCoroutine(LoadClipCoroutine(fileName, folderName ?? "", onLoaded));
    }

    private static string MakeClipCacheKey(string folderName, string fileName)
    {
        return (folderName ?? "").Trim() + "|" + (fileName ?? "").Trim();
    }

    private IEnumerator LoadClipCoroutine(string fileName, string folderName, Action<AudioClip> onLoaded)
    {
        string localRoot = Path.Combine(Application.persistentDataPath, localRootFolderName);
        string localPath = BuildLocalPath(localRoot, folderName, fileName);

        if (!File.Exists(localPath))
        {
            Debug.LogWarning($"[AudioManager] GetClip: file not found: {localPath}");
            onLoaded(null);
            yield break;
        }

        // file:/// for absolute paths: Windows C:\... -> file:///C:/... ; Unix /path -> file:///path
        string uri = localPath.StartsWith("/") ? "file://" + localPath : "file:///" + localPath.Replace('\\', '/');
        using (var req = UnityWebRequestMultimedia.GetAudioClip(uri, AudioType.MPEG))
        {
            // Streaming: decode on the fly from disk instead of loading entire clip into memory.
            // Similar to Unity's "Streaming" load type — faster to start, lower memory.
            var dh = req.downloadHandler as DownloadHandlerAudioClip;
            if (dh != null)
                dh.streamAudio = true;

            yield return req.SendWebRequest();

#if UNITY_2020_2_OR_NEWER
            if (req.result != UnityWebRequest.Result.Success)
#else
            if (req.isNetworkError || req.isHttpError)
#endif
            {
                Debug.LogError($"[AudioManager] Failed to load clip: {uri}\n{req.error}");
                onLoaded(null);
                yield break;
            }

            AudioClip clip = DownloadHandlerAudioClip.GetContent(req);
            if (clip != null)
            {
                string key = MakeClipCacheKey(folderName, fileName);
                _clipCache[key] = clip;
            }
            onLoaded(clip);
        }
    }

    // ---------------------------
    // Core implementation
    // ---------------------------

    private IEnumerator DownloadAudiosCoroutine()
    {
        _isDownloading = true;
        _laterList.Clear();

        string localRoot = Path.Combine(Application.persistentDataPath, localRootFolderName);
        Directory.CreateDirectory(localRoot);

        int total = audios != null ? audios.Count : 0;
        int completed = 0;

        progressUI?.SetProgress(0f, total == 0 ? "No audios configured." : "Preparing downloads...");

        if (total == 0)
        {
            FinishDownloads();
            yield break;
        }

        for (int i = 0; i < audios.Count; i++)
        {
            AudioEntry entry = audios[i];
            if (entry == null) { completed++; continue; }

            string localPath = BuildLocalPath(localRoot, entry);

            if (IsFileValid(localPath))
            {
                completed++;
                progressUI?.SetProgress((float)completed / total, $"{completed}/{total}");
                continue;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(localPath) ?? localRoot);

            string tempPath = localPath + ".tmp";
            if (File.Exists(tempPath)) SafeDelete(tempPath);

            progressUI?.SetProgress((float)completed / total, $"{completed + 1}/{total}");

            bool success = false;
            string primaryUrl = BuildUrl(entry, useFallback: false);
            Debug.Log($"[AudioManager] Downloading {entry.fileName} from Cloudflare: {primaryUrl}");
            yield return StartCoroutine(TryDownloadSingle(primaryUrl, tempPath, localPath,
                (result) => success = result,
                (progress) => progressUI?.SetProgress((completed + progress) / total, $"{completed + 1}/{total}")));

            if (!success)
            {
                string fallbackUrl = BuildUrl(entry, useFallback: true);
                Debug.LogWarning($"[AudioManager] Cloudflare failed for {entry.fileName}, retrying with GitHub: {fallbackUrl}");
                SafeDelete(tempPath);
                yield return StartCoroutine(TryDownloadSingle(fallbackUrl, tempPath, localPath,
                    (result) => success = result,
                    (progress) => progressUI?.SetProgress((completed + progress) / total, $"{completed + 1}/{total}")));
            }

            if (!success)
            {
                Debug.LogError($"[AudioManager] Both sources failed for {entry.fileName}. Putting in later list.");
                SafeDelete(tempPath);
                _laterList.Add(entry);
                continue;
            }

            Debug.Log($"[AudioManager] Successfully downloaded {entry.fileName}.");

            completed++;
            progressUI?.SetProgress((float)completed / total, $"{completed}/{total}");
        }

        _isDownloading = false;

        if (_laterList.Count > 0)
        {
            Debug.LogWarning($"[AudioManager] {_laterList.Count} file(s) could not be downloaded. Retrying in background.");
            StartCoroutine(FadeOutDialogue());
            _backgroundRetryCoroutine = StartCoroutine(BackgroundRetryCoroutine());
        }
        else
        {
            FinishDownloads();
        }
    }

    /// <summary>
    /// Attempts to download a single file from <paramref name="url"/> to <paramref name="localPath"/>
    /// via a temp file. Reports download progress [0,1] via <paramref name="onProgress"/>.
    /// Calls <paramref name="onResult"/> with true on success, false on any failure.
    /// </summary>
    private IEnumerator TryDownloadSingle(string url, string tempPath, string localPath,
        Action<bool> onResult, Action<float> onProgress = null)
    {
        Debug.Log($"[AudioManager] Requesting: {url}");
        using (UnityWebRequest req = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET))
        {
            req.downloadHandler = new DownloadHandlerFile(tempPath, true);

            if (timeoutSeconds > 0)
                req.timeout = timeoutSeconds;

            req.SendWebRequest();

            while (!req.isDone)
            {
                onProgress?.Invoke(Mathf.Clamp01(req.downloadProgress));
                yield return null;
            }

#if UNITY_2020_2_OR_NEWER
            bool ok = req.result == UnityWebRequest.Result.Success;
#else
            bool ok = !req.isNetworkError && !req.isHttpError;
#endif

            if (!ok)
            {
                Debug.LogError($"[AudioManager] Download failed: {url}\nError: {req.error}");
                SafeDelete(tempPath);
                onResult(false);
                yield break;
            }
        }

        SafeDelete(localPath);
        File.Move(tempPath, localPath);

        if (!IsFileValid(localPath))
        {
            Debug.LogError($"[AudioManager] Downloaded file too small, treating as invalid: {localPath}");
            SafeDelete(localPath);
            onResult(false);
            yield break;
        }

        onResult(true);
    }

    private IEnumerator BackgroundRetryCoroutine()
    {
        while (_laterList.Count > 0)
        {
            yield return new WaitForSeconds(16f);

            string localRoot = Path.Combine(Application.persistentDataPath, localRootFolderName);
            var toRetry = new List<AudioEntry>(_laterList);

            foreach (AudioEntry entry in toRetry)
            {
                string localPath = BuildLocalPath(localRoot, entry);

                if (IsFileValid(localPath))
                {
                    _laterList.Remove(entry);
                    continue;
                }

                Directory.CreateDirectory(Path.GetDirectoryName(localPath) ?? localRoot);
                string tempPath = localPath + ".tmp";
                SafeDelete(tempPath);

                bool success = false;
                string primaryUrl = BuildUrl(entry, useFallback: false);
                Debug.Log($"[AudioManager] Background retry: downloading {entry.fileName} from Cloudflare: {primaryUrl}");
                yield return StartCoroutine(TryDownloadSingle(primaryUrl, tempPath, localPath,
                    (result) => success = result));

                if (!success)
                {
                    SafeDelete(tempPath);
                    string fallbackUrl = BuildUrl(entry, useFallback: true);
                    Debug.LogWarning($"[AudioManager] Background retry: Cloudflare failed for {entry.fileName}, trying GitHub: {fallbackUrl}");
                    yield return StartCoroutine(TryDownloadSingle(fallbackUrl, tempPath, localPath,
                        (result) => success = result));
                }

                if (success)
                {
                    Debug.Log($"[AudioManager] Background retry succeeded for {entry.fileName}.");
                    _laterList.Remove(entry);
                }
                else
                {
                    Debug.LogWarning($"[AudioManager] Background retry still failing for {entry.fileName}. Will try again in 16s.");
                }
            }
        }

        Debug.Log("[AudioManager] Background retry queue cleared.");
        _backgroundRetryCoroutine = null;
    }

    private IEnumerator FadeOutDialogue()
    {
        if (download_dialogue == null) yield break;

        var cg = download_dialogue.GetComponent<CanvasGroup>();
        if (cg == null)
        {
            // No CanvasGroup — just hide immediately.
            download_dialogue.SetActive(false);
            yield break;
        }

        float duration = 0.5f;
        float elapsed = 0f;
        float startAlpha = cg.alpha;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / duration);
            yield return null;
        }

        cg.alpha = 0f;
        download_dialogue.SetActive(false);
    }

    private void FinishDownloads()
    {
        progressUI?.SetProgress(1f, "Downloads complete.");

        if (download_dialogue != null)
            download_dialogue.SetActive(false);

        _isDownloading = false;
    }

    private bool AreAllAudiosPresent()
    {
        string localRoot = Path.Combine(Application.persistentDataPath, localRootFolderName);
        if (audios == null || audios.Count == 0) return true;

        for (int i = 0; i < audios.Count; i++)
        {
            var entry = audios[i];
            if (entry == null) continue;

            string localPath = BuildLocalPath(localRoot, entry);
            if (!IsFileValid(localPath))
                return false;
        }
        return true;
    }

    private bool IsFileValid(string path)
    {
        if (!File.Exists(path)) return false;
        if (minValidBytes <= 0) return true;

        try
        {
            var info = new FileInfo(path);
            return info.Length >= minValidBytes;
        }
        catch
        {
            return false;
        }
    }

    private void SafeDelete(string path)
    {
        try
        {
            if (File.Exists(path))
                File.Delete(path);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[AudioManager] Could not delete file: {path}\n{e.Message}");
        }
    }

    private string BuildUrl(AudioEntry entry, bool useFallback = false)
    {
        string b = useFallback
            ? (fallbackBaseUrl ?? "").TrimEnd('/')
            : (baseUrl ?? "").TrimEnd('/');
        return $"{b}/{entry.fileName}.mp3";
    }

    private string BuildLocalPath(string localRoot, AudioEntry entry)
    {
        return BuildLocalPath(localRoot, entry.folderName, entry.fileName);
    }

    private static string BuildLocalPath(string localRoot, string folderName, string fileName)
    {
        string folder = (folderName ?? "").Trim('/', '\\');
        string file = (fileName ?? "").Trim();

        if (string.IsNullOrWhiteSpace(folder))
            return Path.Combine(localRoot, $"{file}.mp3");

        return Path.Combine(localRoot, folder, $"{file}.mp3");
    }

    // ---------------------------
    // Data + UI helper classes
    // ---------------------------

    [Serializable]
    public class AudioEntry
    {
        [Tooltip("Remote folder under baseUrl. Example: 'sfx' or 'music'. Can be empty.")]
        public string folderName;

        [Tooltip("File name WITHOUT extension. Example: 'click_01' (script appends .mp3).")]
        public string fileName;
    }

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

    private void Start()
    {
        Debug.Log("Persistent path: " + Application.persistentDataPath);
        check_audio_integrity();
    }
}