using UnityEngine;

public class FlickNote : Note
{
    public int flicDir;
    public override void Drop(float speed)
    {
        base.Drop(speed);
    }
    public override void Set(int lane, int length)
    {
        base.Set(lane, length);
    }
    public void SetDir(int dir)
    {
        flicDir = dir;
        if (dir == 0)
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        if (dir == 1)
        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
    }

    public override int ReadJudge(int lane, int bpm, int checkType, float curTime)
    {
        if(checkType == 0)
        {
            return base.ReadJudge(lane, bpm, checkType, curTime);
        }
        if (flicDir == 0 && checkType == 5)
        {
            return base.ReadJudge(lane, bpm, checkType, curTime);
        }
        else if (flicDir == 1 && checkType == 6) {
            return base.ReadJudge(lane, bpm, checkType, curTime);
        }
        else
        {
            return 0;
        }
    }

    public override void SetJudge(int judge)
    {
        base.SetJudge(judge);
    }
}
