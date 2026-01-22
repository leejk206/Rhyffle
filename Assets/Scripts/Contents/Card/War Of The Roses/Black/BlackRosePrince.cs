using System.Linq;
using UnityEngine;
using static Define;

public class BlackRosePrince : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.King;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Black Rose Prince";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

        Debug.Log("init");
    }

    public override void OnCardDraw()
    {
        SetCardEffect("Black Rose King");
    }
}
