using UnityEngine;
using System.Linq;
using static Define;

public class RedRoseLongbowman : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        cardBaseId = 58; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Two;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Red Rose Longbowman";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

    }

    public override void OnCardDrawComplete()
    {

        Debug.Log($"{cardName} Effect");
        GetRankBonus();
    }
}