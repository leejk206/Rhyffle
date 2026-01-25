using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using static Define;
using static HitEvent;

public class GameScoreInfo : MonoBehaviour
{
    public float[] handRankScore = new float[12];
    // Base hand rank when applying score
    // Need to be resetted every time card is newly drawn.
    public HandRank baseHandRank = HandRank.None;

    public float[] scaleAdd = new float[7];
    public float[] scaleMulti = new float[7];

    public void SetUp()
    {
        // rankRanking 기본점수
    }

    // With new Hand, Set new hand rank
    public void SetNewHandRank(HandRank handRank)
    {
        baseHandRank = handRank;
    }

    // For Changing on HandRankScore
    public void ChangeHandRankScore(HandRank handRank, float mult)
    {
        handRankScore[(int)handRank] = mult;
    }

    // Used when calling game without cards
    public void NormalScoring()
    {
        
    }

    // Used when calling game with cards
    public void CardScoring(List<HitEvent> hitEvents)
    {
        float hitScore;
        // for each hitEvents
        for (int i = 0; i < hitEvents.Count; i++) {
            // apply baseHandRank
            hitEvents[i].ChangeHandRank(baseHandRank);
            // Apply Cards from left card to right card
            // leftmost card
            if (hitEvents[i].cardEffect[0]) {
                // 카드 효과 적용 후

            }
            if (hitEvents[i].cardEffect[1])
            {
                // 카드 효과 적용 후

            }
            if (hitEvents[i].cardEffect[2])
            {
                // 카드 효과 적용 후

            }
            if (hitEvents[i].cardEffect[3])
            {
                // 카드 효과 적용 후

            }
            if (hitEvents[i].cardEffect[4])
            {
                // 카드 효과 적용 후

            }
            if (hitEvents[i].cardEffect[5])
            {
                // 카드 효과 적용 후

            }
            if (hitEvents[i].cardEffect[6])
            {
                // 카드 효과 적용 후

            }
            // Scoring based on hitEvents
            hitScore = handRankScore[(int)hitEvents[i].GetHandRank()] * hitEvents[i].GetScale();
            Managers.Score.ApplyNoteScore(1, hitEvents[i].GetJudgement(), hitScore);

        }

    }
    
}
