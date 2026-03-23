using DG.Tweening;
using TMPro;
using UnityEngine;

public class WinAction
{
    public void ShowWinText()
    {
        var winText = GameObject.FindGameObjectWithTag("WinText").GetComponent<TextMeshProUGUI>();

        winText.text = "YOU WIN!";

        winText.transform.localScale = Vector3.zero;

        winText.transform.DOScale(1.2f, 0.7f).SetEase(Ease.OutBack).OnComplete(() => {
            winText.transform.DOShakeRotation(2f, 5f, 1, 90, false).SetLoops(-1);
        });
    }
}