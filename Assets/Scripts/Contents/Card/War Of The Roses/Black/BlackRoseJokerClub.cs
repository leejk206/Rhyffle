using System.Linq;
using UnityEngine;
using static Define;

public class BlackRoseJokerClub : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Club;
        cardRank = Define.CardRank.Joker;
        cardRarity = Define.CardRarity.Epic;
        cardName = "Black Rose Joker Club";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

    }

    public override void OnCardDrawComplete()
    {
        JokerInfoWithdrawEffect();
    }
}

public class BlackRoseInfoWithdraw : BrandBase
{
    // 붉은 장미단 고유능력의 추가 점수 5배 적용
    public override void OnBeforeScoreApply(ScoreContext context)
    {
        // 여기서는 단순히 cardBonus 전체를 5배로 처리
        context.CardBonus *= 5;
    }
}