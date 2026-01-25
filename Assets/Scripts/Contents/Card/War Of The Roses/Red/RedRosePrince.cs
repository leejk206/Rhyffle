using System.Linq;
using UnityEngine;
using static Define;

public class RedRosePrince : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.King;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Red Rose Prince";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

        Debug.Log("init");
    }

    public override void OnCardDraw()
    {
        SetCardEffect("Red Rose King");
    }
}
