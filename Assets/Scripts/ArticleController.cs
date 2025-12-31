using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArticleController : MonoBehaviour
{
    public enum ItemType
    {
        Heading,
        Subheading,
        Paragraph,
        Divider,
        Spacer
    }

    [Serializable]
    public class ArticleItem
    {
        public ItemType type = ItemType.Heading;

        // Used for Heading/Subheading/Paragraph
        [TextArea(1, 10)]
        public string text;

        // Only used when type == Heading (default: 140) or Subheading (default: 90)
        public float fontSize = 140f;

        // Only used when type == Spacer
        public float height = 100f;
    }

    // ---------------------------
    // Scene / Prefabs
    // ---------------------------
    [Header("Scene References")]
    [SerializeField] private Transform content;

    [Header("Prefabs")]
    [SerializeField] private GameObject headingPrefab;
    [SerializeField] private GameObject subheadingPrefab;
    [SerializeField] private GameObject paragraphPrefab;
    [SerializeField] private GameObject dividerPrefab;
    [SerializeField] private GameObject spacerPrefab;

    // ---------------------------
    // Data
    // ---------------------------
    [Header("List Contents")]
    [SerializeField] private List<ArticleItem> items = new List<ArticleItem>();

    [Header("Build Options")]
    [SerializeField] private bool buildOnStart = true;
    [SerializeField] private bool clearExistingChildrenBeforeBuild = true;

    private void Start()
    {
        if (buildOnStart)
            Rebuild();
    }

    // ============================================================
    // Public API
    // ============================================================

    public void Rebuild()
    {
        if (content == null)
        {
            Debug.LogError("[ArticleController] Content is not assigned.");
            return;
        }

        if (clearExistingChildrenBeforeBuild)
            ClearContentChildrenRuntime();

        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i];
            if (item == null) continue;

            var prefab = GetPrefabFor(item.type);
            if (prefab == null)
            {
                Debug.LogWarning($"[ArticleController] Missing prefab for {item.type}. Skipping.");
                continue;
            }

            var go = Instantiate(prefab, content, worldPositionStays: false);
            go.name = $"{item.type}: {GetNiceName(item, i)}";
            ApplyItemBindings(go, item);
        }
    }

    public void ClearContent()
    {
        if (content == null) return;
        ClearContentChildrenRuntime();
    }

    public void ClearAll()
    {
        ClearContent();
        items.Clear();
    }

    // ============================================================
    // Optional: Runtime UI Buttons can call these
    // (adds a new item + spawns it immediately)
    // ============================================================

    public void AddHeadingRuntime(string text = "Heading")
    {
        AddItemRuntime(ItemType.Heading, text, spawnImmediately: true);
    }

    public void AddSubheadingRuntime(string text = "Subheading")
    {
        AddItemRuntime(ItemType.Subheading, text, spawnImmediately: true);
    }

    public void AddParagraphRuntime(string text = "Paragraph...")
    {
        AddItemRuntime(ItemType.Paragraph, text, spawnImmediately: true);
    }

    public void AddDividerRuntime()
    {
        AddItemRuntime(ItemType.Divider, "", spawnImmediately: true);
    }

    public void AddSpacerRuntime()
    {
        AddItemRuntime(ItemType.Spacer, "", spawnImmediately: true);
    }

    private void AddItemRuntime(ItemType type, string text, bool spawnImmediately)
    {
        var item = new ArticleItem { type = type, text = text ?? "" };
        if (type == ItemType.Heading)
            item.fontSize = 140f;
        else if (type == ItemType.Subheading)
            item.fontSize = 90f;
        else if (type == ItemType.Spacer)
            item.height = 100f;
        items.Add(item);

        if (!spawnImmediately) return;

        if (content == null)
        {
            Debug.LogError("[ArticleController] Content is not assigned (cannot spawn). Item was added to list only.");
            return;
        }

        var prefab = GetPrefabFor(type);
        if (prefab == null)
        {
            Debug.LogWarning($"[ArticleController] Missing prefab for {type}. Item was added to list only.");
            return;
        }

        var go = Instantiate(prefab, content, worldPositionStays: false);
        go.name = $"{type}: {GetNiceName(item, items.Count - 1)}";
        ApplyItemBindings(go, item);
    }

    // ============================================================
    // Internals
    // ============================================================

    private GameObject GetPrefabFor(ItemType type)
    {
        switch (type)
        {
            case ItemType.Heading: return headingPrefab;
            case ItemType.Subheading: return subheadingPrefab;
            case ItemType.Paragraph: return paragraphPrefab;
            case ItemType.Divider: return dividerPrefab;
            case ItemType.Spacer: return spacerPrefab;
            default: return null;
        }
    }

    private void ApplyItemBindings(GameObject instanceRoot, ArticleItem item)
    {
        switch (item.type)
        {
            case ItemType.Heading:
                SetTmpText(instanceRoot.transform, "text", item.text, item.fontSize, "Heading");
                break;

            case ItemType.Subheading:
                SetTmpText(instanceRoot.transform, "text", item.text, item.fontSize, "Subheading");
                break;

            case ItemType.Paragraph:
                SetTmpText(instanceRoot.transform, "text", item.text, null, "Paragraph");
                break;

            case ItemType.Divider:
                // no bindings
                break;

            case ItemType.Spacer:
                SetSpacerHeight(instanceRoot, item.height);
                break;
        }
    }

    private void SetTmpText(Transform root, string path, string value, float? fontSize, string label)
    {
        var tmp = GetTmpAtPath(root, path);
        if (tmp != null)
        {
            tmp.text = value ?? "";
            if (fontSize.HasValue)
                tmp.fontSize = fontSize.Value;
        }
        else
        {
            Debug.LogWarning($"[ArticleController] {label} TMP_Text not found at '{path}' on '{root.name}'.");
        }
    }

    private void SetSpacerHeight(GameObject instanceRoot, float height)
    {
        var rectTransform = instanceRoot.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            var sizeDelta = rectTransform.sizeDelta;
            sizeDelta.y = height;
            rectTransform.sizeDelta = sizeDelta;
        }
        else
        {
            Debug.LogWarning($"[ArticleController] RectTransform not found on '{instanceRoot.name}'. Cannot set spacer height.");
        }
    }

    private static string GetNiceName(ArticleItem item, int index)
    {
        if (item.type == ItemType.Divider) return $"Divider {index}";
        if (item.type == ItemType.Spacer) return $"Spacer {index}";
        return string.IsNullOrWhiteSpace(item.text) ? $"{item.type} {index}" : item.text.Trim();
    }

    private void ClearContentChildrenRuntime()
    {
        for (int i = content.childCount - 1; i >= 0; i--)
            Destroy(content.GetChild(i).gameObject);
    }

    private static Transform FindByPath(Transform root, string path)
    {
        if (root == null || string.IsNullOrWhiteSpace(path)) return null;

        var parts = path.Split('/');
        Transform current = root;

        for (int i = 0; i < parts.Length; i++)
        {
            var part = parts[i];
            if (string.IsNullOrWhiteSpace(part)) continue;

            current = current.Find(part);
            if (current == null) return null;
        }

        return current;
    }

    private static TMP_Text GetTmpAtPath(Transform root, string path)
    {
        var t = FindByPath(root, path);
        return t != null ? t.GetComponent<TMP_Text>() : null;
    }

