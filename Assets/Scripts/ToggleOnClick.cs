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
        var go = GameObject.FindGameObjectWithTag(canvasTag);
        if (go != null) canvasToEnable = go;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (canvasToEnable != null)
            canvasToEnable.SetActive(true);

        // If you want to disable the prefab instance itself:
        // - easiest is disable the root (or this.gameObject / transform.root)
        var toDisable = prefabRootToDisable != null ? prefabRootToDisable : transform.root.gameObject;
        toDisable.SetActive(false);
    }
}
