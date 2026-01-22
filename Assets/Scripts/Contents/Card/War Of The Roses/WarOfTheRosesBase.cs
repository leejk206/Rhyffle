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
    public void GetRankBonus()
    {
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
        int left = 0;
        int right = Managers.Deck.UnUsedDeck.Count - 1;
        int swaps = 0;

        while (left < right && swaps < 5)
        {
            if (isBlack)
            {
                // 왼쪽에서 다음 '빨간 카드'(검은 카드를 건너뜀) 찾기
                while (left < right && (Managers.Deck.UnUsedDeck[left].cardSuit == Define.CardSuit.Spade || Managers.Deck.UnUsedDeck[left].cardSuit == Define.CardSuit.Club)) left++;

                // 오른쪽에서 다음 '검은 카드'(빨간 카드를 건너뜀) 찾기
                while (left < right && (Managers.Deck.UnUsedDeck[right].cardSuit == Define.CardSuit.Diamond || Managers.Deck.UnUsedDeck[right].cardSuit == Define.CardSuit.Heart)) right--;
            }
            else
            {
                // 왼쪽에서 다음 '검은 카드'(빨간 카드를 건너뜀) 찾기
                while (left < right && (Managers.Deck.UnUsedDeck[left].cardSuit == Define.CardSuit.Heart || Managers.Deck.UnUsedDeck[left].cardSuit == Define.CardSuit.Diamond)) left++;

                // 오른쪽에서 다음 '빨간 카드'(검은 카드를 건너뜀) 찾기
                while (left < right && (Managers.Deck.UnUsedDeck[right].cardSuit == Define.CardSuit.Spade || Managers.Deck.UnUsedDeck[right].cardSuit == Define.CardSuit.Club)) right--;
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
                        (x.cardSuit == suit1 || x.cardSuit == suit2));

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
        for (int i = 0; i < 5; i++)
        {
            Define.CardSuit suit1 = isBlack ? CardSuit.Spade : CardSuit.Heart;
            Define.CardSuit suit2 = isBlack ? CardSuit.Club : CardSuit.Diamond;

            CardInfo card = Managers.Deck.UsedDeck
                .FirstOrDefault(c => c.collection == "War of the Roses" && (c.cardSuit == suit1 || c.cardSuit == suit2));

            if (card == null)
                break;

            Managers.Deck.UsedDeck.Remove(card);
            Managers.Deck.UnUsedDeck.Add(card);
        }
    }

    public void PriestEffect()
    {
        int black = 0;
        int red = 0;

        foreach (var c in Managers.Card.FieldCards)
        {
            if (c.cardSuit == CardSuit.Spade || c.cardSuit == CardSuit.Club)
                black++;
            else if (c.cardSuit == CardSuit.Diamond || c.cardSuit == CardSuit.Heart)
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
    #endregion

}
