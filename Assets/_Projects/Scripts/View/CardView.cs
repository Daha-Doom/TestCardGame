using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField]
    public CardView _inspectorParent;
    [SerializeField]
    public CardView _inspectorChild;

    public CardModel CardModel => _cardModel;
    private CardModel _cardModel;

    public void Bind(CardModel cardModel)
    {
        _cardModel = cardModel;
    }
}