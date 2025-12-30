using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PageController : MonoBehaviour
{
    public enum ItemType
    {
        Header,
        Audio,
        Description,
        Spacer
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

    // ---------------------------
    // Scene / Prefabs
    // ---------------------------
    [Header("Scene References")]
    [SerializeField] private Transform content;

    [Header("Prefabs")]
    [SerializeField] private GameObject headerPrefab;
    [SerializeField] private GameObject audioPrefab;
    [SerializeField] private GameObject descriptionPrefab;
    [SerializeField] private GameObject spacerPrefab;

    // ---------------------------
    // Page-level fields
    // ---------------------------
    [Header("Page Settings")]
    [SerializeField] private Color page_color = Color.white;
    [SerializeField] private Sprite page_gradient;
    [SerializeField] private string page_name;
    [SerializeField] private Sprite page_image;

    // ---------------------------
    // Data
    // ---------------------------
    [Header("List Contents")]
    [SerializeField] private List<PageItem> items = new List<PageItem>();

    [Header("Build Options")]
    [SerializeField] private bool buildOnStart = true;
    [SerializeField] private bool clearExistingChildrenBeforeBuild = true;

    // ---------------------------
    // Hierarchy Paths (relative to SpotifyTemplate root = this.transform)
    // ---------------------------
    // page_color -> SpotifyTemplate/CarouselRoot/background/color (Image.color)
    private const string BG_COLOR_IMAGE_PATH = "CarouselRoot/background/color";

    // page_gradient -> SpotifyTemplate/CarouselRoot/background/gradient (Image.sprite)
    private const string BG_GRADIENT_IMAGE_PATH = "CarouselRoot/background/gradient";

    // page_name -> SpotifyTemplate/CarouselRoot/background_info/module_name_foreground/module_name (TMP_Text.text)
    private const string PAGE_NAME_TMP_PATH = "CarouselRoot/background_info/module_name_background/module_name_foreground/module_name";

    // page_image -> SpotifyTemplate/CarouselRoot/background_info/module_img (Image.sprite)
    private const string PAGE_IMAGE_PATH = "CarouselRoot/background_info/module_img/img";

    // Item bindings (relative to instantiated prefab root)
    private const string HEADER_TMP_PATH = "text";                // header_container -> text -> TMP
    private const string AUDIO_TMP_PATH = "audio_element/text";   // audio_component -> audio_element -> text -> TMP
    private const string AUDIO_SOURCE_PATH = "audio_element";     // audio_component -> audio_element -> AudioSource
    private const string DESCRIPTION_TMP_PATH = "description_element/text";           // description_container -> text -> TMP

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
            Debug.LogError("[PageController] Content is not assigned.");
            return;
        }

        // Apply page settings first (so the UI matches the data even if list is empty)
        ApplyPageBindingsRuntime();

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

            ApplyItemBindings(go, item);
        }
    }

    public void ClearAll()
    {
        if (content != null)
            ClearContentChildrenRuntime();

        ClearPageBindingsRuntime();
    }

    // ============================================================
    // Page bindings
    // ============================================================
    private void ApplyPageBindingsRuntime()
    {
        // Background color image
        var bgColorImg = GetImageAtRootPath(BG_COLOR_IMAGE_PATH);
        if (bgColorImg != null)
            bgColorImg.color = page_color;
        else
            Debug.LogWarning($"[PageController] Background color Image not found at '{BG_COLOR_IMAGE_PATH}'.");

        // Background gradient sprite
        var bgGradientImg = GetImageAtRootPath(BG_GRADIENT_IMAGE_PATH);
        if (bgGradientImg != null)
            bgGradientImg.sprite = page_gradient;
        else
            Debug.LogWarning($"[PageController] Background gradient Image not found at '{BG_GRADIENT_IMAGE_PATH}'.");

        // Page name TMP
        var nameTmp = GetTmpAtRootPath(PAGE_NAME_TMP_PATH);
        if (nameTmp != null)
            nameTmp.text = page_name ?? "";
        else
            Debug.LogWarning($"[PageController] Page name TMP_Text not found at '{PAGE_NAME_TMP_PATH}'.");

        // Page cover image sprite
        var coverImg = GetImageAtRootPath(PAGE_IMAGE_PATH);
        if (coverImg != null)
        {
            coverImg.sprite = page_image;
            coverImg.preserveAspect = true;
        }
        else
            Debug.LogWarning($"[PageController] Page image Image not found at '{PAGE_IMAGE_PATH}'.");
    }

    private void ClearPageBindingsRuntime()
    {
        // "Clear" means reset the driven UI fields to blank/neutral.
        var bgColorImg = GetImageAtRootPath(BG_COLOR_IMAGE_PATH);
        if (bgColorImg != null)
            bgColorImg.color = Color.white;

        var bgGradientImg = GetImageAtRootPath(BG_GRADIENT_IMAGE_PATH);
        if (bgGradientImg != null)
            bgGradientImg.sprite = null;

        var nameTmp = GetTmpAtRootPath(PAGE_NAME_TMP_PATH);
        if (nameTmp != null)
            nameTmp.text = "";

        var coverImg = GetImageAtRootPath(PAGE_IMAGE_PATH);
        if (coverImg != null)
            coverImg.sprite = null;
    }

    // ============================================================
    // Item bindings
    // ============================================================
    private static string GetNiceName(PageItem item)
    {
        if (item.type == ItemType.Description)
            return "Description";
        if (item.type == ItemType.Spacer)
            return "Spacer";
        return string.IsNullOrWhiteSpace(item.displayName) ? "Item" : item.displayName;
    }

    private GameObject GetPrefabFor(ItemType type)
    {
        switch (type)
        {
            case ItemType.Header: return headerPrefab;
            case ItemType.Audio: return audioPrefab;
            case ItemType.Description: return descriptionPrefab;
            case ItemType.Spacer: return spacerPrefab;
            default: return null;
        }
    }

    private void ApplyItemBindings(GameObject instanceRoot, PageItem item)
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

            case ItemType.Spacer:
            {
                // Spacer doesn't need any bindings, just instantiate the prefab
                break;
            }
        }
    }

    // ============================================================
    // Utilities
    // ============================================================
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

    private Image GetImageAtRootPath(string pathFromRoot)
    {
        var t = FindByPath(transform, pathFromRoot);
        return t != null ? t.GetComponent<Image>() : null;
    }

    private TMP_Text GetTmpAtRootPath(string pathFromRoot)
    {
        var t = FindByPath(transform, pathFromRoot);
        return t != null ? t.GetComponent<TMP_Text>() : null;
    }

