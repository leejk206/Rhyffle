using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.GraphicsBuffer;

public class CardManager
{
    //ī�� �̵� �� �ı�, ȿ�� ���� ���� ����ϴ� �Ŵ���

    List<CardBase> _fieldCards;
    public List<CardBase> FieldCards { get { return _fieldCards; } } // ���� �� ī�� ����Ʈ

    Queue<CardInfo> _cemetery;
    public Queue<CardInfo> Cemetery { get { return _cemetery; } } // 묘지 카드 리스트

    GameObject CardSpawnPoint;
    GameObject CardCemeteryPoint; // Card 묫자리
    GameObject CardBoard; // Card ���� ��Ʈ
    List<Vector3> CardPositions;
    Vector3 CardScale;
    Transform CardRoot;

    public List<string> SettedCards;
    public bool isCardSetted = false;


    public void Init()
    {
        OnGameStart(); // 임시로 Init에 설정, 실제 게임 실행 시에는 OnGameStart 코드만 호출

    }

    public void OnGameStart()
    {
        // executed at the start of the game 

        _fieldCards = new List<CardBase> { null, null, null, null, null, null, null };
        CardPositions = new();
        SettedCards = new List<string>() { null, null, null, null, null, null, null };

        _cemetery = new Queue<CardInfo> { };

        #region GetCardTransform
        CardSpawnPoint = GameObject.Find("CardSpawnPoint");
        CardCemeteryPoint = GameObject.Find("CardCemeteryPoint");
        CardBoard = GameObject.Find("CardBoard");
        GameObject CardScaleGuide = GameObject.Find("CardScaleGuide");

        // 카드 기본 크기(CardScale)는 Y 배치 계산 전에 먼저 구해야 한다.
        CardScale = CardScaleGuide.GetComponent<SpriteRenderer>().bounds.size;

        SpriteRenderer sr = CardBoard.GetComponent<SpriteRenderer>();
        Vector3 CardBoardSize = Vector3.Scale(sr.sprite.bounds.size, sr.transform.lossyScale);
        float width = CardBoardSize.x;

        float startX = sr.bounds.min.x;
        float centerY = CardBoard.transform.position.y;
        float z = CardBoard.transform.position.z - 1;

        // 가로 7칸 위치는 그대로 쓰되, Y를 위/아래 두 줄로 번갈아 배치한다.
        int[] indices = { 3, 9, 15, 21, 27, 33, 39 };
        int totalDivisions = 42;

        // 위/아래 줄 간격은 카드 높이(CardScale.y)를 기준으로 조정
        float rowOffset = CardScale.y * 0.6f;
        float upperY = centerY + rowOffset * 0.5f;
        float lowerY = centerY - rowOffset * 0.5f;

        for (int slot = 0; slot < indices.Length; slot++)
        {
            int i = indices[slot];
            float t = (float)i / totalDivisions;
            float posX = startX + t * width;

            // 0,2,4,6번 슬롯은 위줄, 1,3,5번 슬롯은 아래줄
            float y = (slot % 2 == 0) ? upperY : lowerY;

            CardPositions.Add(new Vector3(posX, y, z));
        }

        CardRoot = GameObject.Find("Cards").GetComponent<Transform>();
        #endregion

        TempKeyAllocate(); // Todo - 실제 카드 삭제 로직에 맞추어 구현.

    }

    public void OnUpdate()
    {
        // 디버깅을 위한 임시 코드.
        if (Input.GetKeyDown(KeyCode.A))
        {
            ResetCards();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Managers.Deck.ResetDeck();
        }
    }

    public void DrawAllCard()
    {
        // NOTE:
        // - 카드 효과(OnCardDraw 등)로 isCardSetted가 "이번 DrawAllCard 도중" 켜질 수 있다.
        // - 이 경우는 "다음 ResetCards"에 적용되어야 하므로, DrawAllCard 마지막에 무조건 false로 내리면 안 된다.
        bool usedSettedAtStart = isCardSetted;

        if (usedSettedAtStart)
        {
            int cnt = 0;
            foreach (string item in SettedCards)
            {
                if (item != null)
                {
                    DrawCard(0, item);
                    cnt++;
                }
            }
            for (int i = 0; i < 7 - cnt; i++)
            {
                DrawCard();
            }
        }
        else
        {
            DrawCard();
            DrawCard();
            DrawCard();
            DrawCard();
            DrawCard();
            DrawCard();
            DrawCard();
        }

        // 세팅 드로우를 실제로 사용한 경우에만 1회 소모
        if (usedSettedAtStart)
            isCardSetted = false;

        Managers.Score.CurrentMultiflier = 1;
    }

