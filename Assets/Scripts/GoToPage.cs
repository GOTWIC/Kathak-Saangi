using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class GoToPage : MonoBehaviour, IPointerClickHandler
{
    public enum SlideDirection { Left, Right }

    [Header("Canvas to Enable")]
    [SerializeField] private GameObject canvasToEnable;

    [Header("Canvas to Disable")]
    [SerializeField] private GameObject canvasToDisable;

    [Header("Slide-out")]
    [SerializeField] private SlideDirection direction = SlideDirection.Right;
    [SerializeField] private float velocity = 10000f;
    [SerializeField] private float threshold = 6000f;

    private static bool _isSliding;

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

        if (_isSliding) return;

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
            _isSliding = true;
            StartCoroutine(SlideOutAndDisable());
        }
        else
        {
            Debug.LogWarning($"[GoToPage] CanvasToDisable is NULL on '{gameObject.name}'");
        }
    }

    private IEnumerator SlideOutAndDisable()
    {
        GameObject canvas = canvasToDisable;
        canvas.transform.SetAsLastSibling();

        float thresholdX = direction == SlideDirection.Left ? -threshold : threshold;

        // Screen Space canvases reset the root each frame; slide all children instead
        if (canvas.GetComponent<Canvas>() != null && canvas.transform.childCount > 0)
        {
            int childCount = canvas.transform.childCount;
            RectTransform[] childRects = new RectTransform[childCount];
            Vector2[] startAnchored = new Vector2[childCount];
            Transform[] childTransforms = new Transform[childCount];
            Vector3[] startPositions = new Vector3[childCount];
            bool useRect = false;

            for (int i = 0; i < childCount; i++)
            {
                Transform childTr = canvas.transform.GetChild(i);
                childTransforms[i] = childTr;
                startPositions[i] = childTr.position;
                RectTransform rt = childTr.GetComponent<RectTransform>();
                if (rt != null)
                {
                    childRects[i] = rt;
                    startAnchored[i] = rt.anchoredPosition;
                    useRect = true;
                }
            }

            bool usedRectLead = useRect && childRects[0] != null;
            if (usedRectLead)
            {
                RectTransform lead = childRects[0];
                if (direction == SlideDirection.Left)
                {
                    while (lead.anchoredPosition.x > thresholdX)
                    {
                        float delta = velocity * Time.deltaTime;
                        for (int i = 0; i < childCount; i++)
                        {
                            if (childRects[i] != null)
                            {
                                Vector2 pos = childRects[i].anchoredPosition;
                                pos.x -= delta;
                                childRects[i].anchoredPosition = pos;
                            }
                            else
                            {
                                childTransforms[i].position += Vector3.left * delta;
                            }
                        }
                        yield return null;
                    }
                }
                else
                {
                    while (lead.anchoredPosition.x < thresholdX)
                    {
                        float delta = velocity * Time.deltaTime;
                        for (int i = 0; i < childCount; i++)
                        {
                            if (childRects[i] != null)
                            {
                                Vector2 pos = childRects[i].anchoredPosition;
                                pos.x += delta;
                                childRects[i].anchoredPosition = pos;
                            }
                            else
                            {
                                childTransforms[i].position += Vector3.right * delta;
                            }
                        }
                        yield return null;
                    }
                }
            }
            else
            {
                Transform lead = childTransforms[0];
                if (direction == SlideDirection.Left)
                {
                    while (lead.position.x > thresholdX)
                    {
                        float delta = velocity * Time.deltaTime;
                        for (int i = 0; i < childCount; i++)
                            childTransforms[i].position += Vector3.left * delta;
                        yield return null;
                    }
                }
                else
                {
                    while (lead.position.x < thresholdX)
                    {
                        float delta = velocity * Time.deltaTime;
                        for (int i = 0; i < childCount; i++)
                            childTransforms[i].position += Vector3.right * delta;
                        yield return null;
                    }
                }
            }

            for (int i = 0; i < childCount; i++)
            {
                if (usedRectLead && childRects[i] != null)
                    childRects[i].anchoredPosition = startAnchored[i];
                else
                    childTransforms[i].position = startPositions[i];
            }
        }
        else
        {
            RectTransform rectToMove = canvas.GetComponent<RectTransform>();
            if (rectToMove != null)
            {
                Vector2 startPos = rectToMove.anchoredPosition;
                if (direction == SlideDirection.Left)
                {
                    while (rectToMove.anchoredPosition.x > thresholdX)
                    {
                        Vector2 pos = rectToMove.anchoredPosition;
                        pos.x -= velocity * Time.deltaTime;
                        rectToMove.anchoredPosition = pos;
                        yield return null;
                    }
                }
                else
                {
                    while (rectToMove.anchoredPosition.x < thresholdX)
                    {
                        Vector2 pos = rectToMove.anchoredPosition;
                        pos.x += velocity * Time.deltaTime;
                        rectToMove.anchoredPosition = pos;
                        yield return null;
                    }
                }
                rectToMove.anchoredPosition = startPos;
            }
            else
            {
                Transform t = canvas.transform;
                Vector3 startPos = t.position;
                if (direction == SlideDirection.Left)
                {
                    while (t.position.x > thresholdX)
                    {
                        t.position += Vector3.left * (velocity * Time.deltaTime);
                        yield return null;
                    }
                }
                else
                {
                    while (t.position.x < thresholdX)
                    {
                        t.position += Vector3.right * (velocity * Time.deltaTime);
                        yield return null;
                    }
                }
                t.position = startPos;
            }
        }

        canvas.SetActive(false);
        _isSliding = false;
    }
}

