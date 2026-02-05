using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class ImageScaler : MonoBehaviour
{
    // Target width in UI units (typically pixels on a Screen Space Canvas)
    public float width = 900f;

    /// <summary>
    /// Recomputes and applies RectTransform size based on the Image's Sprite aspect ratio.
    /// Call this externally after setting/changing the sprite.
    /// </summary>
    public void set_dim()
    {
        Image img = GetComponent<Image>();
        RectTransform rt = GetComponent<RectTransform>();

        if (img.sprite == null)
        {
            Debug.LogWarning($"{nameof(ImageScaler)} on '{name}': Image has no sprite.");
            return;
        }

        // Sprite's source rectangle in pixels (handles atlases/cropped sprites correctly)
        Rect spriteRect = img.sprite.rect;

        float spriteWidth = spriteRect.width;
        float spriteHeight = spriteRect.height;

        if (spriteWidth <= 0f || spriteHeight <= 0f)
        {
            Debug.LogWarning($"{nameof(ImageScaler)} on '{name}': Invalid sprite dimensions.");
            return;
        }

        // aspect = height / width
        float aspect = spriteHeight / spriteWidth;
        float newHeight = aspect * width;

        // Apply size to RectTransform (respects anchors better than setting sizeDelta directly)
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
    }
}
