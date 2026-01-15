using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleOnClick : MonoBehaviour, IPointerClickHandler
{
    [Header("Scene target (found at runtime)")]
    [SerializeField] private string canvasTag = "MainCanvas";
    private GameObject canvasToEnable;

    [Header("Prefab target")]
    [SerializeField] private GameObject prefabRootToDisable; // can be the prefab root or any parent

    private void Awake()
    {
        FindCanvas();
    }

    private void FindCanvas()
    {
        var go = GameObject.FindGameObjectWithTag(canvasTag);
        if (go != null)
        {
            canvasToEnable = go;
            Debug.Log($"[ToggleOnClick] Found canvas '{canvasToEnable.name}' with tag '{canvasTag}' on '{gameObject.name}'");
        }
        else
        {
            Debug.LogWarning($"[ToggleOnClick] Could not find GameObject with tag '{canvasTag}' on '{gameObject.name}'. Will try again on click.");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Try to find canvas if it wasn't found in Awake (e.g., if it was instantiated later)
        if (canvasToEnable == null)
        {
            FindCanvas();
        }

        if (canvasToEnable != null)
        {
            Debug.Log($"[ToggleOnClick] Enabling canvas '{canvasToEnable.name}' from '{gameObject.name}'");
            canvasToEnable.SetActive(true);
        }
        else
        {
            Debug.LogError($"[ToggleOnClick] Cannot enable canvas - no GameObject found with tag '{canvasTag}' on '{gameObject.name}'");
        }

        // If you want to disable the prefab instance itself:
        // - easiest is disable the root (or this.gameObject / transform.root)
        var toDisable = prefabRootToDisable != null ? prefabRootToDisable : transform.root.gameObject;
        Debug.Log($"[ToggleOnClick] Disabling '{toDisable.name}' from '{gameObject.name}'");
        toDisable.SetActive(false);
    }
}
