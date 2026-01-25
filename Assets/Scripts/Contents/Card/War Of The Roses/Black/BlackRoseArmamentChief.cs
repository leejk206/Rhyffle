using System.Linq;
using UnityEngine;

public class BlackRoseArmamentChief : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 60; // todo
        cardSuit = Define.CardSuit.Club;
        cardRank = Define.CardRank.Four;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Black Rose Armament Chief";
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
