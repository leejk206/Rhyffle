using UnityEngine;
using static Define;

public class BlackRoseFieldCommander : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 60; // todo
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.Four;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Black Rose Field Commander";
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