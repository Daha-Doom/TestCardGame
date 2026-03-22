using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    private ChainGenerator _chainGenerator;
    private LevelGenerator _levelGenerator;

    public MainController()
    {
        _chainGenerator = new();
        _levelGenerator = new();
    }

    public void StartGenerate(int cardCount)
    {
        var chains = _chainGenerator.GenerateChains(cardCount);

        foreach (var chain in chains)
            Debug.Log(string.Join(',', chain));

        _levelGenerator.Generate(chains, cardCount);
    }
}
