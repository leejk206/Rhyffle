using System.Collections.Generic;
using UnityEngine;

public class DeckManager
{
    // 덱 순환 및 관리 담당 매니저

    List<GameCardInfo> _deck; // 덱의 정보를 담당하는 리스트 : 덱의 카드 교체 등 직접적인 수정이 아닌 한 수정되지 않음
    public List<GameCardInfo> Deck { get { return _deck; } }

    List<GameCardInfo> _unUsedDeck; // 게임 플레이 중 덱을 들고있는 리스트
    public List<GameCardInfo> UnUsedDeck { get { return _unUsedDeck; } }

    List<GameCardInfo> _usedDeck; // 묘지
    public List<GameCardInfo> UsedDeck { get { return _usedDeck; } }


    public void Init()
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

        foreach (var suit in suits)
        {
            foreach (var rank in ranks)
            {
                GameCardInfo cardInfo = new GameCardInfo(suit, rank);
                _deck.Add(cardInfo);
            }
        }

        #region SpecialCardTesting
        _deck.Add(new GameCardInfo(Define.CardSuit.Spade, Define.CardRank.Ace, "NewContinentScout", "NewContinent"));
        _deck.Add(new GameCardInfo(Define.CardSuit.Spade, Define.CardRank.Ace, "NewContinentScout", "NewContinent"));
        _deck.Add(new GameCardInfo(Define.CardSuit.Spade, Define.CardRank.Ace, "NewContinentScout", "NewContinent"));
        _deck.Add(new GameCardInfo(Define.CardSuit.Spade, Define.CardRank.Ace, "NewContinentScout", "NewContinent"));
            #endregion
    }

    public void ResetDeckByInfo() // Shuffle 혹은 새로운 게임 시작 시 Deck의 모든 참조를 UnUsedDeck으로 복사 후 셔플
    {
        _usedDeck.Clear();
        _unUsedDeck.Clear();

        foreach (GameCardInfo card in _deck)
        {
            _unUsedDeck.Add(card);
        }

        Shuffle();
    }

    public void ResetDeck() // Shuffle 혹은 새로운 게임 시작 시 Deck의 모든 참조를 UnUsedDeck으로 복사 후 셔플
    {
        _usedDeck.Clear();
        _unUsedDeck.Clear();

        foreach (GameCardInfo card in _deck)
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
            GameCardInfo temp = _unUsedDeck[i];
            _unUsedDeck[i] = _unUsedDeck[rand];
            _unUsedDeck[rand] = temp;
        }
    }

    public GameCardInfo PopCard(int idx = 0)
    {
        if (_unUsedDeck.Count <= 0) { ResetDeck(); }

        GameCardInfo item;
        if (idx == 0) { item = _unUsedDeck[0]; _unUsedDeck.RemoveAt(0); }
        else { item = _unUsedDeck[idx]; _unUsedDeck.RemoveAt(idx); }
        
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