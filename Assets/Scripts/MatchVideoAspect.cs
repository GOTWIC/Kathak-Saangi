using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(AspectRatioFitter))]
public class MatchVideoAspect : MonoBehaviour
{
    public VideoPlayer player;
    public AspectRatioFitter.AspectMode mode = AspectRatioFitter.AspectMode.FitInParent;

    private AspectRatioFitter fitter;

    void Awake()
    {
        fitter = GetComponent<AspectRatioFitter>();
        fitter.aspectMode = mode;

        if (player != null)
            player.prepareCompleted += OnPrepared;
    }

    void OnEnable()
    {
        if (player != null && !player.isPrepared)
            player.Prepare();
        else if (player != null && player.isPrepared)
            OnPrepared(player);
    }

    void OnPrepared(VideoPlayer vp)
    {
        // width/height become valid after Prepare/prepareCompleted
        if (vp.height > 0)
            fitter.aspectRatio = (float)vp.width / (float)vp.height;
    }

    void OnDestroy()
    {
        if (player != null)
            player.prepareCompleted -= OnPrepared;
    }
}
