using DG.Tweening;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractCardHandler
{
    private CardModel _topBankCard;
    public void SetBankCard(CardModel bankCard) => _topBankCard = bankCard;

    public InteractCardHandler(CardModel bankCard)
    {
        _topBankCard = bankCard;
    }

    public async Task InteractAsync(CardModel clickedCard)
    {
        if (clickedCard.IsBankCard)
        {
            InteractBankCard();
        }
        else
        {
            await InteractBaseCardAsync(clickedCard);
        }
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
        }
        else
        {
            Shake(clickedCard);
        }
    }

    public void InteractBankCard()
    {
        if (_topBankCard.ParentCard != null)
        {
            var nextCard = _topBankCard.ParentCard;

            GoNext(_topBankCard);

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
        card.CardView.GetComponent<Image>().raycastTarget = false;

        await card.CardView.transform.DOMove(_topBankCard.CardView.transform.position, 0.4f)
        .AsyncWaitForCompletion();

        //card.CardView.transform.DOMove(_topBankCard.CardView.transform.position, 0.4f)
        //                        .OnComplete(async () =>
        //                        {
        //                            _topBankCard.CardView.Mark = card.CardView.Mark;
        //                            _topBankCard.SetValue(card.Value);
        //                            GoNext(card);
        //                            await Task.Delay(1000);
        //                            CheckWinCondition();
        //                        });

        _topBankCard.CardView.Mark = card.CardView.Mark;
        _topBankCard.SetValue(card.Value);

        GoNext(card);

        CheckWinCondition();
    }

    private void GoNext(CardModel card)
    {
        card.OpenParent();
        UnityEngine.Object.Destroy(card.CardView.gameObject);
    }

    public void Shake(CardModel card)
    {
        card.CardView.transform.DOShakePosition(0.5f, 5f);
    }

    private void CheckWinCondition()
    {
        var bunches = GameObject.FindGameObjectsWithTag("CardBunch");

        foreach (var bunch in bunches)
        {
            Debug.Log("Кучка имеет " + bunch.transform.childCount + " детей");
            if (bunch.transform.childCount > 1)
            {
                return;
            }
        }
        OnWin();
    }

    private void OnWin()
    {
        Debug.Log("Победа! Поле очищено.");

        var winText = GameObject.FindGameObjectWithTag("WinText").GetComponent<TextMeshProUGUI>();

        winText.text = "YOU WIN!";

        winText.transform.localScale = Vector3.zero;

        winText.transform.DOScale(1.2f, 0.7f).SetEase(Ease.OutBack).OnComplete(() => {
            winText.transform.DOShakeRotation(2f, 5f, 1, 90, false).SetLoops(-1);
        });
    }
}