#if UNITY_EDITOR
    // ============================================================
    // Editor-aware rebuild/clear (supports edit mode + Undo)
    // ============================================================

    [ContextMenu("Rebuild (Editor/Play)")]
    private void ContextRebuild() => RebuildEditorAware();

    [ContextMenu("Clear Content (Editor/Play)")]
    private void ContextClearContent() => ClearContentEditorAware();

    [ContextMenu("Clear All (Editor/Play)")]
    private void ContextClearAll() => ClearAllEditorAware();

    private void RebuildEditorAware()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            Rebuild();
            return;
        }

        if (content == null)
        {
            Debug.LogError("[ArticleController] Content is not assigned.");
            return;
        }

        if (clearExistingChildrenBeforeBuild)
        {
            for (int i = content.childCount - 1; i >= 0; i--)
                UnityEditor.Undo.DestroyObjectImmediate(content.GetChild(i).gameObject);
        }

        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i];
            if (item == null) continue;

            var prefab = GetPrefabFor(item.type);
            if (prefab == null) continue;

            GameObject go = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(prefab, content);
            UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Rebuild Article Content");
            go.transform.SetParent(content, false);

            go.name = $"{item.type}: {GetNiceName(item, i)}";
            
            // Record undo for components before applying bindings
            if (item.type == ItemType.Spacer)
            {
                var rectTransform = go.GetComponent<RectTransform>();
                if (rectTransform != null)
                    UnityEditor.Undo.RecordObject(rectTransform, "Set Spacer Height");
            }
            else if (item.type == ItemType.Heading || item.type == ItemType.Subheading)
            {
                var tmp = go.GetComponent<TMP_Text>();
                if (tmp != null)
                    UnityEditor.Undo.RecordObject(tmp, "Set Text and Font Size");
            }
            
            ApplyItemBindings(go, item);

            if (item.type == ItemType.Spacer)
            {
                var rectTransform = go.GetComponent<RectTransform>();
                if (rectTransform != null)
                    UnityEditor.EditorUtility.SetDirty(rectTransform);
            }
            else if (item.type == ItemType.Heading || item.type == ItemType.Subheading)
            {
                var tmp = go.GetComponent<TMP_Text>();
                if (tmp != null)
                    UnityEditor.EditorUtility.SetDirty(tmp);
            }

            UnityEditor.EditorUtility.SetDirty(go);
        }

        UnityEditor.EditorUtility.SetDirty(this);
    }

    private void ClearContentEditorAware()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            ClearContent();
            return;
        }

        if (content == null) return;

        for (int i = content.childCount - 1; i >= 0; i--)
            UnityEditor.Undo.DestroyObjectImmediate(content.GetChild(i).gameObject);

        UnityEditor.EditorUtility.SetDirty(this);
    }

    private void ClearAllEditorAware()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            ClearAll();
            return;
        }

        ClearContentEditorAware();
        UnityEditor.Undo.RecordObject(this, "Clear Article Items");
        items.Clear();
        UnityEditor.EditorUtility.SetDirty(this);
    }

    // ============================================================
    // Custom Inspector (ReorderableList + Add buttons)
    // ============================================================

    [UnityEditor.CustomEditor(typeof(ArticleController))]
    private class ArticleControllerEditor : UnityEditor.Editor
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

            _list.drawHeaderCallback = rect => UnityEditor.EditorGUI.LabelField(rect, "Items");

            _list.elementHeightCallback = index =>
            {
                var element = _list.serializedProperty.GetArrayElementAtIndex(index);
                var typeProp = element.FindPropertyRelative("type");
                var textProp = element.FindPropertyRelative("text");

                float lineH = UnityEditor.EditorGUIUtility.singleLineHeight;
                float pad = 8f;

                var type = (ItemType)typeProp.enumValueIndex;

                if (type == ItemType.Divider)
                    return lineH + pad; // just the type

                if (type == ItemType.Spacer)
                {
                    var heightProp = element.FindPropertyRelative("height");
                    return (2 * lineH) + pad; // type + height
                }

                float textH = UnityEditor.EditorGUI.GetPropertyHeight(textProp, includeChildren: true);

                if (type == ItemType.Heading || type == ItemType.Subheading)
                {
                    return (3 * lineH) + textH + pad; // type + fontSize + text
                }

                return lineH + textH + pad;
            };

            _list.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = _list.serializedProperty.GetArrayElementAtIndex(index);
                var typeProp = element.FindPropertyRelative("type");
                var textProp = element.FindPropertyRelative("text");
                var fontSizeProp = element.FindPropertyRelative("fontSize");
                var heightProp = element.FindPropertyRelative("height");

                float lineH = UnityEditor.EditorGUIUtility.singleLineHeight;
                rect.y += 2f;

                var type = (ItemType)typeProp.enumValueIndex;

                var r0 = new Rect(rect.x, rect.y, rect.width, lineH);
                UnityEditor.EditorGUI.PropertyField(r0, typeProp, new GUIContent("Type"));

                if (type == ItemType.Spacer)
                {
                    var r1 = new Rect(rect.x, rect.y + lineH, rect.width, lineH);
                    UnityEditor.EditorGUI.PropertyField(r1, heightProp, new GUIContent("Height"));
                }
                else if (type == ItemType.Heading || type == ItemType.Subheading)
                {
                    var r1 = new Rect(rect.x, rect.y + lineH, rect.width, lineH);
                    UnityEditor.EditorGUI.PropertyField(r1, fontSizeProp, new GUIContent("Font Size"));
                    
                    float textH = UnityEditor.EditorGUI.GetPropertyHeight(textProp, includeChildren: true);
                    var r2 = new Rect(rect.x, rect.y + 2 * lineH, rect.width, textH);
                    UnityEditor.EditorGUI.PropertyField(r2, textProp, new GUIContent("Text"), includeChildren: true);
                }
                else if (type != ItemType.Divider)
                {
                    float textH = UnityEditor.EditorGUI.GetPropertyHeight(textProp, includeChildren: true);
                    var r1 = new Rect(rect.x, rect.y + lineH, rect.width, textH);
                    UnityEditor.EditorGUI.PropertyField(r1, textProp, new GUIContent("Text"), includeChildren: true);
                }
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            UnityEditor.EditorGUILayout.LabelField("Scene References", UnityEditor.EditorStyles.boldLabel);
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("content"));

            UnityEditor.EditorGUILayout.Space(8);

            UnityEditor.EditorGUILayout.LabelField("Prefabs", UnityEditor.EditorStyles.boldLabel);
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("headingPrefab"));
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("subheadingPrefab"));
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("paragraphPrefab"));
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("dividerPrefab"));
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("spacerPrefab"));

            UnityEditor.EditorGUILayout.Space(8);

            UnityEditor.EditorGUILayout.LabelField("Build Options", UnityEditor.EditorStyles.boldLabel);
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("buildOnStart"));
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("clearExistingChildrenBeforeBuild"));

            UnityEditor.EditorGUILayout.Space(10);

            _list.DoLayoutList();

            UnityEditor.EditorGUILayout.Space(6);

            // Add buttons
            UnityEditor.EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Heading")) AddItem(ItemType.Heading);
            if (GUILayout.Button("Subheading")) AddItem(ItemType.Subheading);
            if (GUILayout.Button("Paragraph")) AddItem(ItemType.Paragraph);
            if (GUILayout.Button("Divider")) AddItem(ItemType.Divider);
            if (GUILayout.Button("Spacer")) AddItem(ItemType.Spacer);
            UnityEditor.EditorGUILayout.EndHorizontal();

            UnityEditor.EditorGUILayout.Space(10);

            // Rebuild / Clear buttons
            var ac = (ArticleController)target;

            UnityEditor.EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Rebuild Now"))
            {
                var method = typeof(ArticleController).GetMethod(
                    "RebuildEditorAware",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic
                );
                method?.Invoke(ac, null);
            }

            if (GUILayout.Button("Clear Content"))
            {
                var method = typeof(ArticleController).GetMethod(
                    "ClearContentEditorAware",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic
                );
                method?.Invoke(ac, null);
            }

            if (GUILayout.Button("Clear All"))
            {
                var method = typeof(ArticleController).GetMethod(
                    "ClearAllEditorAware",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic
                );
                method?.Invoke(ac, null);
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

            var textProp = element.FindPropertyRelative("text");
            if (type == ItemType.Heading) textProp.stringValue = "Heading";
            else if (type == ItemType.Subheading) textProp.stringValue = "Subheading";
            else if (type == ItemType.Paragraph) textProp.stringValue = "Paragraph...";
            else textProp.stringValue = "";

            if (type == ItemType.Heading)
            {
                var fontSizeProp = element.FindPropertyRelative("fontSize");
                fontSizeProp.floatValue = 140f;
            }
            else if (type == ItemType.Subheading)
            {
                var fontSizeProp = element.FindPropertyRelative("fontSize");
                fontSizeProp.floatValue = 90f;
            }
            else if (type == ItemType.Spacer)
            {
                var heightProp = element.FindPropertyRelative("height");
                heightProp.floatValue = 100f;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
