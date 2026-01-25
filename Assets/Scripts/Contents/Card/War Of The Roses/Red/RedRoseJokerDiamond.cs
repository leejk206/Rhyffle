using UnityEngine;
using static Define;

public class RedRoseJokerDiamond : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        // Red Rose Joker (Diamond)
        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Joker;
        cardRarity = Define.CardRarity.Epic;
        cardName = "Red Rose Joker Diamond";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        // Red 쪽 조커이므로 isBlack 은 false
        isBlack = false;

        LoadCardSprite();
    }

    public override void OnCardDrawComplete()
    {
        JokerInfoGatherEffect();
    }
}

public class RedRoseInfoGather : BrandBase
{

}

