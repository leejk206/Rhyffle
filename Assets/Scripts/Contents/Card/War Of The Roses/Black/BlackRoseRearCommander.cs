using System.Linq;
using UnityEngine;
using static Define;

public class BlackRoseRearCommander : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 60; // todo
        cardSuit = Define.CardSuit.Club;
        cardRank = Define.CardRank.Ace;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Black Rose Rear Commander";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

    }

    public override void OnCardDrawComplete()
    {
        RearCommanderEffect();
    }
}