    public void DrawCard(int idx = 0, string cardName = "")
    {
        // If no parameter is provided, draw the top card.
        // If an index is provided, draw the card at the specified index.
        // If you want to draw a card with specific name, write index (0, cardname)
        if (_fieldCards.Count - _fieldCards.Count(item => item == null) < 7)
        {
            CardInfo pop;
            if (idx != 0) { pop = Managers.Deck.PopCard(idx); }
            else if (cardName != "") { pop = Managers.Deck.PopCard(0, cardName); }
            else { pop = Managers.Deck.PopCard(); }

            if (pop != null)
            {
                GameObject go = Managers.Resource.Instantiate(
                    $"Card/{pop.collection}/{(pop.collection == "Standard" ? "StandardCard" : pop.cardName)}", CardRoot);
                CardBase card;

                if (go != null && go.GetComponent<CardBase>() != null) { card = go.GetComponent<CardBase>();  }
                else { Debug.Log($"null : {go.name}"); return; }

                for (int i = 0; i < 7; i++) // Find empty space and fill it
                {
                    if (_fieldCards[i] == null)
                    {
                        _fieldCards[i] = card;
                        CardAlignment(card, i);

                        card.Initialize(pop); // 카드 초기화(단일 진입점)

                        // Initialize 안에서 스프라이트가 교체되므로,
                        // 최종 스프라이트 기준으로 CardScaleGuide 크기에 맞춰 스케일을 보정한다.
                        #region SetCardTransform
                        go.transform.position = CardSpawnPoint.transform.position;

                        SpriteRenderer target = go.GetComponent<SpriteRenderer>();
                        if (target != null)
                        {
                            Vector3 targetSize = target.bounds.size;
                            Vector3 scale = target.transform.localScale;
                            if (targetSize.x != 0 && targetSize.y != 0)
                            {
                                scale.x *= CardScale.x / targetSize.x;
                                scale.y *= CardScale.y / targetSize.y;
                                target.transform.localScale = scale;
                            }
                        }
                        #endregion

                        card.OnCardDraw(); // 현재 드로우 한 카드의 드로우 시 실행되는 효과 발동 

                        Managers.Hand.Evaluate(_fieldCards.Where(c => c != null).ToList()); // 족보 판정 시도

                        // 내구도 = 0일 때
                        card.SlotIndex = i;

                        card.isDurabilityZero = (int index) =>
                        {
                            switch (index)
                            {
                                case 0: Del0(); break;
                                case 1: Del1(); break;
                                case 2: Del2(); break;
                                case 3: Del3(); break;
                                case 4: Del4(); break;
                                case 5: Del5(); break;
                                case 6: Del6(); break;
                            }

                            DrawCard();

                            // 묘지 상태 확인 용
                            string summary = string.Join(" | ", _cemetery.Select(c => $"Slot {c.SlotIndex}: {c.cardBaseId}"));
                            Debug.Log("[묘지 상태] " + summary);
                        };

                        break;
                    }
                }


            }
        }
    }

    public void CardAlignment(CardBase card, int idx)
    {
        card.CardPosition = CardPositions[idx];
        card.MoveTransform(card.CardPosition, 0.2f);
    }

    public void RemoveAllCards()
    {
        Del0();
        Del1();
        Del2();
        Del3();
        Del4();
        Del5();
        Del6();
    }

    public void ResetCards()
    {
        // 슬롯 0만 보지 말고, 하나라도 있으면 전체 제거
        if (_fieldCards.Any(c => c != null))
        {
            RemoveAllCards();
        }
        DrawAllCard();

        if (Managers.Effect.Brands != null)
        {
            foreach (BrandBase item in Managers.Effect.Brands)
            {
                item.OnCardDrawComplete(); // 모든 카드 드로우 완료 시 각 낙인/징표의 효과 발동
            }
        }

        foreach (CardBase item in _fieldCards)
        {
            if (item == null) continue;
            item.OnCardDrawComplete(); // 모든 카드 드로우 완료 시 각 카드의 효과 발동
        }
    }

    #region ForKeyActionDebug // 디버깅을 위한 임시 코드.
    Action _keypadKeyAction;


