using UnityEngine;
using static Define;

public class RedRoseKing : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Heart;
        cardRank = Define.CardRank.King;
        cardRarity = Define.CardRarity.Legendary;
        cardName = "Red Rose King";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

        Debug.Log("init");
    }

    public override void OnCardDrawComplete()
    {
        ApplyRedRoseKingEffect();
    }
}
