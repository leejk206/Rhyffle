using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using static Define;


public abstract class CardBase : MonoBehaviour
{
    private bool _isInitialized;

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

    public int CardRank; // 점수 카운팅 시 계산되는 랭크
    public List<JudgementType> presetJudgementType; // 이미 정해진 노트 판정이 있다면 그걸 사용

    public float ScaleAdd;
    public float ScaleMult;


    public void MoveTransform(Vector3 pos, float dotweenTime) // ī���� ��ġ�� �������� dotweenTime���� ����
    {
        if (gameObject != null)
        {
            transform.DOMove(pos, dotweenTime);
        }
    }

    /// <summary>
    /// 카드 초기화 단일 진입점.
    /// 외부(CardManager 등)에서는 반드시 이 메서드만 호출하도록 통일한다.
    /// </summary>
    public void Initialize(CardInfo cardInfo)
    {
        if (_isInitialized)
            return;

        _isInitialized = true;

        // 공통 초기화(중복/덮어쓰기 방지 목적)
        ResetJudgementType();
        ScaleAdd = 0;
        ScaleMult = 0;

        // 1) 데이터 기반 초기화(덱에서 뽑힌 CardInfo 반영 및 베이스 초기화)
        Init(cardInfo);

        // 2) 타입(카드 스크립트) 정의 초기화
        // 대부분의 카드가 Init()만 오버라이드하고 있으므로,
        // "실제로 오버라이드된 경우에만" 호출해서 불필요한 2중 초기화를 피한다.
        var init0 = GetType().GetMethod("Init", System.Type.EmptyTypes);
        if (init0 != null && init0.DeclaringType != typeof(CardBase))
        {
            Init();
        }

        // 3) 최종 정리: Init 내부에서 cardRank가 바뀌어도 CardRank는 일관되게 맞춘다
        CardRank = (int)cardRank;
    }

    public virtual void Init()
    {
        // 카드 시작 시 Start 함수를 대체. 추상 메소드 호출은 Start에서 하면 위험함.
        // 카드의 기본 정보를 여기서 입력해야 함.
        // 하위 클래스에서 실행 : cardSuit = cardInfo.cardSuit 등
        CardRank = (int)cardRank;
        ResetJudgementType();
        ScaleAdd = 0;
        ScaleMult = 0;
    }

    public virtual void Init(CardInfo cardInfo) 
    {
        // 카드 시작 시 Start 함수를 대체. 추상 메소드 호출은 Start에서 하면 위험함.
        // 카드의 기본 정보를 여기서 입력해야 함.
        // 하위 클래스에서 실행 : cardSuit = cardInfo.cardSuit 등
        CardRank = (int)cardRank;
        ResetJudgementType();
    }

    public void SetCardRank(int rank)
    {
        CardRank = rank;
    }

    #region Effects : 카드 효과 관련
    public virtual void OnCardDraw() { } // 카드 드로우 시 효과
    public virtual void OnCardDrawComplete() { } // 모든 카드 드로우 완료 시 효과
    public virtual HitEvent OnNoteTrigger(HitEvent hitEvent) 
    {
        hitEvent.AddRank(CardRank);
        hitEvent.AddScaleAdd(0);
        return hitEvent;
    } // 노트 판정 시 효과
    public virtual HitEvent OnCardExist(HitEvent hitEvent) {
        hitEvent.AddScaleAdd(ScaleAdd);
        hitEvent.AddScaleMult(ScaleMult);
        return hitEvent; 
    }

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

    public void ResetJudgementType()
    {
        presetJudgementType = new List<JudgementType>() { JudgementType.Miss, JudgementType.Good, JudgementType.Great, JudgementType.Perfect };
    }

    protected void LoadCardSprite()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        string spritePath = $"Art/Card/{collection}/{cardName}";
        Sprite sprite = Resources.Load<Sprite>(spritePath);
        if (sprite != null)
        {
            sr.sprite = sprite;
        }
        else
        {
            Debug.Log($"{cardName} sprite is null (Path: {spritePath})");
        }
    }
}
