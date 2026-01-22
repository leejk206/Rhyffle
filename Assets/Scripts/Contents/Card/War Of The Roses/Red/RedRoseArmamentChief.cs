using System.Linq;
using UnityEngine;

public class RedRoseArmamentChief : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        cardBaseId = 60; // todo
        cardSuit = Define.CardSuit.Heart;
        cardRank = Define.CardRank.Seven;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Red Rose Armament Chief";
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
