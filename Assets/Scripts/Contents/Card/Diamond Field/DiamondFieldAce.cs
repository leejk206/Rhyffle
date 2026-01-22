using UnityEngine;

public class DiamondFieldAce : DiamondFieldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 113; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Ace;
        cardRarity = Define.CardRarity.Legendary;
        cardName = "Diaond Field Ace";
        collection = "Diamond Field";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

    }
}
