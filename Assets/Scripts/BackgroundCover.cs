using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform)), RequireComponent(typeof(Image))]
public class BackgroundCover : MonoBehaviour {
  void LateUpdate() {
    var img = GetComponent<Image>();
    var rt = (RectTransform)transform;
    if (img.sprite == null) return;

    float texW = img.sprite.rect.width, texH = img.sprite.rect.height;
    float texAR = texW / texH;

    var rect = rt.rect;
    float viewAR = rect.width / Mathf.Max(1f, rect.height);

    // Cover: scale to fill, crop overflow
    img.preserveAspect = true;
    if (viewAR > texAR) {
      // screen wider than texture → fit height
      rt.sizeDelta = new Vector2(rect.height * texAR, rect.height);
    } else {
      // screen taller → fit width
      rt.sizeDelta = new Vector2(rect.width, rect.width / texAR);
    }
  }
}