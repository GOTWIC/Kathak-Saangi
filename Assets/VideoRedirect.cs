using UnityEngine;
using UnityEngine.EventSystems;

public class VideoRedirect : MonoBehaviour, IPointerClickHandler
{
    [Header("YouTube")]
    [SerializeField] private string youtubeVideoId = "dQw4w9WgXcQ";
    [SerializeField] private bool useShortLink = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        OpenVideo();
    }

    public void OpenVideo()
    {
        if (string.IsNullOrWhiteSpace(youtubeVideoId))
        {
            Debug.LogWarning($"{nameof(VideoRedirect)}: No YouTube video ID set on {gameObject.name}.");
            return;
        }

        string url = useShortLink
            ? $"https://youtu.be/{youtubeVideoId}"
            : $"https://www.youtube.com/watch?v={youtubeVideoId}";

        Application.OpenURL(url);
    }
}
