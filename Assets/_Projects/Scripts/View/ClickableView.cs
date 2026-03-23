using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action OnClicked;

    private Vector3 _initialScale;

    [SerializeField]
    private float _hoverMultiplier = 1.05f;

    private void Awake()
    {
        _initialScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = _initialScale * _hoverMultiplier;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = _initialScale;
    }
}
