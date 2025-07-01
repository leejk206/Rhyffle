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
    public void MoveTransform(Vector3 pos, float dotweenTime) // 카드의 위치와 스케일을 dotweenTime동안 변경
    {
        if (gameObject != null)
        {
            transform.DOMove(pos, dotweenTime);
        }

    }
}
