using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    public List<Sprite> CardsImage;
    [SerializeField]
    public int Order;

    public event Action OnClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        OnClicked?.Invoke();
    }

    public void RenderFaceCard(int value, int mark)
    {
        var img = this.GetComponent<Image>();
        img.sprite = CardsImage[value + (13 * mark)];
    }
}