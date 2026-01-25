using UnityEngine;
using System.Linq;
using static Define;

public class BlackRoseCrossbowman : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 61; // todo
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.Five;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Black Rose Crossbowman";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();
    }

    public override void OnCardDrawComplete()
    {

        Debug.Log("Black Rose Crossbowman Effect");
        GetRankBonus();
    }
}