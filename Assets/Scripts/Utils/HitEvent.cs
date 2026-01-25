using UnityEngine;
using static Define;

public class HitEvent
{
    // judge
    Define.JudgementType judge;
    // hand ranking
    Define.HandRank handRank;
    // leftMost lane
    int minRange;
    // rightMost lane
    int maxRange;

    // scale은 기본값이 족보의 배율이고 추가될 수 있음
    float scale = 1;
    // rank는 말 그대로 카드들의 rank
    int rank = 0;

    // which card this card will be affected
    public bool[] cardEffect = new bool[7];
    public HitEvent(Define.JudgementType judge, int min, int max)
    {
        handRank = Define.HandRank.None;
        scale = 1;
        rank = 0;
        this.judge = judge;
        minRange = min;
        maxRange = max;
        #region cardRange
        if (minRange < 3)
        {
            cardEffect[0] = true;
        }
        if (minRange < 6)
        {
            cardEffect[1] = true;
        }
        if (minRange < 9)
        {
            cardEffect[2] = true;
        }
        if (minRange < 12)
        {
            cardEffect[3] = true;
        }
        if (minRange < 15)
        {
            cardEffect[4] = true;
        }
        if (minRange < 18)
        {
            cardEffect[5] = true;
        }
        cardEffect[6] = true;
        if (maxRange < 18)
        {
            cardEffect[6] = false;
        }
        if (maxRange < 15)
        {
            cardEffect[5] = false;
        }
        if (maxRange < 12)
        {
            cardEffect[4] = false;
        }
        if (maxRange < 9)
        {
            cardEffect[3] = false;
        }
        if (maxRange < 6)
        {
            cardEffect[2] = false;
        }
        if (maxRange < 3)
        {
            cardEffect[1] = false;
        }
        #endregion
    }

    // scale Set to the hand rankings
    public void SetScale(float rankBonus)
    {
        scale = rankBonus;
    }
    public void ResetRank()
    {
        rank = 0;
    }

    // scale addition
    public void AddScaleAdd(float change)
    {
        scale += change;
    }
    // scale multiplication
    public void AddScaleMult(float change)
    {
        scale *= change;
    }

    // handrank change
    public void ChangeHandRank(HandRank newHandRank)
    {
        handRank = newHandRank;
    }

    // judge change
    public void ChangeJudge(JudgementType newJudge)
    {
        judge = newJudge;
    }

    public void AddRank(int newRank)
    {
        rank += newRank;
    }

    public JudgementType GetJudgement() { return judge; }

    public HandRank GetHandRank() { return handRank; }

    public float GetScale() {  return scale; }

    public int GetRank() { return rank; }   
}
