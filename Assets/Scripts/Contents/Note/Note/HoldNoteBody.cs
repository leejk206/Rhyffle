using UnityEngine;
using static Define;
using System.Collections.Generic;

public class HoldNoteBody : Note
{
    public List<HoldNote> holdNotes = new List<HoldNote>();
    public List<BoxCollider2D> colliders = new List<BoxCollider2D>();
    public LineRenderer lineRenderer;
    public int curJudge = 0;

    // Draw Line에서 사용
    Vector3 drawVector = Vector3.zero;
    float x, y;

    // 0 not touched yet, 1 touching, 2 touch ended
    int touchSig = 0;

    public override void Drop(float speed)
    {
        for (int i = 0; i < holdNotes.Count; i++) {
            holdNotes[i].Drop(speed);
        }
        DrawLine();
    }

    public override JudgementType ReadJudge(int lane, int bpm, int checkType, float curTime)
    {
        
        if (curJudge == 0)
        {
            if(checkType == 1 || checkType == 0)
            {
                JudgementType temp = holdNotes[0].ReadJudge(lane, bpm,checkType, curTime);
                if (temp!=JudgementType.Checked && temp!=JudgementType.NotChecked)
                {
                    curJudge++;
                }
                return temp;
            }
            else { return JudgementType.NotChecked; }
        }
        else if (curJudge == holdNotes.Count - 1) {
            if (checkType == 4 || checkType == 0)
            {
                if (fingerID == endFingerID)
                {
                    JudgementType tempJudge = holdNotes[curJudge].ReadJudge(lane, bpm, checkType, curTime);
                    if(tempJudge == JudgementType.Checked || tempJudge == JudgementType.NotChecked)
                    {
                        return JudgementType.SpMiss;
                    }
                    else
                    {
                        return tempJudge;
                    }
                }
                else
                {
                    return holdNotes[curJudge].ReadJudge(lane, bpm, 0, curTime);
                }
            }
            else
            {
                return JudgementType.NotChecked;
            }
        }
        else
        {
            if(checkType == 0)
            {
                return holdNotes[curJudge].ReadJudge(lane,bpm,checkType, curTime);
            }else if(checkType == 3)
            {
                if(lane >= holdNotes[curJudge].line && lane <= holdNotes[curJudge].line + holdNotes[curJudge].length && pressFingerID == fingerID)
                {
                    float tempJudge = holdNotes[curJudge].judge;
                    if(curTime < tempJudge - (float)bpm / 600 * 16) 
                    {
                        return JudgementType.Checked;
                    }
                    else if (curTime < tempJudge + (float)bpm / 600 * 16)
                    {
                        curJudge++;
                        return JudgementType.Perfect;
                    }else if(curTime < tempJudge + (float)bpm / 600 * 1.5f * 16)
                    {
                        curJudge++;
                        return JudgementType.Great;
                    }else if (curTime < tempJudge + (float)bpm/600 *2f * 16)
                    {
                        curJudge++;
                        return JudgementType.Good;
                    }
                    else
                    {
                        curJudge++;
                        return JudgementType.Miss;
                    }
                }
                else
                {
                    return JudgementType.NotChecked;
                }
            }
            else if(checkType == 4)
            {
                if (fingerID == endFingerID)
                {
                    return JudgementType.SpMiss;
                }
            }
            else
            {
                return JudgementType.NotChecked;
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
