using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GenerateTools
{
    public static CardModel CreateCardChain(int index, CardView[] cardViews, CardModel parentModel, CardModel[] targerArray)
    {
        if (index >= cardViews.Length) return null;

        var currentModel = new CardModel(cardViews[index]);

        currentModel.SetParent(parentModel);

        var child = CreateCardChain(index + 1, cardViews, currentModel, targerArray);

        currentModel.SetChild(child);

        targerArray[index] = currentModel;

        return currentModel;
    }
}
