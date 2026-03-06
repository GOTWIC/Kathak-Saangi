using System.Runtime.InteropServices;
using UnityEngine;

public class IOSAudioSessionSetup : MonoBehaviour
{
#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void SetAudioSessionPlayback();
#endif

    void Awake()
    {
#if UNITY_IOS && !UNITY_EDITOR
        SetAudioSessionPlayback();
#endif
    }
}