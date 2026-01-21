using UnityEngine;
using static Define;

public class HoldNote : Note
{
    bool isCheck = false;
    public override void Set(int lane, int length)
    {
        base.Set(lane, length);
    }
    public override void Set(int lane, int length, float height)
    {
        base.Set(lane, length, height);
    }

    public override void Drop(float speed)
    {
        base.Drop(speed);
    }
    public override void SetJudge(int judge)
    {
        base.SetJudge(judge);
    }

    public override JudgementType ReadJudge(int lane, float bpm, int checkType, float curTime)
    {
        return base.ReadJudge(lane, bpm, checkType, curTime);
    }
}
