using UnityEngine;
using DG.Tweening;

public class CardBase : MonoBehaviour
{
    public Vector3 CardPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MoveTransform(Vector3 pos, float dotweenTime) // ī���� ��ġ�� �������� dotweenTime���� ����
    {
        if (gameObject != null)
        {
            transform.DOMove(pos, dotweenTime);
        }

    }
}
