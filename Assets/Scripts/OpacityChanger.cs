using UnityEngine;
using UnityEngine.UI;

public class OpacityChanger : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private GameObject targetObject;

    [SerializeField] private float alpha_min;
    [SerializeField] private float alpha_max;
    [SerializeField] private float threshold_min;
    [SerializeField] private float threshold_max;
    [SerializeField] private Color color = Color.white;

    private const string TARGET_PATH = "CarouselRoot/CarouselScrollView/Viewport/Content/main";

    private void Update()
    {
        if (targetObject == null)
        {
            Transform parent = transform.parent;
            if (parent != null)
            {
                Transform main = parent.Find(TARGET_PATH);
                if (main != null && main.childCount > 0)
                {
                    // Debug.Log("[OpacityChanger] Found Content.");
                    // Debug.Log($"[OpacityChanger] Found {main.childCount} children.");
                    // Debug.Log("[OpacityChanger] Getting 1st child.");
                    targetObject = main.GetChild(0).gameObject;
                    // Debug.Log("[OpacityChanger] Found target: " + targetObject.name);
                }
            }
        }

        if (image == null || targetObject == null) return;

        float imageY = image.transform.position.y;
        float objectY = targetObject.transform.position.y;
        float diff = imageY - objectY;
        //Debug.Log(diff);

        float alpha;
        if (diff >= threshold_min)
            alpha = alpha_min;
        else if (diff <= threshold_max)
            alpha = alpha_max;
        else
            alpha = Mathf.Lerp(alpha_max, alpha_min, (diff - threshold_max) / (threshold_min - threshold_max));

        Color c = color;
        c.a = alpha;
        image.color = c;
    }
}
