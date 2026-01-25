using UnityEngine;
using static Define;

public class BlackRoseJokerSpade : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.Joker;
        cardRarity = Define.CardRarity.Epic;
        cardName = "Black Rose Joker Spade";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

    }

    public override void OnCardDrawComplete()
    {
        JokerInfoGatherEffect();
    }
}

public class BlackRoseInfoGather : BrandBase
{

}