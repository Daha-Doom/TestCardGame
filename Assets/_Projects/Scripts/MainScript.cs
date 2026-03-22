using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    [SerializeField]
    private int _cardCount = 40;

    private void Awake()
    {
        var mainController = new MainController();

        mainController.StartGenerate(_cardCount);
    }
}
