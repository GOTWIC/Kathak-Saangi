using UnityEngine;
using System;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class gradient_generator : MonoBehaviour
{
    [Header("Texture Settings")]
    [Min(1)] public int width = 256;
    [Min(1)] public int height = 256;

    [Header("Gradient Colors (Top -> Bottom)")]
    public Color topColor = new Color(1f, 1f, 1f, 1f);
    public Color bottomColor = new Color(1f, 1f, 1f, 0f);

    [Header("Output (saved under Assets/)")]
    public string fileName = "vertical_gradient.png";

    public void Generate()
    {
        width = Mathf.Max(1, width);
        height = Mathf.Max(1, height);

        // Ensure filename + .png
        if (string.IsNullOrWhiteSpace(fileName))
            fileName = "vertical_gradient.png";

        if (!fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
            fileName += ".png";

        var tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        tex.wrapMode = TextureWrapMode.Clamp;

        // Texture2D pixel coords: y=0 is bottom, y=height-1 is top.
        // We want topColor at the top, bottomColor at the bottom.
        for (int y = 0; y < height; y++)
        {
            float y01 = (height == 1) ? 0f : (y / (height - 1f)); // 0 bottom -> 1 top
            float t = 1f - y01;                                   // 1 bottom -> 0 top

            Color rowColor = Color.Lerp(topColor, bottomColor, t);

            for (int x = 0; x < width; x++)
                tex.SetPixel(x, y, rowColor);
        }

        tex.Apply();

        byte[] png = tex.EncodeToPNG();
        string path = Path.Combine(Application.dataPath, fileName);
        File.WriteAllBytes(path, png);

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif

        Debug.Log("Saved gradient PNG to: " + path);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(gradient_generator))]
public class gradient_generator_editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        if (GUILayout.Button("Generate PNG"))
        {
            ((gradient_generator)target).Generate();
        }
    }
}
#endif
