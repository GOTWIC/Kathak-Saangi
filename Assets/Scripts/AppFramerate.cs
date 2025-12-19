using UnityEngine;

public class AppFramerate : MonoBehaviour
{
    void Awake()
    {
        // vSync is ignored on mobile, but setting this is harmless
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 144;
    }
}