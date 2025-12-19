using UnityEngine;
using UnityEngine.UI;

public class CarouselScaleEffect : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScrollRect scrollRect;

    [Header("Scale Settings")]
    [SerializeField] private float minScale = 0.8f;
    [SerializeField] private float maxScale = 1.0f;

    // Optional: how far from the center the effect reaches.
    // If 0, we use half the viewport width (edge of the screen).
    [SerializeField] private float customEffectRadius = 0f;

    private RectTransform viewport;
    private RectTransform content;

    private void Awake()
    {
        if (!scrollRect)
            scrollRect = GetComponent<ScrollRect>();

        viewport = scrollRect.viewport;
        content = scrollRect.content;

        // If you want, you can also hook this to ScrollRect.onValueChanged
        // scrollRect.onValueChanged.AddListener(_ => UpdateScales());
    }

    private void LateUpdate()
    {
        UpdateScales();
    }

    private void UpdateScales()
    {
        if (!viewport || !content) return;

        // Center of the viewport in world space
        var vpRect = viewport.rect;
        Vector3 viewportCenterWorld =
            viewport.TransformPoint(vpRect.center);

        // Distance from center to edge (in world units along X)
        float maxDistance;
        if (customEffectRadius > 0f)
        {
            maxDistance = customEffectRadius;
        }
        else
        {
            // half the viewport width → left/right edges
            maxDistance = vpRect.width * 0.5f;
        }

        // Loop through all children under Content (your cards)
        for (int i = 0; i < content.childCount; i++)
        {
            RectTransform card = content.GetChild(i) as RectTransform;
            if (card == null) continue;

            // If you have spacer objects, you can skip them here
            // e.g. if (!card.GetComponent<Image>()) continue;

            // Center of this card in world space
            var cardRect = card.rect;
            Vector3 cardCenterWorld = card.TransformPoint(cardRect.center);

            // Horizontal distance from viewport center
            float distance = Mathf.Abs(cardCenterWorld.x - viewportCenterWorld.x);


            // Normalize distance → 0 at center, 1 at or beyond edge
            float t = Mathf.Clamp01(distance / maxDistance);

            // Interpolate scale (0 = center → maxScale, 1 = edge → minScale)
            float scale = Mathf.Lerp(maxScale, minScale, t);

            card.localScale = new Vector3(scale, scale, 1f);

            if (card.name == "Card 4"){
                Debug.Log("Distance: " + distance + " Max Distance: " + maxDistance + " T: " + t + " Scale: " + scale);
            }
        }
    }
}
