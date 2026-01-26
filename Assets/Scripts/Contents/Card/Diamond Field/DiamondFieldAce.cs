using System.Linq;
using UnityEngine;

public class DiamondFieldAce : DiamondFieldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 113; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Ace;
        cardRarity = Define.CardRarity.Legendary;
        cardName = "Diamond Field Ace";
        collection = "Diamond Field";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

    }

    public override void OnCardDrawComplete()
    {
        base.OnCardDrawComplete();

        if (Managers.Effect.Brands.Any(b => b is IronWallInfield) && Managers.Effect.Brands.Any(b => b is IronWallOutfield))
        {
            if (Managers.Card.FieldCards.Any(card => card is DiamondFieldCatcher))
            {
                // 중복 방지: 퍼펙트 게임 낙인은 1회만 획득
                if (!Managers.Effect.Brands.Any(b => b is PerfectGameBrand))
                {
                    Managers.Effect.Brands.Add(new PerfectGameBrand());
                }
            }
        }
    }
}

public class PerfectGameBrand : BrandBase
{
    // 퍼펙트 게임: 모든 "랭크 점수"에 +99
    // - Challenge 모드에서만 CardBonus가 점수에 합산되므로(ScoreManager),
    //   여기서 CardBonus를 올리면 요구사항과 모드 제약을 동시에 만족한다.
    // - 중복은 DiamondFieldAce에서 방지(일반적으로 1개만 존재)
    public override void OnBeforeScoreApply(ScoreContext context)
    {
        context.CardBonus += 99;
        base.OnBeforeScoreApply(context);
    }
}