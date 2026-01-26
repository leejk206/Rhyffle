using UnityEngine;

public class DiamondFieldFirstBaseman : DiamondFieldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 115; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Three;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Diamond Field First Baseman";
        collection = "Diamond Field";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

        property = Property.Infielder;

    }
}