#if UNITY_EDITOR
    // ============================================================
    // Editor-aware rebuild/clear (supports edit mode + Undo)
    // ============================================================
    [ContextMenu("Rebuild (Editor/Play)")]
    private void ContextRebuild() => RebuildEditorAware();

    [ContextMenu("Clear All (Editor/Play)")]
    private void ContextClear() => ClearAllEditorAware();

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

        ApplyPageBindingsEditorUndo();

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
            ApplyItemBindings(go, item);

            UnityEditor.EditorUtility.SetDirty(go);
        }

        UnityEditor.EditorUtility.SetDirty(this);
    }

    private void ClearAllEditorAware()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            ClearAll();
            return;
        }

        if (content != null)
        {
            for (int i = content.childCount - 1; i >= 0; i--)
                UnityEditor.Undo.DestroyObjectImmediate(content.GetChild(i).gameObject);
        }

        ClearPageBindingsEditorUndo();

        UnityEditor.EditorUtility.SetDirty(this);
    }

    private void ApplyPageBindingsEditorUndo()
    {
        var bgColorImg = GetImageAtRootPath(BG_COLOR_IMAGE_PATH);
        if (bgColorImg != null)
        {
            UnityEditor.Undo.RecordObject(bgColorImg, "Set Page Color");
            bgColorImg.color = page_color;
            UnityEditor.EditorUtility.SetDirty(bgColorImg);
        }

        var bgGradientImg = GetImageAtRootPath(BG_GRADIENT_IMAGE_PATH);
        if (bgGradientImg != null)
        {
            UnityEditor.Undo.RecordObject(bgGradientImg, "Set Page Gradient");
            bgGradientImg.sprite = page_gradient;
            UnityEditor.EditorUtility.SetDirty(bgGradientImg);
        }

        var nameTmp = GetTmpAtRootPath(PAGE_NAME_TMP_PATH);
        if (nameTmp != null)
        {
            UnityEditor.Undo.RecordObject(nameTmp, "Set Page Name");
            nameTmp.text = page_name ?? "";
            UnityEditor.EditorUtility.SetDirty(nameTmp);
        }

        var coverImg = GetImageAtRootPath(PAGE_IMAGE_PATH);
        if (coverImg != null)
        {
            UnityEditor.Undo.RecordObject(coverImg, "Set Page Image");
            coverImg.sprite = page_image;
            coverImg.preserveAspect = true;
            UnityEditor.EditorUtility.SetDirty(coverImg);
        }
    }

    private void ClearPageBindingsEditorUndo()
    {
        var bgColorImg = GetImageAtRootPath(BG_COLOR_IMAGE_PATH);
        if (bgColorImg != null)
        {
            UnityEditor.Undo.RecordObject(bgColorImg, "Clear Page Color");
            bgColorImg.color = Color.white;
            UnityEditor.EditorUtility.SetDirty(bgColorImg);
        }

        var bgGradientImg = GetImageAtRootPath(BG_GRADIENT_IMAGE_PATH);
        if (bgGradientImg != null)
        {
            UnityEditor.Undo.RecordObject(bgGradientImg, "Clear Page Gradient");
            bgGradientImg.sprite = null;
            UnityEditor.EditorUtility.SetDirty(bgGradientImg);
        }

        var nameTmp = GetTmpAtRootPath(PAGE_NAME_TMP_PATH);
        if (nameTmp != null)
        {
            UnityEditor.Undo.RecordObject(nameTmp, "Clear Page Name");
            nameTmp.text = "";
            UnityEditor.EditorUtility.SetDirty(nameTmp);
        }

        var coverImg = GetImageAtRootPath(PAGE_IMAGE_PATH);
        if (coverImg != null)
        {
            UnityEditor.Undo.RecordObject(coverImg, "Clear Page Image");
            coverImg.sprite = null;
            UnityEditor.EditorUtility.SetDirty(coverImg);
        }
    }
