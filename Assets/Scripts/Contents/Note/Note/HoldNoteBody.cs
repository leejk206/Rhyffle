using UnityEngine;
using System.Collections.Generic;

public class HoldNoteBody : MonoBehaviour
{
    // line Renderer�� ��ġ���� �ֱ� ���� HoldNotes
    // ���° HoldNote���� (json�� count���̴�)
    public int count;
    // �� Ȧ�� ��Ʈ�� �����ؾ� �ϴ���
    bool remove = false;
    // ���� ���� �����ؾ� �ϴ� ��Ʈ�� ��ġ
    int current = 0;

    List<HoldNote> holdNotes = new List<HoldNote>();
    List<BoxCollider2D> colliders = new List<BoxCollider2D>();
    public LineRenderer lineRenderer;

    //���½�Ű�� �뵵, Pool�ǰų� ó�� ������ �� ������ �ѹ��� ȣ���
    public void ResetValues()
    {
        count = 0;
        lineRenderer.positionCount = 0;
        remove = false;
    }

    // ��Ʈ���� �Է¹޾Ƽ� �ϳ��� �ִ� ���
    public void ConnectNotes(GameObject holdNotes)
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        this.holdNotes.Add(holdNotes.GetComponent<HoldNote>());
        this.colliders.Add(holdNotes.GetComponent<BoxCollider2D>());
        lineRenderer.positionCount = this.holdNotes.Count;
    }

    // �� �����Ӹ��� ����Ǿ� ���� �׾��ִ� �Լ�
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

    //��Ʈ ���������� ������ ������ ����� ��ȯ�ϴ� ���
    //LineRenderer�� ���̸� 0���� ����� ������� ���߿� Pool�Ǿ��� ���� ��� �����ϰ� �ٲ�
    //������ ����� ����
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

    //��Ʈ�� ���� ����߸�
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

    //���� HoldNote�� ���� ��Ʈ�� ��Ī�ϵ��� current�� ���� ������Ŵ
    //�� ������ ��Ʈ���� ��� remove�� true�� �ٲپ� ���Ž�Ŵ
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

    //remove�� ���� ��ȯ���ִ� �Լ�
    public bool IsRemove()
    {
        return remove;
    }

    // HoldType�� ��ġ�� ���۵� �κ����� �߰� ��Ʈ����, ��ġ�� �����ϴ� ��Ʈ���� Ȯ�ν�����
    // 0 --> ��ġ ����, 1 --> �߰�, 2 --> ��ġ ��
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
