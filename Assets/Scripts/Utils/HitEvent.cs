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

    float scale;

    // which card this card will be affected
    public bool[] cardEffect = new bool[7];
    public HitEvent(Define.JudgementType judge, int min, int max)
    {
        handRank = Define.HandRank.None;
        scale = 1;
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

    // scale addition
    public void ChangeScaleAdd(float change)
    {
        scale += change;
    }
    // scale multiplication
    public void ChangeScaleMult(float change)
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

    public JudgementType GetJudgement() { return judge; }

    public HandRank GetHandRank() { return handRank; }

    public float GetScale() {  return scale; }
}
