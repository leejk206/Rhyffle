using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager
{
    // 덱 순환 및 관리 담당 매니저

    List<CardInfo> _deck; // 덱의 정보를 담당하는 리스트 : 덱의 카드 교체 등 직접적인 수정이 아닌 한 수정되지 않음
    public List<CardInfo> Deck { get { return _deck; } }

    List<CardInfo> _unUsedDeck; // 게임 플레이 중 덱을 들고있는 리스트
    public List<CardInfo> UnUsedDeck { get { return _unUsedDeck; } }

    List<CardInfo> _usedDeck; // 묘지
    public List<CardInfo> UsedDeck { get { return _usedDeck; } }


    public void Init()
    {
        OnGameStart();
    }

    public void OnGameStart()
    {
        _deck = new(); _unUsedDeck = new(); _usedDeck = new();

        #region SetInitialDeck
        SetInitialDeck();
        #endregion
    }

    public void SetInitialDeck() // 기본 52장의 스탠다드 카드 추가
    {
        List<Define.CardSuit> suits = new() { Define.CardSuit.Spade, Define.CardSuit.Heart, Define.CardSuit.Diamond, Define.CardSuit.Club };
        List<Define.CardRank> ranks = new()
        {
            Define.CardRank.Ace, Define.CardRank.Two, Define.CardRank.Three, Define.CardRank.Four, Define.CardRank.Five, Define.CardRank.Six, Define.CardRank.Seven,
            Define.CardRank.Eight, Define.CardRank.Nine, Define.CardRank.Ten, Define.CardRank.Jack, Define.CardRank.Queen, Define.CardRank.King
        };

        // War of the Roses (Black)
        _deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Ace, "Black Rose Vanguard", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Two, "Black Rose Longbowman", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Three, "Black Rose Cavalry Vanguard", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Four, "Black Rose Field Commander", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Five, "Black Rose Crossbowman", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Six, "Black Rose Musketeer", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Seven, "Black Rose Axeman", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Eight, "Black Rose Spearman", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Nine, "Black Rose Heavy Infantry", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Ten, "Black Rose Light Cavalry", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Jack, "Black Rose Negotiator", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Queen, "Black Rose Queen", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.King, "Black Rose Prince", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Joker, "Black Rose Joker Spade", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Club, Define.CardRank.Ace, "Black Rose Rear Commander", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Club, Define.CardRank.Two, "Black Rose Siege Engineer", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Club, Define.CardRank.Three, "Black Rose Rear Cavalry", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Club, Define.CardRank.Four, "Black Rose Armament Chief", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Club, Define.CardRank.Five, "Black Rose Crewman", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Club, Define.CardRank.Six, "Black Rose Deck Soldier", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Club, Define.CardRank.Seven, "Black Rose Gunner", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Club, Define.CardRank.Eight, "Black Rose Quatermaster", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Club, Define.CardRank.Nine, "Black Rose Medic", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Club, Define.CardRank.Ten, "Black Rose Blacksmith", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Club, Define.CardRank.Jack, "Black Rose Priest", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Club, Define.CardRank.Queen, "Black Rose Princess", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Club, Define.CardRank.King, "Black Rose King", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Club, Define.CardRank.Joker, "Black Rose Joker Club", "War of the Roses"));

        // War of the Roses (Red)
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Ace, "Red Rose Vanguard", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Two, "Red Rose Longbowman", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Three, "Red Rose Crossbowman", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Four, "Red Rose Musketeer", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Five, "Red Rose Axeman", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Six, "Red Rose Cavalry Vanguard", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Seven, "Red Rose Field Commander", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Eight, "Red Rose Spearman", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Nine, "Red Rose Heavy Infantry", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Ten, "Red Rose Light Cavalry", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Jack, "Red Rose Negotiator", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Queen, "Red Rose Queen", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.King, "Red Rose Prince", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Joker, "Red Rose Joker Diamond", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Heart, Define.CardRank.Ace, "Red Rose Rear Commander", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Heart, Define.CardRank.Two, "Red Rose Siege Engineer", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Heart, Define.CardRank.Three, "Red Rose Crewman", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Heart, Define.CardRank.Four, "Red Rose Deck Soldier", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Heart, Define.CardRank.Five, "Red Rose Gunner", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Heart, Define.CardRank.Six, "Red Rose Rear Cavalry", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Heart, Define.CardRank.Seven, "Red Rose Armament Chief", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Heart, Define.CardRank.Eight, "Red Rose Quatermaster", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Heart, Define.CardRank.Nine, "Red Rose Medic", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Heart, Define.CardRank.Ten, "Red Rose Blacksmith", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Heart, Define.CardRank.Jack, "Red Rose Priest", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Heart, Define.CardRank.Queen, "Red Rose Princess", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Heart, Define.CardRank.King, "Red Rose King", "War of the Roses"));
        _deck.Add(new CardInfo(Define.CardSuit.Heart, Define.CardRank.Joker, "Red Rose Joker Heart", "War of the Roses"));

        // Diamond Field
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Ace, "Diamond Field Ace", "Diamond Field"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Two, "Diamond Field Catcher", "Diamond Field"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Three, "Diamond Field First Baseman", "Diamond Field"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Four, "Diamond Field Second Baseman", "Diamond Field"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Five, "Diamond Field Third Baseman", "Diamond Field"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Six, "Diamond Field Shortstop", "Diamond Field"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Seven, "Diamond Field Left Fielder", "Diamond Field"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Eight, "Diamond Field Center Fielder", "Diamond Field"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Nine, "Diamond Field Right Fielder", "Diamond Field"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Ten, "Diamond Field Supporters", "Diamond Field"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Jack, "Diamond Field Manager", "Diamond Field"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Queen, "Diamond Field General Manager", "Diamond Field"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.King, "Diamond Field Closer", "Diamond Field"));
        _deck.Add(new CardInfo(Define.CardSuit.Diamond, Define.CardRank.Joker, "Diamond Field Utility Player", "Diamond Field"));

        foreach (var suit in suits)
        {
            foreach (var rank in ranks)
            {
                CardInfo cardInfo = new CardInfo(suit, rank);
                _deck.Add(cardInfo);
            }
        }

        #region SpecialCardTesting
        //_deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Ace, "NewContinentScout", "NewContinent"));
        //_deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Ace, "NewContinentScout", "NewContinent"));
        //_deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Ace, "NewContinentScout", "NewContinent"));
        #endregion
    }


    public void ResetDeck() // Shuffle 혹은 새로운 게임 시작 시 Deck의 모든 참조를 UnUsedDeck으로 복사 후 셔플
    {
        _usedDeck.Clear();
        _unUsedDeck.Clear();

        foreach (CardInfo card in _deck)
        {
            _unUsedDeck.Add(card);
        }

        Shuffle();
    }

    public void Shuffle()
    {
        for (int i = 0; i < _deck.Count; i++)
        {
            int rand = Random.Range(i, _deck.Count);
            CardInfo temp = _unUsedDeck[i];
            _unUsedDeck[i] = _unUsedDeck[rand];
            _unUsedDeck[rand] = temp;
        }

        Debug.Log("Shuffled");
    }

    public CardInfo PopCard(int idx = 0, string cardName = "")
    {
        if (_unUsedDeck.Count <= 0)
            ResetDeck();

        CardInfo item;

        // idx가 0이 아니면 무조건 idx 드로우
        if (idx != 0)
        {
            item = _unUsedDeck[idx];
            _unUsedDeck.RemoveAt(idx);
            return item;
        }

        // idx == 0일 때: cardName 우선 시도 → 실패 시 일반 드로우
        item = !string.IsNullOrEmpty(cardName)
            ? _unUsedDeck.FirstOrDefault(x => x.cardName == cardName)
            : null;

        // 못 찾았으면 기본 드로우(0번)
        if (item == null)
        {
            item = _unUsedDeck[0];
            _unUsedDeck.RemoveAt(0);
        }
        else
        {
            _unUsedDeck.Remove(item);
        }

        return item;
    }

    public void DiscardTopCard()
    {

    }

    public void DoNothing() // 게임 개발 초반 Manager 인스턴스 생성용 호출 코드
    {
        return;
    }
}