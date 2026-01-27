using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class GharanaSelector : MonoBehaviour
{
    [Header("References")]
    public ScrollRect scrollRect;

    [Header("Control (edit this in Inspector)")]
    [Range(0f, 1f)]
    public float targetXNormalized = 0f;

    [Tooltip("If ON, targetXNormalized drives the ScrollRect every frame.")]
    public bool driveFromTarget = true;

    [Header("Readout (do not edit)")]
    [Range(0f, 1f)]
    public float currentXNormalized = 0f;

    public Vector2 contentAnchoredPos;

    private void Reset()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Awake()
    {
        if (scrollRect == null)
            scrollRect = GetComponent<ScrollRect>();
    }

    private void OnEnable()
    {
        // Initialize target from current position so it doesn't jump on play.
        if (scrollRect != null)
            targetXNormalized = scrollRect.horizontalNormalizedPosition;
    }

    private void OnValidate()
    {
        if (scrollRect == null)
            scrollRect = GetComponent<ScrollRect>();

        targetXNormalized = Mathf.Clamp01(targetXNormalized);

        // In edit mode, apply immediately when you tweak the slider.
        if (!Application.isPlaying)
            ApplyTarget();
    }

    private void Update()
    {
        if (scrollRect == null) return;

        currentXNormalized = scrollRect.horizontalNormalizedPosition;

        if (scrollRect.content != null)
            contentAnchoredPos = scrollRect.content.anchoredPosition;
    }

    private void LateUpdate()
    {
        if (!driveFromTarget) return;
        ApplyTarget();
    }

    public void SetXNormalized(float x)
    {
        targetXNormalized = Mathf.Clamp01(x);
        ApplyTarget();
    }

    private void ApplyTarget()
    {
        if (scrollRect == null) return;

        float x = Mathf.Clamp01(targetXNormalized);

        // Set only X, preserve Y
        scrollRect.normalizedPosition = new Vector2(x, scrollRect.normalizedPosition.y);
    }
}
