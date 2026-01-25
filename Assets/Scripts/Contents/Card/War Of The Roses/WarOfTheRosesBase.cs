using UnityEngine;
using System.Linq;
using static Define;

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

    #region Methods
    private bool IsEffectBlocked()
    {
        if (isBlack && Managers.Effect.Brands.Any(b => b is BlackRoseInfoWithdraw))
        {
            return true;
        }
        if (!isBlack && Managers.Effect.Brands.Any(b => b is RedRoseInfoWithdraw))
        {
            return true;
        }
        return false;
    }

    public void GetRankBonus()
    {
        if (IsEffectBlocked())
            return;

        int blackCnt = 0;
        int redCnt = 0;

        foreach (CardBase item in Managers.Card.FieldCards)
        {
            if (item == null)
                continue;

            if (item is WaroftheRosesBase wotr1 && wotr1.isBlack) { blackCnt += 2; } else { blackCnt += 1; }
            if (item is WaroftheRosesBase wotr2 && !wotr2.isBlack) { redCnt += 2; } else { redCnt += 1; }
        }
        
        if (isBlack)
        {
            this.CardRank += blackCnt * Managers.Effect.EffectData.BlackRoseMultiplier;
        }
        else
        {
            this.CardRank += redCnt * Managers.Effect.EffectData.RedRoseMultiplier;
        }
        Debug.Log($"current {cardName} rank is {this.CardRank}");
    }

    public void GetRankDouble()
    {
        if (IsEffectBlocked())
            return;

        if (isBlack)
        {
            Managers.Effect.EffectData.BlackRoseMultiplier *= 2;
            Debug.Log($"{cardName} Draw Effected. Current Multiplier : {Managers.Effect.EffectData.BlackRoseMultiplier}");
        }
        else
        {
            Managers.Effect.EffectData.RedRoseMultiplier *= 2;
            Debug.Log($"{cardName} Draw Effected. Current Multiplier : {Managers.Effect.EffectData.RedRoseMultiplier}");
        }
    }

    public void RemoveRankDouble()
    {
        if (IsEffectBlocked())
            return;

        if (isBlack)
        {
            Managers.Effect.EffectData.BlackRoseMultiplier /= 2;
            Debug.Log($"{cardName} Destroy Effected. Current Multiplier : {Managers.Effect.EffectData.BlackRoseMultiplier}");
        }
        else
        {
            Managers.Effect.EffectData.RedRoseMultiplier /= 2;
            Debug.Log($"{cardName} Destroy Effected. Current Multiplier : {Managers.Effect.EffectData.RedRoseMultiplier}");
        }
    }

    public void VanguardEffect()
    {
        if (IsEffectBlocked())
            return;

        int left = 0;
        int right = Managers.Deck.UnUsedDeck.Count - 1;
        int swaps = 0;

        while (left < right && swaps < 5)
        {
            if (isBlack)
            {
                // 왼쪽에서 다음 '빨간 카드'(검은 카드를 건너뜀) 찾기
                while (left < right)
                {
                    var suit = SuitHelper.GetEffectiveSuit(Managers.Deck.UnUsedDeck[left].cardSuit);
                    if (suit == Define.CardSuit.Diamond || suit == Define.CardSuit.Heart) break;
                    left++;
                }

                // 오른쪽에서 다음 '검은 카드'(빨간 카드를 건너뜀) 찾기
                while (left < right)
                {
                    var suit = SuitHelper.GetEffectiveSuit(Managers.Deck.UnUsedDeck[right].cardSuit);
                    if (suit == Define.CardSuit.Spade || suit == Define.CardSuit.Club) break;
                    right--;
                }
            }
            else
            {
                // 왼쪽에서 다음 '검은 카드'(빨간 카드를 건너뜀) 찾기
                while (left < right)
                {
                    var suit = SuitHelper.GetEffectiveSuit(Managers.Deck.UnUsedDeck[left].cardSuit);
                    if (suit == Define.CardSuit.Spade || suit == Define.CardSuit.Club) break;
                    left++;
                }

                // 오른쪽에서 다음 '빨간 카드'(검은 카드를 건너뜀) 찾기
                while (left < right)
                {
                    var suit = SuitHelper.GetEffectiveSuit(Managers.Deck.UnUsedDeck[right].cardSuit);
                    if (suit == Define.CardSuit.Diamond || suit == Define.CardSuit.Heart) break;
                    right--;
                }
            }

            if (left < right)
            {
                var tmp = Managers.Deck.UnUsedDeck[left];
                Managers.Deck.UnUsedDeck[left] = Managers.Deck.UnUsedDeck[right];
                Debug.Log($"{tmp.cardName} changed into {Managers.Deck.UnUsedDeck[right].cardName}");
                Managers.Deck.UnUsedDeck[right] = tmp;

                swaps++;
                left++;
                right--;
            }
        }
    }

    public void NegotiatorEffect()
    {
        if (IsEffectBlocked())
            return;

        Debug.Log($"{cardName} Effect");

        foreach (CardBase item in Managers.Card.FieldCards)
        {
            if (item == null)
                continue;

            if (item is WaroftheRosesBase wotr && wotr.isBlack != this.isBlack)
            {
                wotr.isBlack = this.isBlack;
            }
        }
    }

    public void SetCardEffect(string targetCardName)
    {
        if (IsEffectBlocked())
            return;

        Debug.Log($"{cardName} Effect");

        Managers.Card.isCardSetted = true;
        for (int i = 0; i < Managers.Card.SettedCards.Count; i++)
        {
            if (Managers.Card.SettedCards[i] == null)
            {
                Managers.Card.SettedCards[i] = targetCardName;
                break;
            }
        }

        for (int i = 0; i < Managers.Card.SettedCards.Count; i++)
        {
            if (Managers.Card.SettedCards[i] == null)
            {
                Define.CardSuit suit1 = isBlack ? CardSuit.Spade : CardSuit.Heart;
                Define.CardSuit suit2 = isBlack ? CardSuit.Club : CardSuit.Diamond;

                var card = Managers.Deck.UnUsedDeck
                    .FirstOrDefault(x =>
                        x.collection == "War of the Roses" &&
                        (SuitHelper.GetEffectiveSuit(x.cardSuit) == suit1 ||
                         SuitHelper.GetEffectiveSuit(x.cardSuit) == suit2));

                if (card != null)
                {
                    Managers.Card.SettedCards[i] = card.cardName;
                }

                break;
            }
        }
    }

    public void RearCommanderEffect()
    {
        if (IsEffectBlocked())
            return;

        for (int i = 0; i < 5; i++)
        {
            Define.CardSuit suit1 = isBlack ? CardSuit.Spade : CardSuit.Heart;
            Define.CardSuit suit2 = isBlack ? CardSuit.Club : CardSuit.Diamond;

            CardInfo card = Managers.Deck.UsedDeck
                .FirstOrDefault(c =>
                    c.collection == "War of the Roses" &&
                    (SuitHelper.GetEffectiveSuit(c.cardSuit) == suit1 ||
                     SuitHelper.GetEffectiveSuit(c.cardSuit) == suit2));

            if (card == null)
                break;

            Managers.Deck.UsedDeck.Remove(card);
            Managers.Deck.UnUsedDeck.Add(card);
        }
    }

    public void PriestEffect()
    {
        if (IsEffectBlocked())
            return;

        int black = 0;
        int red = 0;

        foreach (var c in Managers.Card.FieldCards)
        {
            var suit = SuitHelper.GetEffectiveSuit(c.cardSuit);
            if (suit == CardSuit.Spade || suit == CardSuit.Club)
                black++;
            else if (suit == CardSuit.Diamond || suit == CardSuit.Heart)
                red++;
        }

        if (isBlack && black > red)
        {
            Managers.Score.CurrentMultiflier *= 1.2f;
        }
        else if (!isBlack && black < red)
        {
            Managers.Score.CurrentMultiflier *= 1.2f;
        }
    }

    // 왕 전용 효과: 붉은/검은 장미 승리 징표 부여 + 상대 왕 영면 처리
    protected void ApplyRedRoseKingEffect()
    {
        int black = 0;
        int red = 0;

        foreach (var c in Managers.Card.FieldCards)
        {
            if (c is WaroftheRosesBase wotr)
            {
                if (wotr.isBlack) black++;
                else red++;
            }
        }

        if (red > black)
        {
            // 징표 - 붉은 장미의 승리 획득 (중복 방지)
            if (!Managers.Effect.Brands.Any(b => b is RedRoseVictoryBrand))
            {
                Managers.Effect.Brands.Add(new RedRoseVictoryBrand());
            }

            // 검은 장미의 왕 영면 처리
            SendKingToRest("Black Rose King");
        }
    }

    protected void ApplyBlackRoseKingEffect()
    {
        int black = 0;
        int red = 0;

        foreach (var c in Managers.Card.FieldCards)
        {
            if (c is WaroftheRosesBase wotr)
            {
                if (wotr.isBlack) black++;
                else red++;
            }
        }

        if (black > red)
        {
            // 징표 - 검은 장미의 승리 획득 (중복 방지)
            if (!Managers.Effect.Brands.Any(b => b is BlackRoseVictoryBrand))
            {
                Managers.Effect.Brands.Add(new BlackRoseVictoryBrand());
            }

            // 붉은 장미의 왕 영면 처리
            SendKingToRest("Red Rose King");
        }
    }

    // 왕 카드를 현재 위치에 상관없이 사용 불가능 상태로 만드는 유틸리티
    private void SendKingToRest(string kingCardName)
    {
        // 1) 필드에 존재하면 조용히 제거 (영면: 효과/파괴 이펙트 발동 없음)
        var fieldCards = Managers.Card.FieldCards;
        for (int i = 0; i < fieldCards.Count; i++)
        {
            CardBase card = fieldCards[i];
            if (card == null) continue;

            if (card.cardName == kingCardName)
            {
                if (card.gameObject != null)
                {
                    Object.Destroy(card.gameObject);
                }
                fieldCards[i] = null;
            }
        }

        // 2) 덱(원본, 미사용, 사용)에서 모두 제거하여 다시는 등장하지 않도록 함
        if (Managers.Deck.Deck != null)
            Managers.Deck.Deck.RemoveAll(ci => ci.cardName == kingCardName);

        if (Managers.Deck.UnUsedDeck != null)
            Managers.Deck.UnUsedDeck.RemoveAll(ci => ci.cardName == kingCardName);

        if (Managers.Deck.UsedDeck != null)
            Managers.Deck.UsedDeck.RemoveAll(ci => ci.cardName == kingCardName);
    }

    public void JokerInfoGatherEffect()
    {
        if (IsEffectBlocked())
            return;

        Debug.Log($"{cardName} Effect");

        foreach (CardBase item in Managers.Card.FieldCards)
        {
            if (item == null)
                continue;

            if (item is WaroftheRosesBase wotr && wotr.isBlack == this.isBlack)
            {
                if (wotr.cardRarity == CardRarity.Epic || wotr.cardRarity == CardRarity.Legendary)
                {
                    if (isBlack)
                    {
                        Managers.Effect.Brands.Add(new BlackRoseInfoGather());
                    }
                    else
                    {
                        Managers.Effect.Brands.Add(new RedRoseInfoGather());
                    }
                    break;
                }
            }
        }
    }

    public void JokerInfoWithdrawEffect()
    {
        if (IsEffectBlocked())
            return;

        Debug.Log($"{cardName} Effect");

        if (isBlack)
        {
            if (Managers.Effect.Brands.Any(b => b is BlackRoseInfoGather))
            {
                var target = Managers.Effect.Brands.FirstOrDefault(b => b is BlackRoseInfoGather);

                if (target != null)
                {
                    Managers.Effect.Brands.Remove(target);
                }
                Managers.Effect.Brands.Add(new BlackRoseInfoWithdraw());
            }
        }
        else
        {
            if (Managers.Effect.Brands.Any(b => b is RedRoseInfoGather))
            {
                var target = Managers.Effect.Brands.FirstOrDefault(b => b is RedRoseInfoGather);

                if (target != null)
                {
                    Managers.Effect.Brands.Remove(target);
                }
                Managers.Effect.Brands.Add(new RedRoseInfoWithdraw());
            }
        }
    }
    #endregion



    #region temporary
    public HitEvent Soilder_Scoring(HitEvent hitEvent)
    {
        // 배율 +5배
        hitEvent.ChangeScaleAdd(5);
        return hitEvent;
    }
    #endregion
}
