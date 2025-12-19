using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaFitter : MonoBehaviour {
  RectTransform rt; Rect last;
  void Awake(){ rt = GetComponent<RectTransform>(); Apply(); }
  void OnRectTransformDimensionsChange(){ Apply(); }
  void Apply(){
    var sa = Screen.safeArea; if (sa == last) return; last = sa;
    Vector2 min = sa.position, max = sa.position + sa.size;
    min.x /= Screen.width;  min.y /= Screen.height;
    max.x /= Screen.width;  max.y /= Screen.height;
    rt.anchorMin = min; rt.anchorMax = max;
    rt.offsetMin = rt.offsetMax = Vector2.zero;
  }
}