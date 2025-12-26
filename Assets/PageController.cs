using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PageController : MonoBehaviour
{
    public enum ItemType
    {
        Header,
        Audio,
        Description
    }

    [Serializable]
    public class PageItem
    {
        public ItemType type = ItemType.Header;

        // Used for Header + Audio
        public string displayName;

        // Only used when type == Audio
        public AudioClip audioClip;

        // Only used when type == Description
        [TextArea(2, 8)]
        public string descriptionText;
    }

    [Header("Scene References")]
    [SerializeField] private Transform content;

    [Header("Prefabs")]
    [SerializeField] private GameObject headerPrefab;
    [SerializeField] private GameObject audioPrefab;
    [SerializeField] private GameObject descriptionPrefab;

    [Header("Data")]
    [SerializeField] private List<PageItem> items = new List<PageItem>();

    [Header("Build Options")]
    [SerializeField] private bool buildOnStart = true;
    [SerializeField] private bool clearExistingChildrenBeforeBuild = true;

    private const string HEADER_TMP_PATH = "text";                // header_container -> text -> TMP
    private const string AUDIO_TMP_PATH = "audio_element/text";   // audio_component -> audio_element -> text -> TMP
    private const string AUDIO_SOURCE_PATH = "audio_element";     // audio_component -> audio_element -> AudioSource
    private const string DESCRIPTION_TMP_PATH = "text";           // description_container -> text -> TMP

    private void Start()
    {
        if (buildOnStart)
            Rebuild();
    }

    public void Rebuild()
    {
        if (content == null)
        {
            Debug.LogError("[PageController] Content is not assigned.");
            return;
        }

        if (clearExistingChildrenBeforeBuild)
            ClearContentChildrenRuntime();

        foreach (var item in items)
        {
            if (item == null) continue;

            GameObject prefab = GetPrefabFor(item.type);
            if (prefab == null)
            {
                Debug.LogWarning($"[PageController] Missing prefab for {item.type}. Skipping.");
                continue;
            }

            GameObject go = Instantiate(prefab, content, worldPositionStays: false);
            go.name = $"{item.type}: {GetNiceName(item)}";

            ApplyBindings(go, item);
        }
    }

    private static string GetNiceName(PageItem item)
    {
        if (item.type == ItemType.Description)
            return "Description";
        return string.IsNullOrWhiteSpace(item.displayName) ? "Item" : item.displayName;
    }

    private GameObject GetPrefabFor(ItemType type)
    {
        switch (type)
        {
            case ItemType.Header: return headerPrefab;
            case ItemType.Audio: return audioPrefab;
            case ItemType.Description: return descriptionPrefab;
            default: return null;
        }
    }

    private void ApplyBindings(GameObject instanceRoot, PageItem item)
    {
        switch (item.type)
        {
            case ItemType.Header:
            {
                var tmp = GetTmpAtPath(instanceRoot.transform, HEADER_TMP_PATH);
                if (tmp != null) tmp.text = item.displayName ?? "";
                else Debug.LogWarning($"[PageController] Header TMP not found at '{HEADER_TMP_PATH}' on '{instanceRoot.name}'.");
                break;
            }

            case ItemType.Audio:
            {
                var tmp = GetTmpAtPath(instanceRoot.transform, AUDIO_TMP_PATH);
                if (tmp != null) tmp.text = item.displayName ?? "";
                else Debug.LogWarning($"[PageController] Audio TMP not found at '{AUDIO_TMP_PATH}' on '{instanceRoot.name}'.");

                var audioT = FindByPath(instanceRoot.transform, AUDIO_SOURCE_PATH);
                var src = audioT != null ? audioT.GetComponent<AudioSource>() : null;

                if (src == null)
                    Debug.LogWarning($"[PageController] AudioSource not found at '{AUDIO_SOURCE_PATH}' on '{instanceRoot.name}'.");
                else
                    src.clip = item.audioClip;

                break;
            }

            case ItemType.Description:
            {
                var tmp = GetTmpAtPath(instanceRoot.transform, DESCRIPTION_TMP_PATH);
                if (tmp != null) tmp.text = item.descriptionText ?? "";
                else Debug.LogWarning($"[PageController] Description TMP not found at '{DESCRIPTION_TMP_PATH}' on '{instanceRoot.name}'.");
                break;
            }
        }
    }

    private static TMP_Text GetTmpAtPath(Transform root, string path)
    {
        var t = FindByPath(root, path);
        return t != null ? t.GetComponent<TMP_Text>() : null;
    }

    private static Transform FindByPath(Transform root, string path)
    {
        if (root == null || string.IsNullOrWhiteSpace(path)) return null;

        var parts = path.Split('/');
        Transform current = root;

        for (int i = 0; i < parts.Length; i++)
        {
            string part = parts[i];
            if (string.IsNullOrWhiteSpace(part)) continue;

            current = current.Find(part);
            if (current == null) return null;
        }

        return current;
    }

    private void ClearContentChildrenRuntime()
    {
        for (int i = content.childCount - 1; i >= 0; i--)
            Destroy(content.GetChild(i).gameObject);
    }

#if UNITY_EDITOR
    [ContextMenu("Rebuild (Editor/Play)")]
    private void ContextRebuild()
    {
        RebuildEditorAware();
    }

    private void RebuildEditorAware()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            Rebuild();
            return;
        }

        if (content == null)
        {
            Debug.LogError("[PageController] Content is not assigned.");
            return;
        }

        if (clearExistingChildrenBeforeBuild)
        {
            for (int i = content.childCount - 1; i >= 0; i--)
                UnityEditor.Undo.DestroyObjectImmediate(content.GetChild(i).gameObject);
        }

        foreach (var item in items)
        {
            if (item == null) continue;

            GameObject prefab = GetPrefabFor(item.type);
            if (prefab == null) continue;

            GameObject go = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(prefab, content);
            UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Rebuild Page Content");
            go.transform.SetParent(content, false);

            go.name = $"{item.type}: {GetNiceName(item)}";

            ApplyBindings(go, item);

            UnityEditor.EditorUtility.SetDirty(go);
        }

        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(PageController))]
    private class PageControllerEditor : UnityEditor.Editor
    {
        private UnityEditorInternal.ReorderableList _list;

        private void OnEnable()
        {
            _list = new UnityEditorInternal.ReorderableList(
                serializedObject,
                serializedObject.FindProperty("items"),
                draggable: true,
                displayHeader: true,
                displayAddButton: false,
                displayRemoveButton: true
            );

            _list.drawHeaderCallback = rect =>
            {
                UnityEditor.EditorGUI.LabelField(rect, "Items");
            };

            _list.elementHeightCallback = index =>
            {
                var element = _list.serializedProperty.GetArrayElementAtIndex(index);
                var typeProp = element.FindPropertyRelative("type");
                var descProp = element.FindPropertyRelative("descriptionText");

                float lineH = UnityEditor.EditorGUIUtility.singleLineHeight;
                float pad = 8f;

                var type = (ItemType)typeProp.enumValueIndex;
                if (type == ItemType.Header) return (2 * lineH) + pad; // type + name
                if (type == ItemType.Audio) return (3 * lineH) + pad;  // type + name + clip

                // Description: type + big text area
                float descH = UnityEditor.EditorGUI.GetPropertyHeight(descProp, includeChildren: true);
                return lineH + descH + pad;
            };

            _list.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = _list.serializedProperty.GetArrayElementAtIndex(index);
                var typeProp = element.FindPropertyRelative("type");
                var nameProp = element.FindPropertyRelative("displayName");
                var clipProp = element.FindPropertyRelative("audioClip");
                var descProp = element.FindPropertyRelative("descriptionText");

                float lineH = UnityEditor.EditorGUIUtility.singleLineHeight;
                rect.y += 2f;

                var type = (ItemType)typeProp.enumValueIndex;

                var r0 = new Rect(rect.x, rect.y, rect.width, lineH);
                UnityEditor.EditorGUI.PropertyField(r0, typeProp, new GUIContent("Type"));

                if (type == ItemType.Header)
                {
                    var r1 = new Rect(rect.x, rect.y + lineH, rect.width, lineH);
                    UnityEditor.EditorGUI.PropertyField(r1, nameProp, new GUIContent("Name"));
                }
                else if (type == ItemType.Audio)
                {
                    var r1 = new Rect(rect.x, rect.y + lineH, rect.width, lineH);
                    var r2 = new Rect(rect.x, rect.y + 2 * lineH, rect.width, lineH);

                    UnityEditor.EditorGUI.PropertyField(r1, nameProp, new GUIContent("Name"));
                    UnityEditor.EditorGUI.PropertyField(r2, clipProp, new GUIContent("Audio Clip"));
                }
                else // Description
                {
                    float descH = UnityEditor.EditorGUI.GetPropertyHeight(descProp, includeChildren: true);
                    var r1 = new Rect(rect.x, rect.y + lineH, rect.width, descH);
                    UnityEditor.EditorGUI.PropertyField(r1, descProp, new GUIContent("Description"), includeChildren: true);
                }
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("content"));
            UnityEditor.EditorGUILayout.Space(4);

            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("headerPrefab"));
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("audioPrefab"));
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("descriptionPrefab"));
            UnityEditor.EditorGUILayout.Space(8);

            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("buildOnStart"));
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("clearExistingChildrenBeforeBuild"));
            UnityEditor.EditorGUILayout.Space(8);

            UnityEditor.EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Header")) AddItem(ItemType.Header);
            if (GUILayout.Button("Add Audio")) AddItem(ItemType.Audio);
            if (GUILayout.Button("Add Description")) AddItem(ItemType.Description);
            UnityEditor.EditorGUILayout.EndHorizontal();

            UnityEditor.EditorGUILayout.Space(6);
            _list.DoLayoutList();
            UnityEditor.EditorGUILayout.Space(8);

            var pc = (PageController)target;

            UnityEditor.EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Rebuild Now"))
            {
                var method = typeof(PageController).GetMethod(
                    "RebuildEditorAware",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic
                );
                method?.Invoke(pc, null);
            }

            if (GUILayout.Button("Clear Content"))
            {
                if (pc.content != null)
                {
                    for (int i = pc.content.childCount - 1; i >= 0; i--)
                        UnityEditor.Undo.DestroyObjectImmediate(pc.content.GetChild(i).gameObject);
                }
            }
            UnityEditor.EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }

        private void AddItem(ItemType type)
        {
            var itemsProp = serializedObject.FindProperty("items");
            int idx = itemsProp.arraySize;
            itemsProp.InsertArrayElementAtIndex(idx);

            var element = itemsProp.GetArrayElementAtIndex(idx);
            element.FindPropertyRelative("type").enumValueIndex = (int)type;
            element.FindPropertyRelative("displayName").stringValue = "";
            element.FindPropertyRelative("audioClip").objectReferenceValue = null;
            element.FindPropertyRelative("descriptionText").stringValue = "";

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
