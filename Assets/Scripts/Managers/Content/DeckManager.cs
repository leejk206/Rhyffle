using System.Collections.Generic;
using UnityEngine;

public class DeckManager
{
    // 덱 순환 및 관리 담당 매니저

    List<GameObject> _deck; // 덱의 정보를 담당하는 리스트 : 덱의 카드 교체 등 직접적인 수정이 아닌 한 수정되지 않음
    public List<GameObject> Deck { get { return _deck; } }

    List<GameObject> _unUsedDeck; // 게임 플레이 중 덱을 들고있는 리스트
    public List<GameObject> UnUsedDeck { get { return _unUsedDeck; } }

    List<GameObject> _usedDeck; // 묘지
    public List<GameObject> UsedDeck { get { return _usedDeck; } }

    public void Init()
    {
        _deck = new(); _unUsedDeck = new(); _usedDeck = new();

        #region SetInitialDeck
        SetupDeck();
        #endregion
    }

    public void SetInitialDeck() // 기본 52장의 스탠다드 카드 추가
    {
        List<string> suits = new() {"Spade", "Heart", "Diamond", "Club" };
        //List<string> ranks = new() {"Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King" };

        //foreach (string suit in suits)
        //{
        //    foreach (string rank in ranks)
        //    {
        //        _deck.Add(Resources.Load<GameObject>("Prefabs/Card/Standard"));
        //    }
        //}
    }

    public void SetupDeck() // init 시 초기 60장의 카드를 덱에 추가
    {
        // 임시 초기 덱 설정 - 초기 덱 구현 이후 삭제 예정
        _deck.Add(Resources.Load<GameObject>("Prefabs/Card/TempCard"));
        _deck.Add(Resources.Load<GameObject>("Prefabs/Card/TempCard"));
        _deck.Add(Resources.Load<GameObject>("Prefabs/Card/TempCard"));
        _deck.Add(Resources.Load<GameObject>("Prefabs/Card/TempCard"));
        _deck.Add(Resources.Load<GameObject>("Prefabs/Card/TempCard"));
        _deck.Add(Resources.Load<GameObject>("Prefabs/Card/TempCard"));
        _deck.Add(Resources.Load<GameObject>("Prefabs/Card/TempCard"));
        _deck.Add(Resources.Load<GameObject>("Prefabs/Card/TempCard"));
        _deck.Add(Resources.Load<GameObject>("Prefabs/Card/TempCard"));
        _deck.Add(Resources.Load<GameObject>("Prefabs/Card/TempCard"));
        _deck.Add(Resources.Load<GameObject>("Prefabs/Card/TempCard"));
        _deck.Add(Resources.Load<GameObject>("Prefabs/Card/TempCard"));
        _deck.Add(Resources.Load<GameObject>("Prefabs/Card/TempCard"));
        _deck.Add(Resources.Load<GameObject>("Prefabs/Card/TempCard"));
        // Todo 초기 덱 구현
    }

    public void ResetDeck() // Shuffle 혹은 새로운 게임 시작 시 Deck의 모든 참조를 UnUsedDeck으로 복사 후 셔플
    {
        _usedDeck.Clear();
        _unUsedDeck.Clear();

        foreach (GameObject card in _deck)
        {
            _unUsedDeck.Add(card);
        }

        Shuffle();
    }

    public void Shuffle()
    {
        for (int i = 0; i < _deck.Count; i++)
        {
            int rand = Random.Range(0, _deck.Count);
            GameObject Temp = _deck[i];
            _unUsedDeck[i] = _deck[rand];
            _unUsedDeck[rand] = Temp;
        }
    }

    public GameObject PopCard()
    {
        if (_unUsedDeck.Count <= 0)
        {
            ResetDeck();
        }

        GameObject item = _unUsedDeck[0];
        _unUsedDeck.RemoveAt(0);
        item.SetActive(true);
        return item;
    }

    public void DoNothing() // 게임 개발 초반 Manager 인스턴스 생성용 호출 코드
    {
        return;
    }
}
