using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainController : MonoBehaviour
{
    private ChainGenerator _chainGenerator;
    private LevelGenerator _levelGenerator;
    private ParentChildCardSetter _parentChildCardSetter;

    public MainController()
    {
        _chainGenerator = new();
        _levelGenerator = new();
        _parentChildCardSetter = new ParentChildCardSetter();
    }

    public void StartGenerate(int cardCount, CardView cardPrefab)
    {
        var allCard = _parentChildCardSetter.SetParentChildCard();

        foreach (var cards in allCard)
            Debug.Log(string.Join(',', cards.ToList()));

        var chains = _chainGenerator.GenerateChains(cardCount);

        foreach (var chain in chains)
            Debug.Log(string.Join(',', chain));

        _levelGenerator.Generate(chains, allCard, cardCount, cardPrefab);
    }
}
