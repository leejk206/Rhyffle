using UnityEngine;
using static Define;

public class RedRoseLightCavalry : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Ten;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Red Rose Light Cavarly";
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