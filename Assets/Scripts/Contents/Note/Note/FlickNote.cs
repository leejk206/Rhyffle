using UnityEngine;

public class FlickNote : Note
{
    public int flicDir;
    
    // FlickUp & Down 구분
    public Sprite flickUpSprite;
    public Sprite flickDownSprite;

    private Vector3 _upOffset = new Vector3(0, 0.3f, 0);
    private Vector3 _downOffset = new Vector3(0, -0.3f, 0);

    private SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
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
        
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (flicDir == 0) // Flick Down
        {
            spriteRenderer.sprite = flickDownSprite;
            transform.localPosition = _downOffset;
        }
        else if (flicDir == 1) // Flick Up
        {
            spriteRenderer.sprite = flickUpSprite;
            transform.localPosition = _upOffset;
        }
        
        // if (dir == 0) gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        // if (dir == 1) gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
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
