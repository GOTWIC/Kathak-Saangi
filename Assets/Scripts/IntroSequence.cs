using UnityEngine;
using System.Collections;

public class IntroSequence : MonoBehaviour
{
    [Header("Canvas Groups (need CanvasGroup components)")]
    public CanvasGroup splash_screen;   // First screen
    public CanvasGroup main_menu;   // Second screen


    // list of sub canvases to disable when main menu is shown
    public CanvasGroup[] sub_canvases;

    [Header("Timings (seconds)")]
    public float INTO_SEC   = 2f;   // Time before starting fade-out of canvas 1
    public float FADE_OUT   = 1f;   // Duration of canvas 1 fade-out
    public float TRANSITION = 0.5f; // Delay between fade-out of canvas 1 and fade-in of canvas 2
    public float FADE_IN    = 1f;   // Duration of canvas 2 fade-in

    private void Start()
    {
        // Ensure initial state:
        // canvas1 visible, canvas2 hidden
        if (splash_screen != null)
        {
            splash_screen.gameObject.SetActive(true);
            splash_screen.alpha = 1f;
            splash_screen.interactable = true;
            splash_screen.blocksRaycasts = true;
        }

        if (main_menu != null)
        {
            main_menu.gameObject.SetActive(false);
            main_menu.alpha = 0f;
            main_menu.interactable = false;
            main_menu.blocksRaycasts = false;
        }

        foreach (CanvasGroup sub_canvas in sub_canvases)
        {
            sub_canvas.gameObject.SetActive(false);
        }

        StartCoroutine(RunIntro());
    }

    private IEnumerator RunIntro()
    {
        // Wait before starting fade-out of canvas 1
        if (INTO_SEC > 0f)
            yield return new WaitForSeconds(INTO_SEC);

        // Fade out canvas1
        if (splash_screen != null && FADE_OUT >= 0f)
        {
            yield return StartCoroutine(FadeCanvas(splash_screen, 1f, 0f, FADE_OUT));
            // Disable interaction and optionally deactivate
            splash_screen.interactable = false;
            splash_screen.blocksRaycasts = false;
            splash_screen.gameObject.SetActive(false);
        }

        // Transition delay before canvas2 fades in
        if (TRANSITION > 0f)
            yield return new WaitForSeconds(TRANSITION);

        // Fade in canvas2
        if (main_menu != null && FADE_IN >= 0f)
        {
            main_menu.gameObject.SetActive(true);
            yield return StartCoroutine(FadeCanvas(main_menu, 0f, 1f, FADE_IN));
            main_menu.interactable = true;
            main_menu.blocksRaycasts = true;
        }
    }

    private IEnumerator FadeCanvas(CanvasGroup canvasGroup, float from, float to, float duration)
    {
        if (canvasGroup == null)
            yield break;

        if (duration <= 0f)
        {
            canvasGroup.alpha = to;
            yield break;
        }

        float elapsed = 0f;
        canvasGroup.alpha = from;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            canvasGroup.alpha = Mathf.Lerp(from, to, t);
            yield return null;
        }

        canvasGroup.alpha = to;
    }
}
