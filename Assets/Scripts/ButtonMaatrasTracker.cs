using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonMaatrasTracker : MonoBehaviour
{
    [Header("Assign exactly 6 buttons here (drag from Hierarchy).")]
    [SerializeField] private List<Button> buttons = new List<Button>(6);

    [Header("Updated automatically from selected button text.")]
    public int maatras;

    void Update()
    {
        maatras = GetMaatrasFromSelection();
    }

    private int GetMaatrasFromSelection()
    {
        int best = 0;

        // 1) Most common case: only one selected via EventSystem
        GameObject selected = EventSystem.current != null ? EventSystem.current.currentSelectedGameObject : null;

        if (selected != null)
        {
            // Check if the selected object (or its parent) is one of our tracked buttons
            // This handles both direct button selection and child element (like text) selection
            Button selectedButton = selected.GetComponent<Button>() ?? selected.GetComponentInParent<Button>();
            
            if (selectedButton != null && buttons.Contains(selectedButton))
            {
                return ReadNumberFromButton(selectedButton);
            }

            // Also check if selected object is a child of any of our buttons
            for (int i = 0; i < buttons.Count; i++)
            {
                var b = buttons[i];
                if (b == null) continue;

                // Check if selected is the button itself or a child of it
                if (selected == b.gameObject || selected.transform.IsChildOf(b.transform))
                {
                    return ReadNumberFromButton(b);
                }
            }
        }

        return best; // 0 if none
    }

    private int ReadNumberFromButton(Button button)
    {
        if (button == null) return 0;

        // Try TMP first if available, then legacy Text.
        // Text is guaranteed numeric per your note, so int.Parse is fine.
        TMP_Text tmp = button.GetComponentInChildren<TMP_Text>(true);
        if (tmp != null)
            return int.Parse(tmp.text);

        Text uiText = button.GetComponentInChildren<Text>(true);
        if (uiText != null)
            return int.Parse(uiText.text);

        // If no text component found, treat as 0
        return 0;
    }
}
