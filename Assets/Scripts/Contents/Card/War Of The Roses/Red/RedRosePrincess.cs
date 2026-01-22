using System.Linq;
using UnityEngine;
using static Define;

public class RedRosePrincess : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Heart;
        cardRank = Define.CardRank.Queen;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Red Rose Princess";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

        Debug.Log("init");
    }

    public override void OnCardDraw()
    {
        SetCardEffect("Red Rose Queen");
    }
}
