using UnityEngine;

public class TempCard : CardBase
{

    public override void Init(CardInfo cardInfo)
    {
        durability = 5;

        cardBaseId = 0;
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.Ace;
        cardRarity = Define.CardRarity.Common;

        cardName = "TempCard";
        collection = "TempCollection";
        uniqueAbilityId = 0;

        cardNameBack = "TempCardBack";
        uniqueAbilityIdBack = 0;
        collectionBack = "TempCollectionBack";
    }

    public override void OnCardDraw()
    {
        Debug.Log($"{cardName} Draw");
    }
    public override void OnNoteTrigger()
    {

    }


    public override void OnCardDestroy()
    {
        Debug.Log($"{cardName} Destroy");

    }



}
