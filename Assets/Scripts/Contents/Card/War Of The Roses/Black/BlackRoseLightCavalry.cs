using UnityEngine;
using static Define;

public class BlackRoseLightCavalry : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.Ten;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Black Rose Light Cavarly";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();
    }

    public override void OnCardDrawComplete()
    {

        Debug.Log("Black Rose Light Cavarly Effect");
        GetRankBonus();
    }
}