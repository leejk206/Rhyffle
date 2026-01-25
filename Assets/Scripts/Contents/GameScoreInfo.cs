using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using static Define;
using static HitEvent;
using System.Linq;

public class GameScoreInfo : MonoBehaviour
{
    // Basic hand ranking score
    public float[] handRankScore = new float[13];
    // Affect all hand ranking score
    public float allRankBonus = 0;
    // Base hand rank when applying score
    // Need to be resetted every time card is newly drawn.
    public HandRank baseHandRank = HandRank.None;

    public float[] scaleAdd = new float[7];
    public float[] scaleMulti = new float[7];
    CardBase[] cardSets = new CardBase[7];

    public void SetUp()
    {
        // rankRanking 기본점수

        handRankScore = new float[]{1, 1.011f, 1.02211f, 1.1f, 1.1f, 1.11f, 1.554f, 1.32f, 4.44f, 13.76f, 15.0f, 33.22f, 100f };

    }
    public void SetCard()
    {
        for(int i = 0; i < 7; i++)
        {
            cardSets[i] = Managers.Card.FieldCards[i];
        }
    }
    // With new Hand, Set new hand rank
    public void SetNewHandRank(HandRank handRank)
    {
        baseHandRank = Managers.Hand.Evaluate(Managers.Card.FieldCards.Where(c => c != null).ToList());
    }

    // with bonus applied to all of the ranks
    public void ChangeAllRankBonus(float allRank)
    {
        allRankBonus = + allRank;
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
    // CardScoring 함수는 GamePlayer의 resetForFrames에서 각각의 touch에 대한 리셋을 하기 이전에 이 함수를 호출
    // CardScoring 함수에서 hitEvents를 받아옴
    // 기본적인 족보, 판정 적용 후
    // 노트가 걸친 지역에 따라서 왼쪽 카드부터 순차적으로 OnNoteTrigger를 통해 배율 계산
    public void CardScoring(List<HitEvent> hitEvents)
    {
        float hitScore;
        // for each hitEvents
        for (int i = 0; i < hitEvents.Count; i++) {
            
            // apply baseHandRank
            hitEvents[i].ChangeHandRank(baseHandRank);
            hitEvents[i].SetScale(handRankScore[(int)baseHandRank] + allRankBonus);
            hitEvents[i].ResetRank();

            /*
            // Apply Cards from left card to right card
            // leftmost card
            if (hitEvents[i].cardEffect[0]) {
               // OnNoteTrigger로 카드 배율 변화
                hitEvents[i] = cardSets[0].OnNoteTrigger(hitEvents[i]);
            }
            if (hitEvents[i].cardEffect[1])
            {
               // OnNoteTrigger로 카드 배율 변화
                hitEvents[i] = cardSets[1].OnNoteTrigger(hitEvents[i]);
            }
            if (hitEvents[i].cardEffect[2])
            {
               // OnNoteTrigger로 카드 배율 변화
                hitEvents[i] = cardSets[2].OnNoteTrigger(hitEvents[i]);
            }
            if (hitEvents[i].cardEffect[3])
            {
               // OnNoteTrigger로 카드 배율 변화
                hitEvents[i] = cardSets[3].OnNoteTrigger(hitEvents[i]);
            }
            if (hitEvents[i].cardEffect[4])
            {
               // OnNoteTrigger로 카드 배율 변화
                hitEvents[i] = cardSets[4].OnNoteTrigger(hitEvents[i]);
            }
            if (hitEvents[i].cardEffect[5])
            {
               // OnNoteTrigger로 카드 배율 변화
                hitEvents[i] = cardSets[5].OnNoteTrigger(hitEvents[i]);
            }
            if (hitEvents[i].cardEffect[6])
            {
               // OnNoteTrigger로 카드 배율 변화
                hitEvents[i] = cardSets[6].OnNoteTrigger(hitEvents[i]);
            }

            for(int j = 0; j < 7; j++)
            {
                hitEvents[i] = cardSets[j].OnCardExist(hitEvents[i]);
            }
            */            
            // Scoring based on hitEvents
            hitScore = handRankScore[(int)hitEvents[i].GetHandRank()] * hitEvents[i].GetScale();
            Managers.Score.ApplyNoteScore(1, hitEvents[i].GetJudgement(), hitScore);

        }

    }
    
}
