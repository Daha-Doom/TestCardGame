using System.Collections.Generic;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField]
    public List<Sprite> CardsImage;

    public void RenderFaceCard(int value)
    {
        var spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = CardsImage[value];
    }
}