using System.Linq;
using UnityEngine;
using static Define;

public class BlackRosePrincess : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Club;
        cardRank = Define.CardRank.Queen;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Black Rose Princess";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

        Debug.Log("init");
    }

    public override void OnCardDraw()
    {
        SetCardEffect("Black Rose Queen");
    }
}
