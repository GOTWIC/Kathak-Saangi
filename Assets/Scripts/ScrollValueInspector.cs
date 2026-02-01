using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ScrollValueInspector : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private float scrollValue; // serialized so you can see it

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
        // Convenience: if placed on the Scroll View, auto-grab ScrollRect
        if (scrollRect == null)
            scrollRect = GetComponent<ScrollRect>();

        Hook();
        Refresh(force: true);
    }

    private void Update()
    {
        // In edit mode, events may not fire, so we poll.
        if (!Application.isPlaying)
            Refresh(force: false);
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
            // Helps inspector update in Edit Mode
            if (!Application.isPlaying)
                UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }

    private static float GetNormalizedValue(ScrollRect sr)
    {
        if (sr.vertical && !sr.horizontal) return sr.verticalNormalizedPosition;
        if (sr.horizontal && !sr.vertical) return sr.horizontalNormalizedPosition;

        // If both enabled, prefer vertical (typical Scroll View).
        if (sr.vertical) return sr.verticalNormalizedPosition;
        if (sr.horizontal) return sr.horizontalNormalizedPosition;

        return 0f;
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(ScrollValueInspector))]
public class ScrollValueInspectorEditor : UnityEditor.Editor
{
    private UnityEditor.SerializedProperty scrollRectProp;
    private UnityEditor.SerializedProperty scrollValueProp;

    private void OnEnable()
    {
        scrollRectProp = serializedObject.FindProperty("scrollRect");
        scrollValueProp = serializedObject.FindProperty("scrollValue");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        UnityEditor.EditorGUILayout.PropertyField(scrollRectProp);

        using (new UnityEditor.EditorGUI.DisabledScope(true))
        {
            UnityEditor.EditorGUILayout.PropertyField(scrollValueProp);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif