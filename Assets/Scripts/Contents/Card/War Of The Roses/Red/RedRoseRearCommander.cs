using System.Linq;
using UnityEngine;
using static Define;

public class RedRoseRearCommander : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        cardBaseId = 60; // todo
        cardSuit = Define.CardSuit.Heart;
        cardRank = Define.CardRank.Ace;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Red Rose Rear Commander";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

    }

    public override void OnCardDrawComplete()
    {
        RearCommanderEffect();
    }
}
