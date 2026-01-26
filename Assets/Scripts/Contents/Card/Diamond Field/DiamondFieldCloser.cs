using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiamondFieldCloser : DiamondFieldBase
{
    // Born to K: 좌/우 카드의 "점수 랭크(CardRank)"를 K로 취급하기 위한 백업
    private readonly Dictionary<CardBase, int> _bornToKBackupRanks = new();

    public override void Init()
    {
        base.Init();


        cardBaseId = 125; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.King;
        cardRarity = Define.CardRarity.Epic;
        cardName = "Diamond Field Closer";
        collection = "Diamond Field";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

    }

    public override void OnCardDrawComplete()
    {
        base.OnCardDrawComplete();

        ApplyBornToKToAdjacentCards();

        // K 포카드가 성립하면(Closer 존재 + BornToK 적용 후) 족보 배율 보너스
        if (IsKingFourOfAKindCompleteByBornToK())
        {
            ScaleAdd += 4.9f;
        }
    }

    public override void OnCardDestroy()
    {
        base.OnCardDestroy();
        RestoreBornToKAdjacentCards();
    }


    #region Helpers
    private void ApplyBornToKToAdjacentCards()
    {
        RestoreBornToKAdjacentCards();

        var field = Managers.Card?.FieldCards;
        if (field == null) return;

        int idx = SlotIndex;
        var neighborIndices = new List<int>(2);
        if (idx - 1 >= 0) neighborIndices.Add(idx - 1);
        if (idx + 1 <= 6) neighborIndices.Add(idx + 1);

        foreach (var nIdx in neighborIndices)
        {
            var neighbor = field[nIdx];
            if (neighbor == null) continue;

            if (!_bornToKBackupRanks.ContainsKey(neighbor))
                _bornToKBackupRanks[neighbor] = neighbor.CardRank;

            neighbor.CardRank = (int)Define.CardRank.King;
        }
    }

    private void RestoreBornToKAdjacentCards()
    {
        if (_bornToKBackupRanks.Count == 0) return;

        foreach (var kv in _bornToKBackupRanks.ToList())
        {
            var card = kv.Key;
            if (card == null) continue;
            card.CardRank = kv.Value;
        }

        _bornToKBackupRanks.Clear();
    }

    private bool IsKingFourOfAKindCompleteByBornToK()
    {
        var field = Managers.Card?.FieldCards;
        if (field == null) return false;

        int idx = SlotIndex;
        bool IsNeighbor(int i) => i == idx - 1 || i == idx + 1;

        int kingCount = 0;
        for (int i = 0; i < field.Count; i++)
        {
            var c = field[i];
            if (c == null) continue;

            // 족보 판정 관점에서는 cardRank가 원본인데,
            // BornToK로 "옆 카드 랭크를 K로 취급"하므로 여기서는 효과 랭크로 계산한다.
            var effectiveRank = IsNeighbor(i) ? Define.CardRank.King : c.cardRank;
            if (effectiveRank == Define.CardRank.King)
                kingCount++;
        }

        return kingCount >= 4;
    }
    #endregion
}
