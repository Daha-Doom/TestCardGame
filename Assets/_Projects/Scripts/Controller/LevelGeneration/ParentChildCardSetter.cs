using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParentChildCardSetter
{
    private List<CardModel[]> AllCards = new List<CardModel[]>();

    public List<CardModel[]> SetParentChildCard()
    {
        var cardBunchs = GameObject.FindGameObjectsWithTag("CardBunch").ToList();

        foreach (var bunch in cardBunchs)
        {
            var cardViews = bunch.GetComponentsInChildren<CardView>().ToList();

            var cards = cardViews.OrderBy(view => view.Order)
                                    .ToArray();

            var cardsOneBunch = new CardModel[cards.Length];

            GenerateTools.CreateCardChain(0, cards, null, cardsOneBunch);

            AllCards.Add(cardsOneBunch);
        }

        return AllCards;
    }
}
