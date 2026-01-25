using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlackRoseQueen : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.Queen;
        cardRarity = Define.CardRarity.Epic;
        cardName = "Black Rose Queen";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

        Debug.Log("init");
    }

    public override void OnCardDrawComplete()
    {
        if (IsCurrentBestHandComposedOnlyOfSpadesOrClubs(out _, out _))
        {
            foreach (CardBase card in Managers.Card.FieldCards)
            {
                card.ScaleAdd += 4;
            }
        }

    }

    #region SuitHelper
    // =========================
    // Black Rose Queen(요구사항용) 유틸
    // - "현재 필드의 가장 높은 족보"를 기준으로,
    // - 그 족보를 구성하는 카드들의 "유효 무늬"(SuitHelper 적용)를 뽑아내고,
    // - 그 구성 카드가 스페이드/클럽만인지까지 판정
    // =========================

    private const int RequiredFieldCardCount = 7;

    /// <summary>
    /// 현재 필드(7장)가 만들어낸 "가장 높은 족보"를 구성하는 카드들의 유효 무늬 리스트를 반환.
    /// (족보 판정 우선순위는 HandManager.Evaluate 결과를 따름)
    /// </summary>
    private bool TryGetCurrentBestHandComposingEffectiveSuits(out Define.HandRank bestRank, out List<Define.CardSuit> suits)
    {
        suits = new List<Define.CardSuit>();
        bestRank = Define.HandRank.None;

        if (!TryGetCurrentFieldCards(out var fieldCards))
            return false;

        bestRank = Managers.Hand.Evaluate(fieldCards);
        if (!TryComposeCardsForRank(fieldCards, bestRank, out var composingCards))
            return false;

        suits = composingCards
            .Select(c => SuitHelper.GetEffectiveSuit(c.cardSuit))
            .ToList();

        return true;
    }

    /// <summary>
    /// "현재 가장 높은 족보"가 (그 족보를 구성하는 카드 기준으로) 스페이드/클럽만으로 이루어졌는지 판정.
    /// </summary>
    private bool IsCurrentBestHandComposedOnlyOfSpadesOrClubs(out Define.HandRank bestRank, out List<CardBase> composingCards)
    {
        bestRank = Define.HandRank.None;
        composingCards = new List<CardBase>();

        if (!TryGetCurrentFieldCards(out var fieldCards))
            return false;

        bestRank = Managers.Hand.Evaluate(fieldCards);

        bool Allowed(Define.CardSuit s) => s == Define.CardSuit.Spade || s == Define.CardSuit.Club;

        // "가장 높은 족보(bestRank)"를 "스페이드/클럽 무늬 카드만"으로도 구성할 수 있는지 시도
        var allowedSuitCardsOnly = fieldCards
            .Where(c => Allowed(SuitHelper.GetEffectiveSuit(c.cardSuit)))
            .ToList();

        if (!TryComposeCardsForRank(allowedSuitCardsOnly, bestRank, out composingCards))
            return false;

        // 안전장치: 구성 카드가 정말 전부 스페이드/클럽 무늬인지 확인
        return composingCards.All(c => Allowed(SuitHelper.GetEffectiveSuit(c.cardSuit)));
    }

    private bool TryGetCurrentFieldCards(out List<CardBase> fieldCards)
    {
        fieldCards = null;

        if (Managers.Card == null || Managers.Card.FieldCards == null)
            return false;

        fieldCards = Managers.Card.FieldCards.Where(c => c != null).ToList();
        return fieldCards.Count == RequiredFieldCardCount;
    }

    /// <summary>
    /// 주어진 cards(필터링된 카드 목록 가능)에서 특정 rank를 "구성"하는 카드들을 뽑아냄.
    /// </summary>
    private static bool TryComposeCardsForRank(List<CardBase> cards, Define.HandRank rank, out List<CardBase> composed)
    {
        composed = new List<CardBase>();
        if (cards == null)
            return false;

        // rank 판정은 외부에서 이미 끝났다고 가정하지만,
        // 구성은 "가능한 경우"에만 반환.
        var rankGroups = cards
            .GroupBy(c => c.cardRank)
            .ToDictionary(g => g.Key, g => g.ToList());

        switch (rank)
        {
            case Define.HandRank.LuckySeven:
            {
                // 4장 + 3장 = 7장
                var quad = rankGroups.Where(kv => kv.Value.Count == 4).OrderByDescending(kv => (int)kv.Key).FirstOrDefault();
                var triple = rankGroups.Where(kv => kv.Value.Count == 3).OrderByDescending(kv => (int)kv.Key).FirstOrDefault();
                if (quad.Value == null || triple.Value == null)
                    return false;

                composed.AddRange(quad.Value);
                composed.AddRange(triple.Value);
                return composed.Count == 7;
            }
            case Define.HandRank.DoubleTriple:
            {
                var triples = rankGroups
                    .Where(kv => kv.Value.Count == 3)
                    .OrderByDescending(kv => (int)kv.Key)
                    .Take(2)
                    .SelectMany(kv => kv.Value)
                    .ToList();

                if (triples.Count != 6)
                    return false;

                composed = triples;
                return true;
            }
            case Define.HandRank.StraightFlush:
            {
                if (!TryGetStraightFlushCards(cards, out composed))
                    return false;
                return composed.Count == 5;
            }
            case Define.HandRank.Rainbow:
            {
                // HandManager 기준: 7장 스트레이트(레인보우) = 사실상 7장 모두가 구성
                if (cards.Count != 7)
                    return false;
                composed = cards.ToList();
                return true;
            }
            case Define.HandRank.FourCard:
            {
                var quad = rankGroups.Where(kv => kv.Value.Count == 4).OrderByDescending(kv => (int)kv.Key).FirstOrDefault();
                if (quad.Value == null)
                    return false;
                composed = quad.Value.ToList(); // 패턴 구성 카드(4장)
                return true;
            }
            case Define.HandRank.FullHouse:
            {
                var triple = rankGroups.Where(kv => kv.Value.Count == 3).OrderByDescending(kv => (int)kv.Key).FirstOrDefault();
                if (triple.Value == null)
                    return false;

                var pair = rankGroups
                    .Where(kv => kv.Key != triple.Key && kv.Value.Count >= 2)
                    .OrderByDescending(kv => (int)kv.Key)
                    .FirstOrDefault();

                if (pair.Value == null)
                    return false;

                composed.AddRange(triple.Value.Take(3));
                composed.AddRange(pair.Value.Take(2));
                return composed.Count == 5;
            }
            case Define.HandRank.ThreePair:
            {
                var pairs = rankGroups
                    .Where(kv => kv.Value.Count == 2)
                    .OrderByDescending(kv => (int)kv.Key)
                    .Take(3)
                    .SelectMany(kv => kv.Value)
                    .ToList();

                if (pairs.Count != 6)
                    return false;

                composed = pairs;
                return true;
            }
            case Define.HandRank.Flush:
            {
                if (!TryGetFlushCards(cards, out composed))
                    return false;
                return composed.Count == 5;
            }
            case Define.HandRank.Straight:
            {
                if (!TryGetStraightCards(cards, 5, out composed))
                    return false;
                return composed.Count == 5;
            }
            case Define.HandRank.Triple:
            {
                var triple = rankGroups.Where(kv => kv.Value.Count == 3).OrderByDescending(kv => (int)kv.Key).FirstOrDefault();
                if (triple.Value == null)
                    return false;
                composed = triple.Value.Take(3).ToList();
                return true;
            }
            case Define.HandRank.TwoPair:
            {
                var pairs = rankGroups
                    .Where(kv => kv.Value.Count == 2)
                    .OrderByDescending(kv => (int)kv.Key)
                    .Take(2)
                    .SelectMany(kv => kv.Value)
                    .ToList();

                if (pairs.Count != 4)
                    return false;
                composed = pairs;
                return true;
            }
            case Define.HandRank.OnePair:
            {
                var pair = rankGroups.Where(kv => kv.Value.Count == 2).OrderByDescending(kv => (int)kv.Key).FirstOrDefault();
                if (pair.Value == null)
                    return false;
                composed = pair.Value.Take(2).ToList();
                return true;
            }
            case Define.HandRank.None:
            default:
                return false;
        }
    }

    private static bool TryGetFlushCards(List<CardBase> cards, out List<CardBase> flushCards)
    {
        flushCards = new List<CardBase>();
        var suitedGroups = cards
            .GroupBy(c => SuitHelper.GetEffectiveSuit(c.cardSuit))
            .OrderByDescending(g => g.Count())
            .ToList();

        var group = suitedGroups.FirstOrDefault(g => g.Count() >= 5);
        if (group == null)
            return false;

        // 구성용: 같은 무늬 중 높은 랭크 5장
        flushCards = group
            .OrderByDescending(c => (int)c.cardRank)
            .Take(5)
            .ToList();
        return flushCards.Count == 5;
    }

    private static bool TryGetStraightFlushCards(List<CardBase> cards, out List<CardBase> straightFlushCards)
    {
        straightFlushCards = new List<CardBase>();

        var suitedGroups = cards
            .GroupBy(c => SuitHelper.GetEffectiveSuit(c.cardSuit))
            .Where(g => g.Count() >= 5)
            .ToList();

        foreach (var group in suitedGroups)
        {
            var suitedCards = group.ToList();
            if (TryGetStraightCards(suitedCards, 5, out straightFlushCards))
                return straightFlushCards.Count == 5;
        }

        return false;
    }

    private static bool TryGetStraightCards(List<CardBase> cards, int length, out List<CardBase> straightCards)
    {
        straightCards = new List<CardBase>();
        if (cards == null || cards.Count == 0)
            return false;

        var ranks = cards.Select(c => (int)c.cardRank).ToList();
        if (!TryGetBestStraightRanks(ranks, length, out var straightRanks))
            return false;

        // ranks에 대응되는 카드 1장씩 선택
        var remaining = cards.ToList();
        foreach (var r in straightRanks)
        {
            int wanted = (r == 14) ? 1 : r; // Ace-high 표현(14)은 실제 카드에서는 Ace(1)
            var pick = remaining.FirstOrDefault(c => (int)c.cardRank == wanted);
            if (pick == null)
                return false;
            straightCards.Add(pick);
            remaining.Remove(pick);
        }

        return straightCards.Count == length;
    }

    /// <summary>
    /// HandManager.HasStraight와 동일한 방식으로 "가장 높은" 스트레이트 rank 시퀀스를 뽑아냄.
    /// 예: A는 1로 들어오며, 내부에서 14(A-high)도 추가해서 판정.
    /// </summary>
    private static bool TryGetBestStraightRanks(List<int> ranks, int length, out List<int> straightRanks)
    {
        straightRanks = new List<int>();
        if (ranks == null)
            return false;

        var uniqueRanks = ranks.Distinct().ToList();
        if (uniqueRanks.Contains(1))
            uniqueRanks.Add(14);
        uniqueRanks.Sort();

        int consecutive = 1;
        int bestEndIndex = -1;

        for (int i = 1; i < uniqueRanks.Count; i++)
        {
            if (uniqueRanks[i] == uniqueRanks[i - 1] + 1)
            {
                consecutive++;
            }
            else if (uniqueRanks[i] != uniqueRanks[i - 1])
            {
                consecutive = 1;
            }

            if (consecutive >= length)
            {
                // 가능한 스트레이트 중 "가장 높은 것"을 선택 (끝점이 가장 큰 것)
                bestEndIndex = i;
            }
        }

        if (bestEndIndex < 0)
            return false;

        int start = bestEndIndex - length + 1;
        straightRanks = uniqueRanks.GetRange(start, length);
        return true;
    }
    #endregion
}
