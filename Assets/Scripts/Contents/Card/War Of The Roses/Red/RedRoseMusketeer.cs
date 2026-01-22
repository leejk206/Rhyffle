using UnityEngine;
using static Define;

public class RedRoseMusketeer : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        cardBaseId = 62; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Four;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Red Rose Musketeer";
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