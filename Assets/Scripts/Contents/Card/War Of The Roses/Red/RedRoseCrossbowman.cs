using UnityEngine;
using System.Linq;
using static Define;

public class RedRoseCrossbowman : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        cardBaseId = 61; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Three;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Red Rose Crossbowman";
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