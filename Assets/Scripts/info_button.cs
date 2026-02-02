using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class info_button : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite info;
    [SerializeField] private Sprite close;
    [SerializeField] private GameObject targetObject;
    
    private Image imageComponent;
    private Image targetImage;
    private bool isInfoState = true;
    private bool isTransitioning = false;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        
        if (imageComponent != null && info != null)
        {
            imageComponent.sprite = info;
        }

        // Initialize target object
        if (targetObject != null)
        {
            targetImage = targetObject.GetComponent<Image>();
            targetObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isTransitioning)
        {
            StartCoroutine(ToggleSprite());
        }
    }

    private IEnumerator ToggleSprite()
    {
        isTransitioning = true;

        const float duration = 0.1f;

        if (isInfoState)
        {
            // Opening: enable target at alpha 0, then fade button out AND target in at the same time
            if (targetObject != null && targetImage != null)
            {
                targetObject.SetActive(true);
                Color tc = targetImage.color;
                tc.a = 0f;
                targetImage.color = tc;
            }
            yield return StartCoroutine(FadeButtonAndTarget(1f, 0f, 0f, 1f, duration));

            imageComponent.sprite = close;
        }
        else
        {
            // Closing: fade button out AND target out at the same time
            yield return StartCoroutine(FadeButtonAndTarget(1f, 0f, 1f, 0f, duration));

            imageComponent.sprite = info;
            if (targetObject != null)
            {
                targetObject.SetActive(false);
            }
        }
        isInfoState = !isInfoState;

        // Fade in button
        yield return StartCoroutine(FadeImage(0f, 1f, duration));

        isTransitioning = false;
    }

    /// <summary>
    /// Fades the button and target object in parallel over the same duration.
    /// </summary>
    private IEnumerator FadeButtonAndTarget(float buttonStart, float buttonEnd, float targetStart, float targetEnd, float duration)
    {
        float elapsed = 0f;
        Color buttonColor = imageComponent.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            buttonColor.a = Mathf.Lerp(buttonStart, buttonEnd, t);
            imageComponent.color = buttonColor;

            if (targetImage != null)
            {
                Color targetColor = targetImage.color;
                targetColor.a = Mathf.Lerp(targetStart, targetEnd, t);
                targetImage.color = targetColor;
            }

            yield return null;
        }

        buttonColor.a = buttonEnd;
        imageComponent.color = buttonColor;
        if (targetImage != null)
        {
            Color targetColor = targetImage.color;
            targetColor.a = targetEnd;
            targetImage.color = targetColor;
        }
    }

    private IEnumerator FadeImage(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = imageComponent.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            color.a = alpha;
            imageComponent.color = color;
            yield return null;
        }

        // Ensure final alpha is set
        color.a = endAlpha;
        imageComponent.color = color;
    }

    private IEnumerator FadeTargetObject(float startAlpha, float endAlpha, float duration)
    {
        if (targetImage == null) yield break;

        float elapsed = 0f;
        Color color = targetImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            color.a = alpha;
            targetImage.color = color;
            yield return null;
        }

        // Ensure final alpha is set
        color.a = endAlpha;
        targetImage.color = color;
    }
}
