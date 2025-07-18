using UnityEngine;
using System.Collections.Generic;

public class HoldNoteBody : MonoBehaviour
{
    // line Renderer의 위치값을 넣기 위한 HoldNotes
    // 몇번째 HoldNote인지 (json의 count값이다)
    public int count;
    // 이 홀드 노트를 제거해야 하는지
    bool remove = false;
    // 지금 현재 참조해야 하는 노트의 위치
    int current = 0;

    List<HoldNote> holdNotes = new List<HoldNote>();
    List<BoxCollider2D> colliders = new List<BoxCollider2D>();
    public LineRenderer lineRenderer;

    //리셋시키는 용도, Pool되거나 처음 생성될 때 무조건 한번씩 호출됨
    public void ResetValues()
    {
        count = 0;
        lineRenderer.positionCount = 0;
        remove = false;
    }

    // 노트들을 입력받아서 하나씩 넣는 방식
    public void ConnectNotes(GameObject holdNotes)
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        this.holdNotes.Add(holdNotes.GetComponent<HoldNote>());
        this.colliders.Add(holdNotes.GetComponent<BoxCollider2D>());
        lineRenderer.positionCount = this.holdNotes.Count;
    }

    // 매 프레임마다 실행되어 줄을 그어주는 함수
    public void DrawLine()
    {
        Vector3 drawVector = Vector3.zero;
        float x, y;

        for(int i = 0; i < colliders.Count; i++)
        {
            x = (colliders[i].bounds.max.x + colliders[i].bounds.min.x)/2;
            y = holdNotes[i].gameObject.transform.position.y;
            drawVector.x = x;
            drawVector.y = y;
            lineRenderer.SetPosition(i, drawVector);
        }

    }

    //노트 마지막까지 판정이 끝나면 지우고 반환하는 방법
    //LineRenderer의 길이를 0으로 만드는 방식으로 나중에 Pool되었을 때도 사용 가능하게 바꿈
    //지금은 지우는 과정
    public void DeleteNotes()
    {
        while(holdNotes.Count > 0)
        {
            var temp = holdNotes[0];
            holdNotes.RemoveAt(0);
            Destroy(temp.gameObject);
        }
        lineRenderer.positionCount = 0;
    }

    //노트를 각각 떨어뜨림
    public void Drop(float speed)
    {
        for(int i = 0; i < holdNotes.Count; i++)
        {
            holdNotes[i].Drop(speed);
        }
    }

    public HoldNote CurrentJudge()
    {
        return holdNotes[current];
    }

    //다음 HoldNote의 판정 노트를 지칭하도록 current의 값을 증가시킴
    //단 마지막 노트였을 경우 remove를 true로 바꾸어 제거시킴
    public void NextNote()
    {
        current++;
        if(current == holdNotes.Count)
        {
            remove = true;
        }
    }

    public void MissNote()
    {
        remove = true;
    }

    //remove의 값을 반환해주는 함수
    public bool IsRemove()
    {
        return remove;
    }

    // HoldType은 터치가 시작된 부분인지 중간 노트인지, 터치를 떼야하는 노트인지 확인시켜줌
    // 0 --> 터치 시작, 1 --> 중간, 2 --> 터치 뗌
    public int HoldType()
    {
        if(current == 0)
        {
            return 0;
        }else if(current == holdNotes.Count - 1)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
}
