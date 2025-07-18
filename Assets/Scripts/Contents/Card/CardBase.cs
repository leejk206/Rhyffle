using UnityEngine;
using DG.Tweening;

public abstract class CardBase : MonoBehaviour
{
    [Header("카드 상태")]
    public int durability = 3;
    public Vector3 CardPosition;
    public System.Action<int> isDurabilityZero;
    public int SlotIndex { get; set; } // 슬롯되는 인덱스
    
    [Header("카드 기본 정보")]
    public int cardBaseId;
    public Define.CardSuit cardSuit;
    public Define.CardRank cardRank;
    public Define.CardRarity cardRarity;
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

    public void Init() // 카드 시작 시 Start 함수를 대체. 추상 메소드 호출은 Start에서 하면 위험함.
    {
        
    }

    public virtual void SetCardProperty() { } // 카드의 이름, 속성 등을 오브젝트와 매핑하는 함수.
    public virtual void SetCardProperty(Define.CardSuit cardSuit, Define.CardRank cardRank) { } // 기본 트럼프 카드 생성용


    #region Effects : 카드 효과 관련
    public virtual void OnCardDraw() { } // 카드 드로우 시 효과
    public virtual void OnNoteTrigger() { } // 노트 판정 시 효과
    public virtual void OnCardDestroy() { } // 카드 파괴 시 효과

    #endregion

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
