using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager
{
    //ī�� �̵� �� �ı�, ȿ�� ���� ���� ����ϴ� �Ŵ���

    List<CardBase> _fieldCards;
    public List<CardBase> FieldCards { get { return _fieldCards; } } // ���� �� ī�� ����Ʈ
    
    Queue<CardBase> _cemetery;
    public Queue<CardBase> Cemetery { get { return _cemetery; } } // 묘지 카드 리스트

    GameObject CardSpawnPoint;
    GameObject CardCemeteryPoint; // Card 묫자리
    GameObject CardBoard; // Card ���� ��Ʈ
    List<Vector3> CardPositions;
    Vector3 CardScale;


    public void Init()
    {
        _fieldCards = new List<CardBase> { null, null, null, null, null, null, null };
        CardPositions = new();
        
        _cemetery = new Queue<CardBase> {};

        #region GetCardPosition
        CardSpawnPoint = GameObject.Find("CardSpawnPoint");
        CardCemeteryPoint = GameObject.Find("CardCemeteryPoint");
        CardBoard = GameObject.Find("CardBoard");
        SpriteRenderer sr = CardBoard.GetComponent<SpriteRenderer>();
        Vector3 CardBoardSize = Vector3.Scale(sr.sprite.bounds.size, sr.transform.lossyScale);
        float width = CardBoardSize.x;
        float height = CardBoardSize.x;

        float startX = sr.bounds.min.x;
        float y = CardBoard.transform.position.y;
        float z = CardBoard.transform.position.z - 1;

        int[] indices = { 3, 9, 15, 21, 27, 33, 39 };
        int totalDivisions = 42;

        foreach (int i in indices)
        {
            float t = (float)i / totalDivisions;    // ����
            float posX = startX + t * width;
            Vector3 pos = new Vector3(posX, y, z);
            CardPositions.Add(pos);
        }

        Vector3 originalScale = CardBoard.transform.localScale;
        CardScale = new Vector3(originalScale.x * (2f / 21f), originalScale.y * (4f / 5f), 1f);
        #endregion

        TempKeyAllocate(); // Todo - 실제 카드 삭제 로직에 맞추어 구현.
    }

    public void OnUpdate()
    {
        // 디버깅을 위한 임시 코드.
        if (Input.GetKeyDown(KeyCode.A))
        {
            DrawCard();
        }
    }

    public void DrawCard()
    {
        if (_fieldCards.Count - _fieldCards.Count(item => item == null) < 7)
        {
            GameObject pop = Managers.Deck.PopCard();
            if (pop != null)
            {
                CardBase card = Managers.Resource.Instantiate(pop, CardBoard.transform).GetComponent<CardBase>();

                card.gameObject.transform.position = CardSpawnPoint.transform.position;

                for (int i = 0; i < 7; i++)
                {
                    if (_fieldCards[i] == null)
                    {
                        _fieldCards[i] = card;
                        CardAlignment(card, i);

                        card.Init(); // 카드 초기화 코드
                        card.OnCardDraw(); // 카드 드로우 시 효과 발동 로직
                        
                        HandManager.Evaluate(_fieldCards.Where(c => c != null).ToList()); // 족보 판정 시도
                        
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
                            card.OnCardDestroy();
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
        card.transform.position = CardCemeteryPoint.transform.position;
        _cemetery.Enqueue(card);
        _fieldCards[0] = null;
        
        // GameObject temp = _fieldCards[0].gameObject;
        // _fieldCards[0] = null;
        // Managers.Resource.Destroy(temp);
    }

    public void Del1()
    {
        // 묘지에 추가
        CardBase card = _fieldCards[1];
        card.transform.position = CardCemeteryPoint.transform.position;
        _cemetery.Enqueue(card);
        _fieldCards[1] = null;
        
        // GameObject temp = _fieldCards[1].gameObject;
        // _fieldCards[1] = null;
        // Managers.Resource.Destroy(temp);
    }

    public void Del2()
    {
        // 묘지에 추가
        CardBase card = _fieldCards[2];
        card.transform.position = CardCemeteryPoint.transform.position;
        _cemetery.Enqueue(card);
        _fieldCards[2] = null;
        
        // GameObject temp = _fieldCards[2].gameObject;
        // _fieldCards[2] = null;
        // Managers.Resource.Destroy(temp);
    }

    public void Del3()
    {
        // 묘지에 추가
        CardBase card = _fieldCards[3];
        card.transform.position = CardCemeteryPoint.transform.position;
        _cemetery.Enqueue(card);
        _fieldCards[3] = null;
        
        // GameObject temp = _fieldCards[3].gameObject;
        // _fieldCards[3] = null;
        // Managers.Resource.Destroy(temp);
    }

    public void Del4()
    {
        // 묘지에 추가
        CardBase card = _fieldCards[4];
        card.transform.position = CardCemeteryPoint.transform.position;
        _cemetery.Enqueue(card);
        _fieldCards[4] = null;
        
        // GameObject temp = _fieldCards[4].gameObject;
        // _fieldCards[4] = null;
        // Managers.Resource.Destroy(temp);
    }

    public void Del5()
    {
        // 묘지에 추가
        CardBase card = _fieldCards[5];
        card.transform.position = CardCemeteryPoint.transform.position;
        _cemetery.Enqueue(card);
        _fieldCards[5] = null;
        
        // GameObject temp = _fieldCards[5].gameObject;
        // _cemetery.Enqueue(_fieldCards[5]); // 묘지에 추가
        // _fieldCards[5] = null;
        // Managers.Resource.Destroy(temp);
    }

    public void Del6()
    {
        // 묘지에 추가
        CardBase card = _fieldCards[6];
        card.transform.position = CardCemeteryPoint.transform.position;
        _cemetery.Enqueue(card);
        _fieldCards[6] = null;
        
        // GameObject temp = _fieldCards[6].gameObject;
        // _cemetery.Enqueue(_fieldCards[6]); // 묘지에 추가
        // _fieldCards[6] = null;
        // Managers.Resource.Destroy(temp);
    }

    #endregion

}
