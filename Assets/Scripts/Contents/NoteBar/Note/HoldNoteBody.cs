using UnityEngine;
using System.Collections.Generic;

public class HoldNoteBody : MonoBehaviour
{
    // line Renderer의 위치값을 넣기 위한 HoldNotes
    List<HoldNote> holdNotes = new List<HoldNote>();
    List<BoxCollider2D> colliders = new List<BoxCollider2D>();
    LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer  = gameObject.GetComponent<LineRenderer>();
    }

    public void ConnetcNotes(List<GameObject> holdNotes)
    {
        for(int i = 0; i < holdNotes.Count; i++)
        {
            this.holdNotes.Add(holdNotes[i].GetComponent<HoldNote>());
            this.colliders.Add(holdNotes[i].GetComponent<BoxCollider2D>());
        }
    }

    public void DrawLine()
    {
        float min_x, min_y, max_x, max_y;



        for(int i = 1; i < colliders.Count - 1; i++)
        {
            min_x = colliders[i].bounds.min.x;
            min_y = colliders[i].bounds.min.y;
            max_x = colliders[i].bounds.max.x;
            max_y = colliders[i].bounds.max.y;
        }

    }

    //노트 마지막까지 판정이 끝나면 지우고 반환하는 방법
    //지금은 지우는 과정
    public void DeleteNotes()
    {
        while(holdNotes.Count > 0)
        {
            var temp = holdNotes[0];
            holdNotes.RemoveAt(0);
            Destroy(temp.gameObject);
        }
    }

}
