using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(ScrollRect))]
public class CarouselController : MonoBehaviour
{
    [System.Serializable]
    public class CardContent
    {
        public string name;
        [TextArea] public string description;
        public Sprite backgroundImage;
        public GameObject linkedCanvas;  // The canvas to open when this card is selected
    }

    [Header("References")]
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;      // The Content object under Viewport
    [SerializeField] private GameObject cardPrefab;      // Your card prefab
    [SerializeField] private GameObject mainCanvas;      // The main menu canvas to disable when a card is selected

    [Header("Cards")]
    [SerializeField] private List<CardContent> cards = new List<CardContent>(); // Fill in Inspector
    [SerializeField] private int initialCards = 3;       // How many to spawn at start
    [SerializeField] private float preloadPixels = 200f; // How close to the right edge before spawning next

    private int spawnedCount = 0;

    private void Awake()
    {
        if (!scrollRect)
            scrollRect = GetComponent<ScrollRect>();

        if (!content)
            content = scrollRect.content;

        // Make sure ScrollRect is configured the way we expect
        scrollRect.horizontal = true;
        scrollRect.vertical = false;
    }

    private void Start()
    {
        // Spawn the first few cards so you can actually scroll a bit
        int toSpawn = Mathf.Min(initialCards, cards.Count);
        for (int i = 0; i < toSpawn; i++)
        {
            SpawnNextCard();
        }

        // Start at the very first card
        scrollRect.horizontalNormalizedPosition = 0f;
    }

    private void Update()
    {
        // Nothing to do if we've already spawned all cards
        if (spawnedCount >= cards.Count)
            return;

        float contentWidth = content.rect.width;
        float viewportWidth = scrollRect.viewport.rect.width;

        // If content isn't wider than viewport yet, there is nothing to preload
        if (contentWidth <= viewportWidth)
            return;

        // How far are we scrolled in pixels?
        float maxOffset = contentWidth - viewportWidth;
        float offset = scrollRect.horizontalNormalizedPosition * maxOffset;

        float viewportRight = offset + viewportWidth;          // right edge (in content space)
        float distanceToRightEdge = contentWidth - viewportRight;

        // If we are within "preloadPixels" of the right edge, spawn the next card
        if (distanceToRightEdge <= preloadPixels)
        {
            SpawnNextCard();
        }
    }

    private void SpawnNextCard()
    {
        if (spawnedCount >= cards.Count)
            return;

        CardContent data = cards[spawnedCount];

        GameObject card = Instantiate(cardPrefab, content);
        card.transform.localScale = Vector3.one; // just in case

        // change the name of the card to the index + data name (optional)
        card.name = $"Card {spawnedCount} - {data.name}";

        // --- Find & set Name text ---
        // Path: Card -> maskPanel -> Content -> Name -> Text (TMP)
        TextMeshProUGUI nameText = card.transform
            .Find("maskPanel/Content/Name/Text (TMP)")
            ?.GetComponent<TextMeshProUGUI>();

        if (nameText != null)
        {
            nameText.text = data.name;
        }
        else
        {
            Debug.LogWarning($"[CarouselController] Name Text (TMP) not found on spawned card {card.name}");
        }

        // --- Find & set Description text ---
        // Path: Card -> maskPanel -> Content -> Description -> Text (TMP)
        TextMeshProUGUI descriptionText = card.transform
            .Find("maskPanel/Content/Description/Text (TMP)")
            ?.GetComponent<TextMeshProUGUI>();

        if (descriptionText != null)
        {
            descriptionText.text = data.description;
        }
        else
        {
            Debug.LogWarning($"[CarouselController] Description Text (TMP) not found on spawned card {card.name}");
        }

        // --- Find & set Background image ---
        // Path: Card -> maskPanel -> Content -> Name -> Background
        Image backgroundImage = card.transform
            .Find("maskPanel/Content/Name/Background")
            ?.GetComponent<Image>();

        if (backgroundImage != null)
        {
            backgroundImage.sprite = data.backgroundImage;
            backgroundImage.preserveAspect = true; // optional, keeps aspect ratio
        }
        else
        {
            Debug.LogWarning($"[CarouselController] Background Image not found on spawned card {card.name}");
        }

        // --- Add Button functionality to the card ---
        Button cardButton = card.GetComponent<Button>();
        if (cardButton == null)
        {
            // If the card doesn't have a Button component, add one
            cardButton = card.AddComponent<Button>();
        }

        // Set up the button click event
        int cardIndex = spawnedCount; // Capture the index for the lambda
        cardButton.onClick.RemoveAllListeners();
        cardButton.onClick.AddListener(() => OnCardSelected(cardIndex));

        spawnedCount++;
    }

    private void OnCardSelected(int cardIndex)
    {
        if (cardIndex < 0 || cardIndex >= cards.Count)
        {
            Debug.LogWarning($"[CarouselController] Invalid card index: {cardIndex}");
            return;
        }

        CardContent selectedCard = cards[cardIndex];

        // Enable the linked canvas
        if (selectedCard.linkedCanvas != null)
        {
            selectedCard.linkedCanvas.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"[CarouselController] No linked canvas assigned to card: {selectedCard.name}");
        }

        // Disable the main canvas
        if (mainCanvas != null)
        {
            mainCanvas.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"[CarouselController] Main canvas is not assigned!");
        }
    }
}
