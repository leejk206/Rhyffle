using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager
{
    //카드 이동 및 파괴, 효과 적용 등을 담당하는 매니저

    List<CardBase> _fieldCards;
    public List<CardBase> FieldCards { get { return _fieldCards; } } // 현재 깔린 카드 리스트

    GameObject CardSpawnPoint;
    GameObject CardBoard; // Card 생성 루트
    List<Vector3> CardPositions;
    Vector3 CardScale;

    public void Init()
    {
        _fieldCards = new List<CardBase> { null, null, null, null, null, null, null };
        CardPositions = new();

        #region GetCardPosition
        CardSpawnPoint = GameObject.Find("CardSpawnPoint");
        CardBoard = GameObject.Find("CardBoard");
        SpriteRenderer sr = CardBoard.GetComponent<SpriteRenderer>();
        Vector3 CardBoardSize = Vector3.Scale(sr.sprite.bounds.size, sr.transform.lossyScale);
        float width = CardBoardSize.x;
        float height = CardBoardSize.x;

        float startX = sr.bounds.min.x;
        float y = CardBoard.transform.position.y;
        float z = CardBoard.transform.position.z;

        int[] indices = { 3, 9, 15, 21, 27, 33, 39 };
        int totalDivisions = 42;

        foreach (int i in indices)
        {
            float t = (float)i / totalDivisions;    // 비율
            float posX = startX + t * width;
            Vector3 pos = new Vector3(posX, y, z);
            CardPositions.Add(pos);
        }

        Vector3 originalScale = CardBoard.transform.localScale;
        CardScale = new Vector3(originalScale.x * (2f / 21f), originalScale.y * (4f / 5f), 1f);
        #endregion

        TempKeyAllocate(); // Todo - 디버깅 끝내면 없애기
    }

    public void OnUpdate()
    {
        // 임시 : 키패드 1 누르면 카드 드로우되게 설정
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


    #region ForKeyActionDebug // 디버깅을 위한 임시 코드
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
        GameObject temp = _fieldCards[0].gameObject;
        _fieldCards[0] = null;
        Managers.Resource.Destroy(temp);
    }

    public void Del1()
    {
        GameObject temp = _fieldCards[1].gameObject;
        _fieldCards[1] = null;
        Managers.Resource.Destroy(temp);
    }

    public void Del2()
    {
        GameObject temp = _fieldCards[2].gameObject;
        _fieldCards[2] = null;
        Managers.Resource.Destroy(temp);
    }

    public void Del3()
    {
        GameObject temp = _fieldCards[3].gameObject;
        _fieldCards[3] = null;
        Managers.Resource.Destroy(temp);
    }

    public void Del4()
    {
        GameObject temp = _fieldCards[4].gameObject;
        _fieldCards[4] = null;
        Managers.Resource.Destroy(temp);
    }

    public void Del5()
    {
        GameObject temp = _fieldCards[5].gameObject;
        _fieldCards[5] = null;
        Managers.Resource.Destroy(temp);
    }

    public void Del6()
    {
        GameObject temp = _fieldCards[6].gameObject;
        _fieldCards[6] = null;
        Managers.Resource.Destroy(temp);
    }

    #endregion

}
