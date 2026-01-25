using UnityEngine;
using static Define;

public class BlackRoseHeavyInfantry : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.Nine;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Black Rose Heavy Infantry";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

    }

    public override void OnCardDrawComplete()
    {

        Debug.Log("Black Rose Heavy Infantry Effect");
        GetRankBonus();
    }
}