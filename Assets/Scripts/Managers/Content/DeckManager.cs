using System.Collections.Generic;
using UnityEngine;

public class DeckManager
{
    // �� ��ȯ �� ���� ��� �Ŵ���

    List<CardInfo> _deck; // ���� ������ ����ϴ� ����Ʈ : ���� ī�� ��ü �� �������� ������ �ƴ� �� �������� ����
    public List<CardInfo> Deck { get { return _deck; } }

    List<CardInfo> _unUsedDeck; // ���� �÷��� �� ���� ����ִ� ����Ʈ
    public List<CardInfo> UnUsedDeck { get { return _unUsedDeck; } }

    List<CardInfo> _usedDeck; // ����
    public List<CardInfo> UsedDeck { get { return _usedDeck; } }


    public void Init()
    {
        _deck = new(); _unUsedDeck = new(); _usedDeck = new();

        #region SetInitialDeck
        SetInitialDeck();
        #endregion
    }

    public void SetInitialDeck() // �⺻ 52���� ���Ĵٵ� ī�� �߰�
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
                CardInfo cardInfo = new CardInfo(suit, rank);
                _deck.Add(cardInfo);
            }
        }

        #region SpecialCardTesting
        _deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Ace, "NewContinentScout", "NewContinent"));
        _deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Ace, "NewContinentScout", "NewContinent"));
        _deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Ace, "NewContinentScout", "NewContinent"));
        _deck.Add(new CardInfo(Define.CardSuit.Spade, Define.CardRank.Ace, "NewContinentScout", "NewContinent"));
            #endregion
    }

    public void ResetDeckByInfo() // Shuffle Ȥ�� ���ο� ���� ���� �� Deck�� ��� ������ UnUsedDeck���� ���� �� ����
    {
        _usedDeck.Clear();
        _unUsedDeck.Clear();

        foreach (CardInfo card in _deck)
        {
            _unUsedDeck.Add(card);
        }

        Shuffle();
    }

    public void ResetDeck() // Shuffle Ȥ�� ���ο� ���� ���� �� Deck�� ��� ������ UnUsedDeck���� ���� �� ����
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
    }

    public CardInfo PopCard(int idx = 0)
    {
        if (_unUsedDeck.Count <= 0) { ResetDeck(); }

        CardInfo item;
        if (idx == 0) { item = _unUsedDeck[0]; _unUsedDeck.RemoveAt(0); }
        else { item = _unUsedDeck[idx]; _unUsedDeck.RemoveAt(idx); }
        
        return item;
    }

    public void DiscardTopCard()
    {

    }

    public void DoNothing() // ���� ���� �ʹ� Manager �ν��Ͻ� ������ ȣ�� �ڵ�
    {
        return;
    }
}