using UnityEngine;
using static Define;

public class BlackRoseVanguard : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.Ace;
        cardRarity = Define.CardRarity.Legendary;
        cardName = "Black Rose Vanguard";
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