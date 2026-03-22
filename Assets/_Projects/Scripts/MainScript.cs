using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    [SerializeField]
    private int _cardCount = 40;
    [SerializeField]
    private CardView cardPrefab;

    private void Awake()
    {
        var mainController = new MainController();

        mainController.StartGame(_cardCount, cardPrefab);
    }
}
