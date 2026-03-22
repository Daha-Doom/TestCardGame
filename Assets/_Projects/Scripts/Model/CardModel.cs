using DG.Tweening;
using UnityEngine;

public class CardModel
{
    private CardView _cardView;
    public CardView CardView => _cardView;

    private int _value;
    private CardModel _parentCard;
    private CardModel _childCard;
    private bool _isBabkCard = false;

    public int Value => _value;
    public CardModel ParentCard => _parentCard;
    public CardModel ChildCard => _childCard;
    public bool IsOpen => ChildCard == null;
    public bool IsBankCard => _isBabkCard;

    public CardModel(CardView cardView)
    {
        _cardView = cardView;
    }

    public void SetChild(CardModel child) => _childCard = child;
    public void SetParent(CardModel parent) => _parentCard = parent;

    public void SetValue(int value)
    {
        _value = value;

        if(_childCard == null)
        {
            _cardView.RenderFaceCard(value);
        }
    }

    public void SetAsBankCard()
    {
        _isBabkCard = true;
    }

    public void OpenParent()
    {
        if (_parentCard != null)
        {
            _parentCard._childCard = null;
            _parentCard._cardView.RenderFaceCard(_parentCard.Value);
        }
    }

    public void Shake()
    {
        _cardView.transform.DOShakePosition(0.3f, 0.1f);
    }
}
