using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextLink : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text tmpText;

    private void Reset()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (tmpText == null) return;

        // If you have a Camera in Screen Space - Camera or World Space canvas, assign it.
        // For Screen Space - Overlay, you can pass null.
        var cam = eventData.pressEventCamera;

        int linkIndex = TMP_TextUtilities.FindIntersectingLink(tmpText, eventData.position, cam);
        if (linkIndex == -1) return;

        TMP_LinkInfo linkInfo = tmpText.textInfo.linkInfo[linkIndex];
        string linkId = linkInfo.GetLinkID(); // whatever you put inside <link="...">

        Application.OpenURL(linkId);
    }
}