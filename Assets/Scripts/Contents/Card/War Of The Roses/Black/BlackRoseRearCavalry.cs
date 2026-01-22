using UnityEngine;

public class BlackRoseRearCavalry : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 60; // todo
        cardSuit = Define.CardSuit.Club;
        cardRank = Define.CardRank.Three;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Black Rose Rear Cavalry";
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