    void TempKeyAllocate()
    {
        Managers.Input.KeyAction -= Del0;
        Managers.Input.KeyAction -= Del1;
        Managers.Input.KeyAction -= Del2;
        Managers.Input.KeyAction -= Del3;
        Managers.Input.KeyAction -= Del4;
        Managers.Input.KeyAction -= Del5;
        Managers.Input.KeyAction -= Del6;

        _keypadKeyAction = () =>
        {
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                Del0();
                Del1();
                Del2();
                Del3();
                Del4();
                Del5();
                Del6();

            }
            else if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                Del1();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                Del2();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                Del3();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                Del4();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                Del5();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                Del6();
            }
        };
        Managers.Input.KeyAction += _keypadKeyAction;
    }

    public void Del0()
    {
        // 묘지에 추가
        CardBase card = _fieldCards[0];
        if (card == null) return;
        card.OnCardDestroy();
        _cemetery.Enqueue(new CardInfo(card.cardSuit, card.cardRank, card.cardName, card.collection)
        {
            SlotIndex = card.SlotIndex,
            cardBaseId = card.cardBaseId,
        });
        _fieldCards[0] = null;
        if (card.gameObject != null)
            GameObject.Destroy(card.gameObject);
    }

    public void Del1()
    {
        // 묘지에 추가
        CardBase card = _fieldCards[1];
        if (card == null) return;
        card.OnCardDestroy();
        _cemetery.Enqueue(new CardInfo(card.cardSuit, card.cardRank, card.cardName, card.collection)
        {
            SlotIndex = card.SlotIndex,
            cardBaseId = card.cardBaseId,
        });
        _fieldCards[1] = null;
        if (card.gameObject != null)
            GameObject.Destroy(card.gameObject);
    }

    public void Del2()
    {
        // 묘지에 추가
        CardBase card = _fieldCards[2];
        if (card == null) return;
        card.OnCardDestroy();
        _cemetery.Enqueue(new CardInfo(card.cardSuit, card.cardRank, card.cardName, card.collection)
        {
            SlotIndex = card.SlotIndex,
            cardBaseId = card.cardBaseId,
        });
        _fieldCards[2] = null;
        if (card.gameObject != null)
            GameObject.Destroy(card.gameObject);
    }

    public void Del3()
    {
        // 묘지에 추가
        CardBase card = _fieldCards[3];
        if (card == null) return;
        card.OnCardDestroy();
        _cemetery.Enqueue(new CardInfo(card.cardSuit, card.cardRank, card.cardName, card.collection)
        {
            SlotIndex = card.SlotIndex,
            cardBaseId = card.cardBaseId,
        });
        _fieldCards[3] = null;
        if (card.gameObject != null)
            GameObject.Destroy(card.gameObject);
    }

    public void Del4()
    {
        // 묘지에 추가
        CardBase card = _fieldCards[4];
        if (card == null) return;
        card.OnCardDestroy();
        _cemetery.Enqueue(new CardInfo(card.cardSuit, card.cardRank, card.cardName, card.collection)
        {
            SlotIndex = card.SlotIndex,
            cardBaseId = card.cardBaseId,
        });
        _fieldCards[4] = null;
        if (card.gameObject != null)
            GameObject.Destroy(card.gameObject);
    }

    public void Del5()
    {
        // 묘지에 추가
        CardBase card = _fieldCards[5];
        if (card == null) return;
        card.OnCardDestroy();
        _cemetery.Enqueue(new CardInfo(card.cardSuit, card.cardRank, card.cardName, card.collection)
        {
            SlotIndex = card.SlotIndex,
            cardBaseId = card.cardBaseId,
        });
        _fieldCards[5] = null;
        if (card.gameObject != null)
            GameObject.Destroy(card.gameObject);
    }

    public void Del6()
    {
        // 묘지에 추가
        CardBase card = _fieldCards[6];
        if (card == null) return;
        card.OnCardDestroy();
        _cemetery.Enqueue(new CardInfo(card.cardSuit, card.cardRank, card.cardName, card.collection)
        {
            SlotIndex = card.SlotIndex,
            cardBaseId = card.cardBaseId,
        });
        _fieldCards[6] = null;
        if (card.gameObject != null)
            GameObject.Destroy(card.gameObject);
    }

    #endregion

}

public class CardInfo
{
    public CardInfo(Define.CardSuit cardSuit, Define.CardRank cardRank) // standard용 생성자
    {
        this.cardName = $"{cardRank}{cardSuit}";
        this.collection = "Standard";
        this.cardSuit = cardSuit;
        this.cardRank = cardRank;
    }

    public CardInfo(Define.CardSuit cardSuit, Define.CardRank cardRank, string cardName, string collection) // 일반 생성자
    {
        this.cardName = cardName;
        this.collection = collection;
        this.cardSuit = cardSuit;
        this.cardRank = cardRank;
    }

    public int durability = 3;
    public Vector3 CardPosition;
    public System.Action<int> isDurabilityZero;
    public int SlotIndex { get; set; } // 슬롯되는 인덱스

    public int cardBaseId;
    public Define.CardSuit cardSuit;
    public Define.CardRank cardRank;
    public Define.CardRarity cardRarity;
    public string cardName;
    public string collection;
    public int uniqueAbilityId;

    public string cardNameBack;
    public int uniqueAbilityIdBack;
    public string collectionBack;
}