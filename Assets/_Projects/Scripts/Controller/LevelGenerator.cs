using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator
{
    public void Generate(List<List<int>> chains, int cardCount)
    {
        var cardBunchs = GameObject.FindGameObjectsWithTag("CardBunch").ToList();

        var bank = new List<int>();

        foreach(var chain in chains)
        {
            bank.Add(chain[0]);

            for (int i = 1; i < chain.Count; i++)
            {
                int tryCount = 0;
                var isPlaced = false;

                while (!isPlaced)
                {
                    var curantBunch = cardBunchs[Random.Range(0, cardBunchs.Count)];

                    var cards = curantBunch.GetComponentsInChildren<CardView>();

                    var card = cards.FirstOrDefault(c => c._inspectorChild == null)?.CardModel;

                    if (card != null) isPlaced = TryPlaceInBunch(card, chain[i]);

                    tryCount++;

                    if (tryCount == 40)
                        throw new System.Exception("Количество попыток привысило количество кард");
                }
            }
        }
    }

    private bool TryPlaceInBunch(CardModel card, int value)
    {
        if (card.Value == 0)
        {
            card.Value = value;
            return true;
        }
        else if (card.ParentCard != null)
        {
            return TryPlaceInBunch(card.ParentCard, value);
        }
        return false;
    }
}
