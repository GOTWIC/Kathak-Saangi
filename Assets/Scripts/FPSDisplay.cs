using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private float refreshInterval = 0.5f;

    float timer;
    int frames;
    float fps;

    void Update()
    {
        frames++;
        timer += Time.unscaledDeltaTime;

        if (timer >= refreshInterval)
        {
            fps = frames / timer;
            frames = 0;
            timer = 0f;

            if (fpsText != null)
            {
                fpsText.text = $"{fps:0.} FPS";
            }
        }
    }
}
