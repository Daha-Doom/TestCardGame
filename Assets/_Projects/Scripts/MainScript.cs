using UnityEngine;

public class MainScript : MonoBehaviour
{
    [SerializeField]
    private int _cardCount = 40;
    [SerializeField]
    private CardView _cardPrefab;

    [SerializeField]
    private ClickableView _restartButton;
    [SerializeField]
    private ClickableView _exitButton;

    private void Awake()
    {
        var mainController = new MainController();

        mainController.StartGame(_cardCount, _cardPrefab);

        mainController.AllButtonListener(_restartButton, _exitButton);
    }
}
