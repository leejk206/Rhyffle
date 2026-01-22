using UnityEngine;
using static Define;

public class RedRoseFieldCommander : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        cardBaseId = 60; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Seven;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Red Rose Field Commander";
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