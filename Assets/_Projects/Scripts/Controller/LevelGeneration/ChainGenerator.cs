using System.Collections.Generic;
using UnityEngine;

public class ChainGenerator
{
    private const int bottomBorder = 10;
    private const int topBorder = 20;

    public List<List<int>> GenerateChains(int cardCount)
    {
        var curantCardCount = cardCount;

        var chainsList = new List<List<int>>();

        while (curantCardCount > 0)
        {
            int chainLength = Random.Range(bottomBorder, topBorder);

            chainLength = curantCardCount < chainLength ? curantCardCount : chainLength;

            curantCardCount -= chainLength;

            //Чтобы избежать слишком коротких цепочек
            if (curantCardCount < bottomBorder)
            {
                chainLength += curantCardCount;
                curantCardCount = 0;
            }

            //Добавляем карту, которую сможет забрать банк
            chainLength++;

            chainsList.Add(GenerateChain(chainLength));
        }

        return chainsList;
    }

    private List<int> GenerateChain(int chainLength)
    {
        var chain = new List<int>();

        int cardValue = Random.Range(1, 14);
        chain.Add(cardValue);

        bool isUp = CheckToggle(65, true);

        for (int i = 1; i < chainLength; i++)
        {
            cardValue += isUp ? 1 : -1;

            if (cardValue == 14) cardValue = 1;
            else if (cardValue == 0) cardValue = 13;

            chain.Add(cardValue);

            isUp = CheckToggle(15, isUp);
        }

        return chain;
    }

    private bool CheckToggle(int propapility, bool currentBool)
    {
        return Random.Range(0, 100) < propapility ? !currentBool : currentBool;
    }
}
