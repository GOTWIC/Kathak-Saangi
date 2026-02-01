using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteAlways]
public class ScrollValueInspector : MonoBehaviour
{
    // --- Serialized fields ---
    [SerializeField] private ScrollRect scrollRect;

    // Init value you can edit in the Inspector (0 = bottom/left, 1 = top/right)
    [SerializeField, Range(0f, 1f)] private float initialNormalized = 0.88f;

    // Current value (visible, read-only via custom inspector below)
    [SerializeField] private float scrollValue;

    public float Value => scrollValue;

    private void OnEnable()
    {
        Hook();
        Refresh(force: true);
    }

    private void OnDisable()
    {
        Unhook();
    }

    private void OnValidate()
    {
        if (scrollRect == null)
            scrollRect = GetComponent<ScrollRect>();

        Hook();
        Refresh(force: true);
    }

    private void Start()
    {
        // Apply ONLY on Start (Play Mode)
        StartCoroutine(ApplyInitialNextFrame());
    }

    private void Update()
    {
        // Keep inspector updated in Edit Mode (events often don't fire)
        if (!Application.isPlaying)
            Refresh(force: false);
    }

    private IEnumerator ApplyInitialNextFrame()
    {
        // Let layouts/content size settle first
        yield return null;

        Canvas.ForceUpdateCanvases();
        ApplyInitialImmediate();
        Refresh(force: true);
    }

    private void ApplyInitialImmediate()
    {
        if (scrollRect == null) return;

        if (scrollRect.vertical && !scrollRect.horizontal)
        {
            scrollRect.verticalNormalizedPosition = initialNormalized;
        }
        else if (scrollRect.horizontal && !scrollRect.vertical)
        {
            scrollRect.horizontalNormalizedPosition = initialNormalized;
        }
        else
        {
            // If both are enabled, prefer vertical (typical Scroll View).
            if (scrollRect.vertical)
                scrollRect.verticalNormalizedPosition = initialNormalized;
            else if (scrollRect.horizontal)
                scrollRect.horizontalNormalizedPosition = initialNormalized;
        }

        Canvas.ForceUpdateCanvases();
    }

    private void Hook()
    {
        if (scrollRect == null) return;

        scrollRect.onValueChanged.RemoveListener(OnScrollChanged);
        scrollRect.onValueChanged.AddListener(OnScrollChanged);
    }

    private void Unhook()
    {
        if (scrollRect == null) return;
        scrollRect.onValueChanged.RemoveListener(OnScrollChanged);
    }

    private void OnScrollChanged(Vector2 _)
    {
        Refresh(force: false);
    }

    private void Refresh(bool force)
    {
        if (scrollRect == null) return;

        float v = GetNormalizedValue(scrollRect);

        if (force || !Mathf.Approximately(v, scrollValue))
        {
            scrollValue = v;

#if UNITY_EDITOR
            // Ensures Inspector updates in Edit Mode when value changes
            if (!Application.isPlaying)
                UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }

    private static float GetNormalizedValue(ScrollRect sr)
    {
        if (sr == null) return 0f;

        if (sr.vertical && !sr.horizontal) return sr.verticalNormalizedPosition;
        if (sr.horizontal && !sr.vertical) return sr.horizontalNormalizedPosition;

        if (sr.vertical) return sr.verticalNormalizedPosition;
        if (sr.horizontal) return sr.horizontalNormalizedPosition;

        return 0f;
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(ScrollValueInspector))]
public class ScrollValueInspectorEditor_ReadOnlyValue : UnityEditor.Editor
{
    private UnityEditor.SerializedProperty scrollRectProp;
    private UnityEditor.SerializedProperty initialNormalizedProp;
    private UnityEditor.SerializedProperty scrollValueProp;

    private void OnEnable()
    {
        scrollRectProp = serializedObject.FindProperty("scrollRect");
        initialNormalizedProp = serializedObject.FindProperty("initialNormalized");
        scrollValueProp = serializedObject.FindProperty("scrollValue");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        UnityEditor.EditorGUILayout.PropertyField(scrollRectProp);
        UnityEditor.EditorGUILayout.PropertyField(initialNormalizedProp);

        using (new UnityEditor.EditorGUI.DisabledScope(true))
        {
            UnityEditor.EditorGUILayout.PropertyField(scrollValueProp);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif