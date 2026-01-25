using UnityEngine;
using System.Linq;
using static Define;

public class RedRoseJokerHeart : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        // Red Rose Joker (Heart)
        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Heart;
        cardRank = Define.CardRank.Joker;
        cardRarity = Define.CardRarity.Epic;
        cardName = "Red Rose Joker Heart";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        // Red 조커이므로 isBlack 은 false
        isBlack = false;

        LoadCardSprite();
    }

    public override void OnCardDrawComplete()
    {
        JokerInfoWithdrawEffect();
    }
}

public class RedRoseInfoWithdraw : BrandBase
{
    public override void OnBeforeScoreApply(ScoreContext context)
    {
        // 여기서는 단순히 cardBonus 전체를 5배로 처리
        context.CardMultiplier *= 8;
    }
}

