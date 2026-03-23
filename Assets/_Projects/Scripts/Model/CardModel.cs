using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;

public class CardModel
{
    private CardView _cardView;
    public CardView CardView => _cardView;

    private int _mark;
    private int _value;
    private CardModel _parentCard;
    private CardModel _childCard;
    private bool _isBabkCard = false;

    public int Mark => _mark;
    public int Value => _value;
    public CardModel ParentCard => _parentCard;
    public CardModel ChildCard => _childCard;
    public bool IsOpen => ChildCard == null;
    public bool IsBankCard => _isBabkCard;

    public CardModel(CardView cardView, int mark)
    {
        _cardView = cardView;
        _mark = mark;
    }

    public void SetChild(CardModel child) => _childCard = child;
    public void SetParent(CardModel parent) => _parentCard = parent;

    public void SetValue(int value)
    {
        _value = value;

        if(_childCard == null)
        {
            _cardView.RenderFaceCard(value, _mark);
        }
    }

    public void SetCard(int mark, int value)
    {
        _mark = mark;

        SetValue(value);
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
            _parentCard._cardView.RenderFaceCard(_parentCard.Value, _parentCard._mark);
        }
    }

    public void Shake()
    {
        _cardView.transform.DOShakePosition(0.3f, 0.1f);
    }
}
