using UnityEngine;

public class RedRoseRearCavalry : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        cardBaseId = 60; // todo
        cardSuit = Define.CardSuit.Heart;
        cardRank = Define.CardRank.Six;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Red Rose Rear Cavalry";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

    }

    public override void OnCardDraw()
    {
        GetRankDouble();
    }

    public override void OnCardDestroy()
    {
        RemoveRankDouble();
    }
}
