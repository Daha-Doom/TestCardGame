using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController
{
    private ChainGenerator _chainGenerator;
    private LevelGenerator _levelGenerator;
    private ParentChildCardSetter _parentChildCardSetter;
    private InteractCardHandler _interactCardHandler;

    public MainController()
    {
        _chainGenerator = new();
        _levelGenerator = new();
        _parentChildCardSetter = new ParentChildCardSetter();
    }

    public void StartGame(int cardCount, CardView cardPrefab)
    {
        var allCard = _parentChildCardSetter.SetParentChildCard();

        var chains = _chainGenerator.GenerateChains(cardCount);

        foreach (var chain in chains)
            Debug.Log(string.Join(',', chain));

        _levelGenerator.Generate(chains, allCard, cardCount, cardPrefab);

        _interactCardHandler = new InteractCardHandler(allCard[0].FirstOrDefault(c => c.IsOpen), cardCount);

        for (int i = 0; i < allCard.Count; i++)
            foreach(var cardModel in allCard[i])
            {
                CardModel modelRef = cardModel;

                cardModel.CardView.OnClicked += async () =>
                {
                    await _interactCardHandler.InteractAsync(modelRef);
                };
            }
    }

    public void AllButtonListener(ClickableView restartBtn, ClickableView exitBtn)
    {
        restartBtn.OnClicked += () =>
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        };

        exitBtn.OnClicked += () =>
        {
            Debug.Log("Выход из игры...");
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        };
    }
}
