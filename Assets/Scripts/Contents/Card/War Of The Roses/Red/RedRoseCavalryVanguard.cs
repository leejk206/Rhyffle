using UnityEngine;
using static Define;

public class RedRoseCavalryVanguard : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        cardBaseId = 59;
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Six;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Red Rose Cavarly Vanguard";
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