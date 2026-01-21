using UnityEngine;

public abstract class WaroftheRosesBase : CardBase
{
    public bool isBlack;

    public override void Init(CardInfo cardInfo)
    {

        collection = "War Of The Roses";
        uniqueAbilityId = 0;

        cardNameBack = "War Of The Roses Back";
        uniqueAbilityIdBack = 0;
        collectionBack = "War Of The Roses Back";

        isBlack = (cardSuit == Define.CardSuit.Spade || cardSuit == Define.CardSuit.Club);
    }

    public void GetBlackRoseRankBonus()
    {
        int cnt = 0;

        foreach (CardBase item in Managers.Card.FieldCards)
        {
            if (item == null)
                continue;

            if (item is WaroftheRosesBase wotr && wotr.isBlack) { cnt += 2; } else { cnt += 1; }
        }
        this.CardRank += cnt * Managers.Effect.EffectData.BlackRoseMultiplier;
    }

    public void GetRedkRoseRankBonus()
    {
        int cnt = 0;

        foreach (CardBase item in Managers.Card.FieldCards)
        {
            if (item == null)
                continue;

            if (item is WaroftheRosesBase wotr && !wotr.isBlack) { cnt += 2; } else { cnt += 1; }
        }
        this.CardRank += cnt * Managers.Effect.EffectData.RedRoseMultiplier;
    }
}
