using UnityEngine;

public class DiamondFieldLeftFielder : DiamondFieldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 119; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Seven;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Diamond Field Left Fielder";
        collection = "Diamond Field";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();
        property = Property.Outfielder;
    }
}
