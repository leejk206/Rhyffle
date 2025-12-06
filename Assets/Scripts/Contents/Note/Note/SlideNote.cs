using UnityEngine;

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

    public override int ReadJudge(int lane, int bpm, int checkType, float curTime)
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
                    return 0;
                }else if(curTime > judge - (float)bpm / 600 * 16)
                {
                    return 4;
                }else if(curTime > judge - (float)bpm / 600 * 1.5f * 16)
                {
                    return 3;
                }else if(curTime > judge - (float)bpm/600 * 2 * 16)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            return 0;
            
        }else
        {
            return 0;
        }
    }
}
