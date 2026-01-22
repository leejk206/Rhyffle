using UnityEngine;
using static Define;

public class BlackRoseSpearman : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.Eight;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Black Rose Spearman";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();
    }

    public override void OnCardDrawComplete()
    {

        Debug.Log("Black Rose Spearman Effect");
        GetRankBonus();
    }
}