using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class IntroSequence : MonoBehaviour, IPointerClickHandler
{
    [Header("Canvas Groups (need CanvasGroup components)")]
    public CanvasGroup splash_screen;   // First screen
    public CanvasGroup main_menu;   // Second screen

    [Header("Splash slide-out (left)")]
    public float velocity = 20f;     // Units per second
    public float threshold = -10f; // Disable splash when X reaches this (off-screen left)

    private bool hasStarted = false; // Prevent multiple runs

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
            main_menu.alpha = 1f;
            main_menu.interactable = false;
            main_menu.blocksRaycasts = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!hasStarted)
        {
            hasStarted = true;
            StartCoroutine(OnButtonPressed());
        }
    }

    private IEnumerator OnButtonPressed()
    {
        // Enable main menu immediately (it will be behind the splash)
        if (main_menu != null)
        {
            main_menu.gameObject.SetActive(true);
            main_menu.interactable = true;
            main_menu.blocksRaycasts = true;
        }

        // Keep splash in front: move it to last sibling so it draws on top
        if (splash_screen != null)
        {
            splash_screen.transform.SetAsLastSibling();
            splash_screen.interactable = false;
            splash_screen.blocksRaycasts = false;

            RectTransform rect = splash_screen.GetComponent<RectTransform>();
            if (rect != null)
            {
                while (rect.anchoredPosition.x > threshold)
                {
                    Vector2 pos = rect.anchoredPosition;
                    pos.x -= velocity * Time.deltaTime;
                    rect.anchoredPosition = pos;
                    yield return null;
                }
            }
            else
            {
                Transform t = splash_screen.transform;
                while (t.position.x > threshold)
                {
                    t.position += Vector3.left * (velocity * Time.deltaTime);
                    yield return null;
                }
            }
            

            splash_screen.gameObject.SetActive(false);
        }
    }
}
