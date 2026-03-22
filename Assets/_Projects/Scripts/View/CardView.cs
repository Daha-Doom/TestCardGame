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
    [SerializeField]
    public int Mark;

    public void Awake()
    {
        //0 - крести
        //1 - бубны
        //2 - червы
        //3 - пики
        Mark = UnityEngine.Random.Range(0, 3);
    }

    public event Action OnClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        OnClicked?.Invoke();
    }

    public void RenderFaceCard(int value)
    {
        var img = this.GetComponent<Image>();
        img.sprite = CardsImage[value + (13 * Mark)];
    }
}