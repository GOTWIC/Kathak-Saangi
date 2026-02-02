using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CarouselEndJump : MonoBehaviour, IPointerClickHandler
{
    public enum Direction
    {
        Left,
        Right
    }

    [Header("References")]
    [SerializeField] private Slider slider;

    [Header("Settings")]
    [SerializeField] private Direction direction = Direction.Right;

    [Tooltip("How fast to move the slider value (0..1) per second.")]
    [SerializeField] private float speed = 3f;

    private Coroutine _jumpRoutine;

    public void OnPointerClick(PointerEventData eventData)
    {
        BeginJump();
    }

    private void BeginJump()
    {
        if (slider == null) return;

        if (_jumpRoutine != null)
            StopCoroutine(_jumpRoutine);

        _jumpRoutine = StartCoroutine(JumpToEnd());
    }

    private IEnumerator JumpToEnd()
    {
        float target = (direction == Direction.Left) ? 0f : 1f;

        // If speed is invalid, snap instantly
        if (speed <= 0f)
        {
            slider.value = target;
            _jumpRoutine = null;
            yield break;
        }

        // Move until we reach the target
        while (!Mathf.Approximately(slider.value, target))
        {
            slider.value = Mathf.MoveTowards(
                slider.value,
                target,
                speed * Time.unscaledDeltaTime
            );
            yield return null;
        }

        _jumpRoutine = null;
    }

    private void OnDisable()
    {
        if (_jumpRoutine != null)
        {
            StopCoroutine(_jumpRoutine);
            _jumpRoutine = null;
        }
    }
}