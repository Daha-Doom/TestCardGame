using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class InteractCardHandler
{
    private int _cardOnField;
    private bool _isBusy;

    private WinAction _winAction;

    private CardModel _topBankCard;
    public void SetBankCard(CardModel bankCard) => _topBankCard = bankCard;

    public InteractCardHandler(CardModel bankCard, int cardOnField)
    {
        _cardOnField = cardOnField;
        _topBankCard = bankCard;
        _winAction = new WinAction();
    }

    public async Task InteractAsync(CardModel clickedCard)
    {
        if (_isBusy) return;

        _isBusy = true;

        if (clickedCard.IsBankCard)
        {
            await InteractBankCardAsync();
        }
        else
        {
            await InteractBaseCardAsync(clickedCard);
        }

        _isBusy = false;
    }

    public async Task InteractBaseCardAsync(CardModel clickedCard)
    {
        if (!clickedCard.IsOpen)
        {
            Shake(clickedCard);
            return;
        }

        if (IsMoveValid(clickedCard.Value, _topBankCard.Value))
        {
            await ExecuteMoveAsync(clickedCard);

            CheckWinCondition();
        }
        else
        {
            Shake(clickedCard);
        }
    }

    public async Task InteractBankCardAsync()
    {
        if (_topBankCard.ParentCard != null)
        {
            _topBankCard.OpenParent();

            var view = _topBankCard.CardView;

            var sequence = DOTween.Sequence();

            sequence.Join(view.transform.DOMoveX(view.transform.position.x + 500f, 0.5f).SetEase(Ease.InBack));
            sequence.Join(view.transform.DORotate(new Vector3(0, 0, 45f), 0.5f));
            sequence.Join(view.GetComponent<Image>().DOFade(0, 0.5f));

            await sequence.AsyncWaitForCompletion();

            var nextCard = _topBankCard.ParentCard;

            UnityEngine.Object.Destroy(view.gameObject);

            _topBankCard = nextCard;
        }
        else
        {
            Shake(_topBankCard);
        }
    }

    private bool IsMoveValid(int clickedCardValue, int topBankCardValue)
    {
        var diff = Math.Abs(clickedCardValue - topBankCardValue);
        return diff == 1 || diff == 12;
    }

    private async Task ExecuteMoveAsync(CardModel card)
    {
        _cardOnField--;

        card.CardView.GetComponent<Image>().raycastTarget = false;

        await card.CardView.transform.DOMove(_topBankCard.CardView.transform.position, 0.4f)
                                        .AsyncWaitForCompletion();

        _topBankCard.SetCard(card.Mark, card.Value);

        card.OpenParent();
        UnityEngine.Object.Destroy(card.CardView.gameObject);
    }

    public void Shake(CardModel card)
    {
        card.CardView.transform.DOShakePosition(0.5f, 5f);
    }

    private void CheckWinCondition()
    {
        if (_cardOnField == 0)
        {
            _winAction.ShowWinText();
        }
    }
}
