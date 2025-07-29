using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandManager
{
    public Define.HandRank Evaluate(List<CardBase> cards) // 족보 판정 함수
    {
        if (cards.Count != 7) return Define.HandRank.None; // 카드 7장일 때 기준

        var rankGroups = cards.GroupBy(c => c.cardRank).ToDictionary(g => g.Key, g => g.Count());
        var suitGroups = cards.GroupBy(c => c.cardSuit).ToDictionary(g => g.Key, g => g.Count());

        // 스트레이트 or 플러시 판단
        var rankList = cards.Select(c => (int)c.cardRank).ToList();
        bool hasFlush = suitGroups.Any(s => s.Value >= 5);
        bool hasStraight = HasStraight(rankList, 5);
        bool hasRainbow = HasStraight(rankList, 7);
        bool hasStraightFlush = HasStraightFlush(cards);

        // 랭크가 같은 카드 수
        int pairCount = rankGroups.Count(kv => kv.Value == 2);
        int tripleCount = rankGroups.Count(kv => kv.Value == 3);
        int fourCardCount = rankGroups.Count(kv => kv.Value == 4);

        // 족보 판정 - 우선순위 순서대로
        if (fourCardCount == 1 && tripleCount >= 1)
        {
            Debug.Log("족보: 럭키 세븐");
            return Define.HandRank.LuckySeven;
        }

        if (tripleCount >= 2)
        {
            Debug.Log("족보: 더블 트리플");
            return Define.HandRank.DoubleTriple;
        }

        if (hasStraightFlush)
        {
            Debug.Log("족보: 스트레이트 플러시");
            return Define.HandRank.StraightFlush;
        }

        if (hasRainbow)
        {
            Debug.Log("족보: 레인보우");
            return Define.HandRank.Rainbow;
        }

        if (fourCardCount == 1)
        {
            Debug.Log("족보: 포 카드");
            return Define.HandRank.FourCard;
        }

        if (tripleCount == 1 && pairCount >= 1)
        {
            Debug.Log("족보: 풀 하우스");
            return Define.HandRank.FullHouse;
        }

        if (pairCount >= 3)
        {
            Debug.Log("족보: 쓰리 페어");
            return Define.HandRank.ThreePair;
        }

        if (hasFlush)
        {
            Debug.Log("족보: 플러시");
            return Define.HandRank.Flush;
        }

        if (hasStraight)
        {
            Debug.Log("족보: 스트레이트");
            return Define.HandRank.Straight;
        }

        if (tripleCount == 1)
        {
            Debug.Log("족보: 트리플");
            return Define.HandRank.Triple;
        }

        if (pairCount == 2)
        {
            Debug.Log("족보: 투 페어");
            return Define.HandRank.TwoPair;
        }

        if (pairCount == 1)
        {
            Debug.Log("족보: 원 페어");
            return Define.HandRank.OnePair;
        }

        return Define.HandRank.None;
    }

    private bool HasStraight(List<int> ranks, int length) // length만큼 스트레이트 여부 판단 함수
    {
        var uniqueRanks = ranks.Distinct().ToList();
        if (uniqueRanks.Contains(1)) uniqueRanks.Add(14);
        uniqueRanks.Sort();

        int consecutive = 1;
        for (int i = 1; i < uniqueRanks.Count; i++)
        {
            if (uniqueRanks[i] == uniqueRanks[i - 1] + 1)
            {
                consecutive++;
                if (consecutive >= length) return true;
            }
            else if (uniqueRanks[i] != uniqueRanks[i - 1]) consecutive = 1;
        }
        return false;
    }

    private bool HasStraightFlush(List<CardBase> cards) // 스트레이트 플러시 여부 판단 함수
    {
        var grouped = cards.GroupBy(c => c.cardSuit);
        foreach (var group in grouped)
        {
            if (group.Count() >= 5)
            {
                var suitedRanks = group.Select(c => (int)c.cardRank).ToList();
                if (HasStraight(suitedRanks, 5)) return true;
            }
        }
        return false;
    }
}
