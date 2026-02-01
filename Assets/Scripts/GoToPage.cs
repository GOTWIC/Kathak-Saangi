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

    [Header("Reverse (slide-in)")]
    [SerializeField] private bool reverse = false;
    [SerializeField] private float slideInStartX = 2000f;

    private static bool _isSliding;

    private void Awake()
    {
        Debug.Log($"[GoToPage] Component initialized on '{gameObject.name}'. CanvasToEnable: {(canvasToEnable != null ? canvasToEnable.name : "NULL")}, CanvasToDisable: {(canvasToDisable != null ? canvasToDisable.name : "NULL")}");
    }

    private void Start()
    {
        // If this GameObject has the "Back" tag, fill in canvas references only when empty
        if (gameObject.CompareTag("Back"))
        {
            if (canvasToDisable == null)
            {
                canvasToDisable = FindTopmostCanvasParent();
                if (canvasToDisable != null)
                    Debug.Log($"[GoToPage] Set canvasToDisable to topmost canvas: '{canvasToDisable.name}'");
                else
                    Debug.LogWarning($"[GoToPage] Could not find topmost Canvas parent for '{gameObject.name}'");
            }

            if (canvasToEnable == null)
            {
                canvasToEnable = FindMainCanvas();
                if (canvasToEnable != null)
                    Debug.Log($"[GoToPage] Set canvasToEnable to Main canvas: '{canvasToEnable.name}'");
                else
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

    /// <summary>Call from Button.onClick or other code to perform the transition (same as a click).</summary>
    public void PerformTransition()
    {
        if (_isSliding) return;

        if (reverse)
        {
            if (canvasToEnable != null && canvasToDisable != null)
            {
                _isSliding = true;
                StartCoroutine(SlideInAndDisable());
            }
            else
            {
                if (canvasToEnable == null) Debug.LogWarning($"[GoToPage] CanvasToEnable is NULL on '{gameObject.name}'");
                if (canvasToDisable == null) Debug.LogWarning($"[GoToPage] CanvasToDisable is NULL on '{gameObject.name}'");
            }
        }
        else
        {
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
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"[GoToPage] OnPointerClick called on '{gameObject.name}'");
        PerformTransition();
    }

    private IEnumerator SlideOutAndDisable()
    {
        GameObject canvas = canvasToDisable;

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

    private IEnumerator SlideInAndDisable()
    {
        GameObject canvasIn = canvasToEnable;

        // Position canvas off-screen at slideInStartX, then enable and slide to 0
        if (canvasIn.GetComponent<Canvas>() != null && canvasIn.transform.childCount > 0)
        {
            int childCount = canvasIn.transform.childCount;
            RectTransform[] childRects = new RectTransform[childCount];
            Transform[] childTransforms = new Transform[childCount];
            bool useRect = false;

            for (int i = 0; i < childCount; i++)
            {
                Transform childTr = canvasIn.transform.GetChild(i);
                childTransforms[i] = childTr;
                RectTransform rt = childTr.GetComponent<RectTransform>();
                if (rt != null)
                {
                    childRects[i] = rt;
                    rt.anchoredPosition = new Vector2(slideInStartX, rt.anchoredPosition.y);
                    useRect = true;
                }
                else
                {
                    Vector3 p = childTr.position;
                    childTr.position = new Vector3(slideInStartX, p.y, p.z);
                }
            }

            canvasIn.SetActive(true);

            bool usedRectLead = useRect && childRects[0] != null;
            if (usedRectLead)
            {
                RectTransform lead = childRects[0];
                if (slideInStartX > 0f)
                {
                    while (lead.anchoredPosition.x > 0f)
                    {
                        float delta = velocity * Time.deltaTime;
                        for (int i = 0; i < childCount; i++)
                        {
                            if (childRects[i] != null)
                            {
                                Vector2 pos = childRects[i].anchoredPosition;
                                pos.x = Mathf.MoveTowards(pos.x, 0f, delta);
                                childRects[i].anchoredPosition = pos;
                            }
                            else
                            {
                                Vector3 p = childTransforms[i].position;
                                childTransforms[i].position = new Vector3(Mathf.MoveTowards(p.x, 0f, delta), p.y, p.z);
                            }
                        }
                        yield return null;
                    }
                }
                else
                {
                    while (lead.anchoredPosition.x < 0f)
                    {
                        float delta = velocity * Time.deltaTime;
                        for (int i = 0; i < childCount; i++)
                        {
                            if (childRects[i] != null)
                            {
                                Vector2 pos = childRects[i].anchoredPosition;
                                pos.x = Mathf.MoveTowards(pos.x, 0f, delta);
                                childRects[i].anchoredPosition = pos;
                            }
                            else
                            {
                                Vector3 p = childTransforms[i].position;
                                childTransforms[i].position = new Vector3(Mathf.MoveTowards(p.x, 0f, delta), p.y, p.z);
                            }
                        }
                        yield return null;
                    }
                }
            }
            else
            {
                Transform lead = childTransforms[0];
                if (slideInStartX > 0f)
                {
                    while (lead.position.x > 0f)
                    {
                        float delta = velocity * Time.deltaTime;
                        for (int i = 0; i < childCount; i++)
                        {
                            if (childRects[i] != null)
                            {
                                Vector2 pos = childRects[i].anchoredPosition;
                                pos.x = Mathf.MoveTowards(pos.x, 0f, delta);
                                childRects[i].anchoredPosition = pos;
                            }
                            else
                            {
                                Vector3 p = childTransforms[i].position;
                                childTransforms[i].position = new Vector3(Mathf.MoveTowards(p.x, 0f, delta), p.y, p.z);
                            }
                        }
                        yield return null;
                    }
                }
                else
                {
                    while (lead.position.x < 0f)
                    {
                        float delta = velocity * Time.deltaTime;
                        for (int i = 0; i < childCount; i++)
                        {
                            if (childRects[i] != null)
                            {
                                Vector2 pos = childRects[i].anchoredPosition;
                                pos.x = Mathf.MoveTowards(pos.x, 0f, delta);
                                childRects[i].anchoredPosition = pos;
                            }
                            else
                            {
                                Vector3 p = childTransforms[i].position;
                                childTransforms[i].position = new Vector3(Mathf.MoveTowards(p.x, 0f, delta), p.y, p.z);
                            }
                        }
                        yield return null;
                    }
                }
            }

            for (int i = 0; i < childCount; i++)
            {
                if (childRects[i] != null)
                    childRects[i].anchoredPosition = new Vector2(0f, childRects[i].anchoredPosition.y);
                else
                {
                    Vector3 p = childTransforms[i].position;
                    childTransforms[i].position = new Vector3(0f, p.y, p.z);
                }
            }
        }
        else
        {
            RectTransform rectToMove = canvasIn.GetComponent<RectTransform>();
            if (rectToMove != null)
            {
                Vector2 pos = rectToMove.anchoredPosition;
                pos.x = slideInStartX;
                rectToMove.anchoredPosition = pos;
            }
            else
            {
                Transform t = canvasIn.transform;
                t.position = new Vector3(slideInStartX, t.position.y, t.position.z);
            }

            canvasIn.SetActive(true);

            if (rectToMove != null)
            {
                while (Mathf.Abs(rectToMove.anchoredPosition.x) > 0.001f)
                {
                    Vector2 pos = rectToMove.anchoredPosition;
                    pos.x = Mathf.MoveTowards(pos.x, 0f, velocity * Time.deltaTime);
                    rectToMove.anchoredPosition = pos;
                    yield return null;
                }
                rectToMove.anchoredPosition = new Vector2(0f, rectToMove.anchoredPosition.y);
            }
            else
            {
                Transform t = canvasIn.transform;
                while (Mathf.Abs(t.position.x) > 0.001f)
                {
                    float x = Mathf.MoveTowards(t.position.x, 0f, velocity * Time.deltaTime);
                    t.position = new Vector3(x, t.position.y, t.position.z);
                    yield return null;
                }
                t.position = new Vector3(0f, t.position.y, t.position.z);
            }
        }

        if (canvasToDisable != null)
            canvasToDisable.SetActive(false);

        _isSliding = false;
    }
}

