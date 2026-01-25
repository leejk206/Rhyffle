using UnityEngine;
using static Define;

public class RedRoseVanguard : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Ace;
        cardRarity = Define.CardRarity.Legendary;
        cardName = "Red Rose Vanguard";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

        Debug.Log("init");
    }

    public override void OnCardDrawComplete()
    {
        Debug.Log($"{cardName} Effect");
        VanguardEffect();
    }
}