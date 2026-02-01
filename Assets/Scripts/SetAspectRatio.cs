using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class SetAspectRatio : MonoBehaviour
{
    [Header("Aspect Ratios")]
    [SerializeField] private float portraitAspect = 0.5f;
    [SerializeField] private float landscapeAspect = 2f;

    private Image _image;
    private AspectRatioFitter _fitter;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _fitter = GetComponent<AspectRatioFitter>();

        if (_image == null)
        {
            Debug.LogError($"{nameof(SetAspectRatio)}: No Image component found on {name}.", this);
            enabled = false;
            return;
        }

        if (_fitter == null)
        {
            Debug.LogError($"{nameof(SetAspectRatio)}: No AspectRatioFitter found on {name}.", this);
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        ApplyAspect();
    }

#if UNITY_EDITOR
    // Updates in editor when values change / component toggled.
    private void OnValidate()
    {
        if (!isActiveAndEnabled) return;
        _image = GetComponent<Image>();
        _fitter = GetComponent<AspectRatioFitter>();
        if (_image != null && _fitter != null)
            ApplyAspect();
    }
#endif

    private void ApplyAspect()
    {
        var sprite = _image.sprite;
        if (sprite == null || sprite.texture == null)
        {
            Debug.LogWarning($"{nameof(SetAspectRatio)}: Image has no sprite/texture on {name}.", this);
            return;
        }

        // Use the sprite's rect (important if it's in an atlas).
        float w = sprite.rect.width;
        float h = sprite.rect.height;

        // If square, treat as landscape by default.
        bool isPortrait = h > w;

        _fitter.aspectRatio = isPortrait ? portraitAspect : landscapeAspect;
    }
}