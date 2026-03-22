using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator
{
    public void Generate(List<List<int>> chains, List<CardModel[]>allCards, int cardCount, CardView cardPrefab)
    {
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
                    var cards = allCards[Random.Range(0, allCards.Count)];

                    var card = cards.FirstOrDefault(c => c.ChildCard == null);

                    if (card != null) 
                        isPlaced = TryPlaceInBunch(card, chain[i]);

                    tryCount++;

                    if (tryCount == 40)
                        throw new System.Exception("Количество попыток привысило количество кард");
                }
            }
        }

        bank.Reverse();

        GenerateBank(bank, cardPrefab, allCards);
    }

    private bool TryPlaceInBunch(CardModel card, int value)
    {
        if (card.Value == 0)
        {
            card.SetValue(value);
            return true;
        }
        else if (card.ParentCard != null)
        {
            return TryPlaceInBunch(card.ParentCard, value);
        }
        return false;
    }

    private void GenerateBank(List<int> bank, CardView cardPrefab, List<CardModel[]> allCards)
    {
        List<CardView> bankViews = new List<CardView>();
        var bankContainer = GameObject.FindGameObjectWithTag("CardBank").GetComponent<Transform>();

        for (int i = 0; i < bank.Count; i++)
        {
            var newCard = Object.Instantiate(cardPrefab, bankContainer);

            newCard.transform.localPosition = new Vector3(0, i * 0.05f, 0);

            newCard.GetComponent<SpriteRenderer>().sortingOrder = i;
            bankViews.Add(newCard);
        }

        var bankModels = new CardModel[bankViews.Count];
        GenerateTools.CreateCardChain(0, bankViews.ToArray(), null, bankModels);

        for (int i = 0; i < bankModels.Length; i++)
        {
            bankModels[i].SetValue(bank[i]);
        }

        allCards.Add(bankModels);
    }
}
