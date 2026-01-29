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

    private void Start()
    {
        // If this GameObject has the "Back" tag, override canvas references
        if (gameObject.CompareTag("Back"))
        {
            Debug.Log($"[GoToPage] GameObject '{gameObject.name}' has 'Back' tag. Overriding canvas references...");
            
            // Find the topmost Canvas parent
            canvasToDisable = FindTopmostCanvasParent();
            if (canvasToDisable != null)
            {
                Debug.Log($"[GoToPage] Set canvasToDisable to topmost canvas: '{canvasToDisable.name}'");
            }
            else
            {
                Debug.LogWarning($"[GoToPage] Could not find topmost Canvas parent for '{gameObject.name}'");
            }
            
            // Find the Canvas with the "Main" tag
            canvasToEnable = FindMainCanvas();
            if (canvasToEnable != null)
            {
                Debug.Log($"[GoToPage] Set canvasToEnable to Main canvas: '{canvasToEnable.name}'");
            }
            else
            {
                Debug.LogWarning($"[GoToPage] Could not find Canvas with 'Main' tag in the scene");
            }
        }
    }

    private GameObject FindTopmostCanvasParent()
    {
        Transform current = transform;
        Canvas topmostCanvas = null;

        // Traverse up the parent hierarchy
        while (current != null)
        {
            Canvas canvas = current.GetComponent<Canvas>();
            if (canvas != null)
            {
                topmostCanvas = canvas;
            }
            current = current.parent;
        }

        return topmostCanvas != null ? topmostCanvas.gameObject : null;
    }

    private GameObject FindMainCanvas()
    {
        // Find all Canvas components in the scene
        Canvas[] allCanvases = FindObjectsOfType<Canvas>(includeInactive: true);
        
        foreach (Canvas canvas in allCanvases)
        {
            if (canvas.gameObject.CompareTag("Main"))
            {
                return canvas.gameObject;
            }
        }

        return null;
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