#endif

#if UNITY_EDITOR
    // ============================================================
    // Custom Inspector with buttons
    // ============================================================
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

            _list.drawHeaderCallback = rect => UnityEditor.EditorGUI.LabelField(rect, "Items");

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
                if (type == ItemType.Spacer) return lineH + pad;       // type only

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
                else if (type == ItemType.Spacer)
                {
                    // Spacer doesn't need any additional fields
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

            // Scene refs / prefabs
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("content"));
            UnityEditor.EditorGUILayout.Space(4);
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("headerPrefab"));
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("audioPrefab"));
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("descriptionPrefab"));
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("spacerPrefab"));

            UnityEditor.EditorGUILayout.Space(10);

            // Page settings
            UnityEditor.EditorGUILayout.LabelField("Page Settings", UnityEditor.EditorStyles.boldLabel);
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("page_color"), new GUIContent("Page Color"));
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("page_gradient"), new GUIContent("Page Gradient"));
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("page_name"), new GUIContent("Page Name"));
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("page_image"), new GUIContent("Page Image"));

            UnityEditor.EditorGUILayout.Space(10);

            // Build options
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("buildOnStart"));
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("clearExistingChildrenBeforeBuild"));

            UnityEditor.EditorGUILayout.Space(10);

            // Add buttons
            UnityEditor.EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Header")) AddItem(ItemType.Header);
            if (GUILayout.Button("Add Audio")) AddItem(ItemType.Audio);
            if (GUILayout.Button("Add Description")) AddItem(ItemType.Description);
            if (GUILayout.Button("Add Spacer")) AddItem(ItemType.Spacer);
            UnityEditor.EditorGUILayout.EndHorizontal();

            UnityEditor.EditorGUILayout.Space(6);

            _list.DoLayoutList();

            UnityEditor.EditorGUILayout.Space(10);

            // Rebuild / Clear buttons
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
                var method = typeof(PageController).GetMethod(
                    "ClearAllEditorAware",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic
                );
                method?.Invoke(pc, null);
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
