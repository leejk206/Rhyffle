using UnityEngine;
using static Define;

public class BlackRoseKing : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.King;
        cardRarity = Define.CardRarity.Legendary;
        cardName = "Black Rose King";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

        Debug.Log("init");
    }

    public override void OnCardDrawComplete()
    {
        ApplyBlackRoseKingEffect();
    }
}
