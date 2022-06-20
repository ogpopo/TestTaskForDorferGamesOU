using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public event Action<Vector2> OnJoystickMove;

    [SerializeField] private RectTransform _containerRect;
    [SerializeField] private RectTransform _handleRect;

    [SerializeField] private float _joystickRange;

    public void OnPointerDown(PointerEventData eventData)
    {
        ShowJoystick(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_containerRect, eventData.position, null, out Vector2 position);

        position = ApplySizeDelta(position);

        Vector2 clampedPosition = ClampValuesToMagnitude(position);

        Vector2 outputPosition = ApplyInversionFilter(position);

        OutputPointeEventValue(outputPosition.normalized);

        UpdateHandleRectPosition(clampedPosition * _joystickRange);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OutputPointeEventValue(Vector2.zero);

        UpdateHandleRectPosition(Vector2.zero);

        _containerRect.gameObject.SetActive(false);
    }

    private Vector2 ApplySizeDelta(Vector2 position)
    {
        float x = (position.x / _containerRect.sizeDelta.x) * 2.5f;
        float y = (position.y / _containerRect.sizeDelta.y) * 2.5f;
        return new Vector2(x, y);
    }

    private Vector2 ClampValuesToMagnitude(Vector2 position)
    {
        return Vector2.ClampMagnitude(position, 1);
    }

    Vector2 ApplyInversionFilter(Vector2 position)
    {
        position.x = InvertValue(position.x);
        position.y = InvertValue(position.y);

        return position;
    }

    private void UpdateHandleRectPosition(Vector2 newPosition)
    {
        _handleRect.anchoredPosition = newPosition;
    }

    private void OutputPointeEventValue(Vector2 pointerPosition)
    {
        OnJoystickMove?.Invoke(pointerPosition);
    }

    private void ShowJoystick(PointerEventData eventData)
    {
        _containerRect.position = eventData.position;
        _containerRect.gameObject.SetActive(true);
    }

    private float InvertValue(float value)
    {
        return -value;
    }
}