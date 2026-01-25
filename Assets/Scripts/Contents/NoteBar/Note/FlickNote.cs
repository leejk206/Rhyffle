using UnityEngine;

public class FlickNote : Note
{
    public int flicDir;
    public override void drop(float speed)
    {
        base.drop(speed);
    }
    public override void set(int lane, int length)
    {
        base.set(lane, length);
    }
    public void setDir(int dir)
    {
        flicDir = dir;
        if (dir == 0)
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        if (dir == 1)
        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
    }
}
