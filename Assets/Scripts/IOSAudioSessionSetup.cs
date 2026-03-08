using System.Runtime.InteropServices;
using UnityEngine;

public class IOSAudioSessionSetup : MonoBehaviour
{
#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void InitPlaybackAudioSession();

    [DllImport("__Internal")]
    private static extern void SetAudioSessionPlayback();
#endif

    void Awake()
    {
#if UNITY_IOS && !UNITY_EDITOR
        InitPlaybackAudioSession();
#endif
    }

    void OnApplicationPause(bool pause)
    {
#if UNITY_IOS && !UNITY_EDITOR
        if (!pause)
        {
            SetAudioSessionPlayback();
        }
#endif
    }

    void OnApplicationFocus(bool hasFocus)
    {
#if UNITY_IOS && !UNITY_EDITOR
        if (hasFocus)
        {
            SetAudioSessionPlayback();
        }
#endif
    }
}