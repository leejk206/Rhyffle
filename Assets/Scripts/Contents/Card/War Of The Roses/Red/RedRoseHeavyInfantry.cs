using UnityEngine;
using static Define;

public class RedRoseHeavyInfantry : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Nine;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Red Rose Heavy Infantry";
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