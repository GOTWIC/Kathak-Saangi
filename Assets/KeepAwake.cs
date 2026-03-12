using UnityEngine;

public class KeepAwake : MonoBehaviour
{
    void OnEnable()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void OnDisable()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }
}