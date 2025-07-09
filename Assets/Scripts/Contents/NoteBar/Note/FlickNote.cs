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
}
