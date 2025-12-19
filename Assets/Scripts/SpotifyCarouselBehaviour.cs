using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class SpotifyCarouselBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScrollRect scrollRect;

    [Header("Start Position")]
    [Tooltip("If true, start at the top of the list (verticalNormalizedPosition = 1).")]
    [SerializeField] private bool startAtTop = true;

    private void Reset()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Awake()
    {
        if (!scrollRect)
            scrollRect = GetComponent<ScrollRect>();

        // Vertical-only scrolling
        scrollRect.vertical = true;
        scrollRect.horizontal = false;

        // Elastic at edges, free in the middle
        scrollRect.movementType = ScrollRect.MovementType.Elastic;

        // Smooth swipe behavior
        scrollRect.inertia = true;
        scrollRect.decelerationRate = 0.135f;   // Unity default; tweak if you want "heavier" scroll
    }

    private void Start()
    {
        if (startAtTop)
        {
            // 1 = top, 0 = bottom for verticalNormalizedPosition
            scrollRect.verticalNormalizedPosition = 1f;
        }
    }
}
