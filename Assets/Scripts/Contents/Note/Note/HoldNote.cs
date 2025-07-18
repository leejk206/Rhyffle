using UnityEngine;

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
    public void HideNote()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void ShowNote()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled=true;
    }

    public bool isChecked()
    {
        return isCheck;
    }

}
