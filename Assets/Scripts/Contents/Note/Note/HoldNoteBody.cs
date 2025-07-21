using UnityEngine;
using System.Collections.Generic;

public class HoldNoteBody : Note
{
    List<HoldNote> holdNotes = new List<HoldNote>();
    List<BoxCollider2D> colliders = new List<BoxCollider2D>();
    public LineRenderer lineRenderer;
    public int curJudge = 0;

    // Draw Line에서 사용
    Vector3 drawVector = Vector3.zero;
    float x, y;

    public override void Drop(float speed)
    {
        for (int i = 0; i < holdNotes.Count; i++) {
            holdNotes[i].Drop(speed);
        }
        DrawLine();
    }

    public override int ReadJudge(int lane, int bpm, int checkType, float curTime)
    {
        
        if (curJudge == 0)
        {
            if(checkType == 1 || checkType == 0)
            {
                int temp = holdNotes[0].ReadJudge(lane, bpm,checkType, curTime);
                if ((temp!=0))
                {
                    curJudge++;
                }
                return temp;
            }
            else { return 0; }
        }
        else if (curJudge == holdNotes.Count - 1) {
            if (checkType == 4 || checkType == 0)
            {
                return holdNotes[curJudge].ReadJudge(lane, bpm, checkType, curTime);
            }
            else
            {
                return 0;
            }
        }
        else
        {
            if(checkType == 0)
            {
                return holdNotes[curJudge].ReadJudge(lane,bpm,checkType, curTime);
            }else if(checkType == 3)
            {
                if(lane >= holdNotes[curJudge].line && lane <= holdNotes[curJudge].line + holdNotes[curJudge].length)
                {
                    int tempJudge = holdNotes[curJudge].judge;
                    if(curTime < tempJudge - (float)bpm / 600 * 16) 
                    {
                        return 0;
                    }
                    else if (curTime < tempJudge + (float)bpm / 600 * 16)
                    {
                        curJudge++;
                        return 4;
                    }else if(curTime < tempJudge + (float)bpm / 600 * 1.5f * 16)
                    {
                        curJudge++;
                        return 3;
                    }else if (curTime < tempJudge + (float)bpm/600 *2f * 16)
                    {
                        curJudge++;
                        return 2;
                    }
                    else
                    {
                        curJudge++;
                        return 1;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }
        return base.ReadJudge(lane, bpm, checkType, curTime);
    }

    public void AddNotes(HoldNote holdNote)
    {
        holdNotes.Add(holdNote);
        colliders.Add(holdNote.gameObject.GetComponent<BoxCollider2D>());
        lineRenderer.positionCount++;
    }
    public void ResetNotes()
    {
        for (int i = 0; i < holdNotes.Count; i++) {
            Destroy(holdNotes[i].gameObject);
        }
        holdNotes.Clear();
        colliders.Clear();
        curJudge = 0;
        lineRenderer.positionCount = 0;
    }

    // 매 프레임마다 실행되어 줄을 그어주는 함수
    public void DrawLine()
    {
        for(int i = 0; i < colliders.Count; i++)
        {
            x = (colliders[i].bounds.max.x + colliders[i].bounds.min.x)/2;
            y = holdNotes[i].gameObject.transform.position.y;
            drawVector.x = x;
            drawVector.y = y;
            lineRenderer.SetPosition(i, drawVector);
        }

    }
}
