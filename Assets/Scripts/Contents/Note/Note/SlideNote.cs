using UnityEngine;
using static Define;

public class SlideNote : Note
{
    public override void Drop(float speed)
    {
        base.Drop(speed);
    }
    public override void Set(int lane, int length)
    {
        base.Set(lane, length);
    }
    public override void SetJudge(int judge)
    {
        base.SetJudge(judge);
    }

    public override JudgementType ReadJudge(int lane, float bpm, int checkType, float curTime)
    {
        if (checkType == 1 || checkType == 0)
        {
            return base.ReadJudge(lane, bpm, checkType, curTime);
        }
        else if (checkType == 2)
        {
            if(lane >= line && lane <= line + length)
            {
                if(curTime < judge - (float)bpm / 600 * 16)
                {
                    return JudgementType.Checked;
                }else if(curTime > judge - (float)bpm / 600 * 16)
                {
                    return JudgementType.Perfect;
                }else if(curTime > judge - (float)bpm / 600 * 1.5f * 16)
                {
                    return JudgementType.Great;
                }else if(curTime > judge - (float)bpm/600 * 2 * 16)
                {
                    return JudgementType.Good;
                }
                else
                {
                    return JudgementType.Miss;
                }
            }
            return JudgementType.NotChecked;
            
        }else
        {
            return JudgementType.NotChecked;
        }
    }
}
