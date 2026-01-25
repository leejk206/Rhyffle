using UnityEngine;

public class DiamondFieldUtilityPlayer : DiamondFieldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 126; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Joker;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Diaond Field Ace";
        collection = "Diamond Field";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();
        property = Property.MultiPosition;
    }
}
