using UnityEngine;
using UnityEngine.EventSystems;

public class GoToPage : MonoBehaviour, IPointerClickHandler
{
    [Header("Canvas to Enable")]
    [SerializeField] private GameObject canvasToEnable;

    [Header("Canvas to Disable")]
    [SerializeField] private GameObject canvasToDisable;

    private void Awake()
    {
        Debug.Log($"[GoToPage] Component initialized on '{gameObject.name}'. CanvasToEnable: {(canvasToEnable != null ? canvasToEnable.name : "NULL")}, CanvasToDisable: {(canvasToDisable != null ? canvasToDisable.name : "NULL")}");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"[GoToPage] OnPointerClick called on '{gameObject.name}'");
        
        if (canvasToEnable != null)
        {
            Debug.Log($"[GoToPage] Enabling canvas: '{canvasToEnable.name}'");
            canvasToEnable.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"[GoToPage] CanvasToEnable is NULL on '{gameObject.name}'");
        }

        if (canvasToDisable != null)
        {
            Debug.Log($"[GoToPage] Disabling canvas: '{canvasToDisable.name}'");
            canvasToDisable.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"[GoToPage] CanvasToDisable is NULL on '{gameObject.name}'");
        }
    }
}

