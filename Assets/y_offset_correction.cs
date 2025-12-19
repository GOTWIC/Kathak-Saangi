using UnityEngine;

public class y_offset_correction : MonoBehaviour
{
    [SerializeField] private RectTransform spacer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null && spacer != null)
        {
            Vector2 anchoredPosition = rectTransform.anchoredPosition;
            Debug.Log("Spacer height: " + spacer.rect.height);
            anchoredPosition.y = -spacer.rect.height;
            Debug.Log("Anchored Position: " + anchoredPosition);
            rectTransform.anchoredPosition = anchoredPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
