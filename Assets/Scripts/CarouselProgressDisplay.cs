using UnityEngine;
using UnityEngine.UI;

public class CarouselProgressDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Slider slider;

    [Header("Readout")]
    [Range(0f, 1f)]
    public float progress01;

    // Guards against listener feedback loops
    private bool _updatingFromScroll;
    private bool _updatingFromSlider;

    private void Reset()
    {
        // Try to auto-fill common cases
        if (scrollRect == null) scrollRect = GetComponent<ScrollRect>();
        if (slider == null) slider = GetComponent<Slider>();
    }

    private void Awake()
    {
        // Configure slider as 0..1 progress bar (still draggable if you want)
        if (slider != null)
        {
            slider.minValue = 0f;
            slider.maxValue = 1f;
            slider.wholeNumbers = false;
        }
    }

    private void OnEnable()
    {
        if (scrollRect != null)
            scrollRect.onValueChanged.AddListener(OnScrollRectChanged);

        if (slider != null)
            slider.onValueChanged.AddListener(OnSliderChanged);

        // Initial sync
        SyncFromScroll();
    }

    private void OnDisable()
    {
        if (scrollRect != null)
            scrollRect.onValueChanged.RemoveListener(OnScrollRectChanged);

        if (slider != null)
            slider.onValueChanged.RemoveListener(OnSliderChanged);
    }

    private void OnScrollRectChanged(Vector2 _)
    {
        if (_updatingFromSlider) return;
        SyncFromScroll();
    }

    private void OnSliderChanged(float _)
    {
        if (_updatingFromScroll) return;
        SyncFromSlider();
    }

    private void SyncFromScroll()
    {
        if (scrollRect == null) return;

        _updatingFromScroll = true;

        progress01 = Mathf.Clamp01(scrollRect.horizontalNormalizedPosition);

        if (slider != null)
            slider.SetValueWithoutNotify(progress01);

        _updatingFromScroll = false;
    }

    private void SyncFromSlider()
    {
        if (slider == null || scrollRect == null) return;

        _updatingFromSlider = true;

        progress01 = Mathf.Clamp01(slider.value);

        // Stop inertia so setting the value feels responsive
        scrollRect.velocity = Vector2.zero;
        scrollRect.horizontalNormalizedPosition = progress01;

        _updatingFromSlider = false;
    }
}