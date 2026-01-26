using UnityEngine;

public class DiamondFieldSecondBaseman : DiamondFieldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 116; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Four;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Diamond Field Second Baseman";
        collection = "Diamond Field";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

        property = Property.Infielder;
    }
}
