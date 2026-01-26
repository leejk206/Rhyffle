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
        // rankRanking ??????

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
    // CardScoring ????? GamePlayer?? resetForFrames???? ?????? touch?? ???? ?????? ??? ?????? ?? ????? ???
    // CardScoring ??????? hitEvents?? ????
    // ?????? ????, ???? ???? ??
    // ????? ??? ?????? ???? ???? ?????? ?????????? OnNoteTrigger?? ???? ???? ???
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
               // OnNoteTrigger?? ??? ???? ???
                hitEvents[i] = cardSets[0].OnNoteTrigger(hitEvents[i]);
            }
            if (hitEvents[i].cardEffect[1])
            {
               // OnNoteTrigger?? ??? ???? ???
                hitEvents[i] = cardSets[1].OnNoteTrigger(hitEvents[i]);
            }
            if (hitEvents[i].cardEffect[2])
            {
               // OnNoteTrigger?? ??? ???? ???
                hitEvents[i] = cardSets[2].OnNoteTrigger(hitEvents[i]);
            }
            if (hitEvents[i].cardEffect[3])
            {
               // OnNoteTrigger?? ??? ???? ???
                hitEvents[i] = cardSets[3].OnNoteTrigger(hitEvents[i]);
            }
            if (hitEvents[i].cardEffect[4])
            {
               // OnNoteTrigger?? ??? ???? ???
                hitEvents[i] = cardSets[4].OnNoteTrigger(hitEvents[i]);
            }
            if (hitEvents[i].cardEffect[5])
            {
               // OnNoteTrigger?? ??? ???? ???
                hitEvents[i] = cardSets[5].OnNoteTrigger(hitEvents[i]);
            }
            if (hitEvents[i].cardEffect[6])
            {
               // OnNoteTrigger?? ??? ???? ???
                hitEvents[i] = cardSets[6].OnNoteTrigger(hitEvents[i]);
            }

            for(int j = 0; j < 7; j++)
            {
                hitEvents[i] = cardSets[j].OnCardExist(hitEvents[i]);
            }
            */            
            // Scoring based on hitEvents
            hitScore = hitEvents[i].GetRank() + (handRankScore[(int)hitEvents[i].GetHandRank()] * hitEvents[i].GetScale());
            Managers.Score.ApplyNoteScore(1, hitEvents[i].GetJudgement(), hitScore);

        }

    }
    
}
