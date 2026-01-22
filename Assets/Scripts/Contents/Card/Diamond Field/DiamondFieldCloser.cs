using UnityEngine;

public class DiamondFieldCloser : DiamondFieldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 125; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.King;
        cardRarity = Define.CardRarity.Epic;
        cardName = "Diamond Field Closer";
        collection = "Diamond Field";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

    }
}
