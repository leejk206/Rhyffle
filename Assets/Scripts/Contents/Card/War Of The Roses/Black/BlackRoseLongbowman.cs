using UnityEngine;
using System.Linq;
using static Define;

public class BlackRoseLongbowman : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 58; // todo
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.Two;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Black Rose Longbowman";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

    }

    public override void OnCardDrawComplete()
    {

        Debug.Log("Black Rose Longbowman Effect");
        GetRankBonus();
        
    }
}