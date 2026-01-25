using UnityEngine;
using static Define;

public class BlackRoseAxeman : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.Seven;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Black Rose Axeman";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();
    }

    public override void OnCardDrawComplete()
    {

        Debug.Log("Black Rose Axeman Effect");
        GetRankBonus();
    }

}