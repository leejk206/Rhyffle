using UnityEngine;
using DG.Tweening;

public class CardBase : MonoBehaviour
{
    [Header("카드 상태")]
    public int durability = 3;
    public Vector3 CardPosition;
    public System.Action<int> isDurabilityZero;
    public int SlotIndex { get; set; } // 슬롯되는 인덱스
    
    [Header("카드 기본 정보")]
    public int cardBaseId;
    public CardSuit cardSuit;
    public CardRank cardRank;
    public CardRarity cardRarity;
    public string cardName;
    public string collection;
    public int uniqueAbilityId;

    [Header("카드 기본 정보 - Back")]
    public string cardNameBack;
    public int uniqueAbilityIdBack;
    public string collectionBack;
    
    public void MoveTransform(Vector3 pos, float dotweenTime) // ī���� ��ġ�� �������� dotweenTime���� ����
    {
        if (gameObject != null)
        {
            transform.DOMove(pos, dotweenTime);
        }

    }
    
    void OnMouseDown() // (임시) 카드 클릭 시 내구도 감소
    {
        ReduceDurability();
    }

    public void ReduceDurability() // 내구도 감소 함수
    {
        durability--;
        if (durability <= 0) isDurabilityZero?.Invoke(SlotIndex);
    }
}
