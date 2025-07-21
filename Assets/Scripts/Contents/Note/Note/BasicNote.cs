using UnityEngine;

public class BasicNote : Note
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
        if (checkType == 0 || checkType == 1)
        {
            return base.ReadJudge(lane, bpm, checkType, curTime);
        }
        else
        {
            return 0;
        }
    }
}
