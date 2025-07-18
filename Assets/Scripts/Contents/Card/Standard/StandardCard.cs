using UnityEngine;

public class StandardCard : CardBase
{
    public override void SetCardProperty(Define.CardSuit cardSuit, Define.CardRank cardRank)
    {
        durability = 5;

        cardBaseId = 0;
        this.cardSuit = cardSuit;
        this.cardRank = cardRank;
        cardRarity = Define.CardRarity.Common;

        cardName = $"0{cardSuit.ToString()}_{cardRank.ToString()}";
        collection = "Standard";
        uniqueAbilityId = 0;

        cardNameBack = "Standard";
        uniqueAbilityIdBack = 0;
        collectionBack = "TempCollectionBack";
    } // Todo 카드 정보에 맞춰 수정

}
