using UnityEngine;

public class DiamondFieldRightFielder: DiamondFieldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 121; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Nine;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Diamond Field Right Fielder";
        collection = "Diamond Field";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();
        property = Property.Outfielder;
    }
}
