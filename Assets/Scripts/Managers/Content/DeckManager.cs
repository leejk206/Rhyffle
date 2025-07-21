using System.Collections.Generic;
using UnityEngine;

public class DeckManager
{
    // �� ��ȯ �� ���� ��� �Ŵ���

    List<GameObject> _deck; // ���� ������ ����ϴ� ����Ʈ : ���� ī�� ��ü �� �������� ������ �ƴ� �� �������� ����
    public List<GameObject> Deck { get { return _deck; } }

    List<GameObject> _unUsedDeck; // ���� �÷��� �� ���� ����ִ� ����Ʈ
    public List<GameObject> UnUsedDeck { get { return _unUsedDeck; } }

    List<GameObject> _usedDeck; // ����
    public List<GameObject> UsedDeck { get { return _usedDeck; } }

    #region �ڵ�ȭ�� ���� CardInfo ������
    List<CardInfo> _deckInfo; // ����
    public List<CardInfo> DeckInfo { get { return _deckInfo; } }

    List<CardInfo> _unUsedDeckInfo; // ����
    public List<CardInfo> UnUsedDeckInfo { get { return _unUsedDeckInfo; } }

    List<CardInfo> _usedDeckInfo; // ����
    public List<CardInfo> UsedDeckInfo { get { return _usedDeckInfo; } }
    #endregion


    public void Init()
    {
        _deck = new(); _unUsedDeck = new(); _usedDeck = new();
        _deckInfo = new(); _unUsedDeckInfo = new(); _usedDeckInfo = new();

        #region SetInitialDeck
        SetInitialDeck();
        #endregion
    }

    public void SetInitialDeck() // �⺻ 52���� ���Ĵٵ� ī�� �߰�
    {
        List<Define.CardSuit> suits = new() { Define.CardSuit.Spade, Define.CardSuit.Heart, Define.CardSuit.Diamond, Define.CardSuit.Clover };
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
                _deckInfo.Add(cardInfo);
            }
        }
    }

    public void SetupDeck() // init �� �ʱ� 60���� ī�带 ���� �߰�
    {
        // �ӽ� �ʱ� �� ���� - �ʱ� �� ���� ���� ���� ����
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
        // Todo �ʱ� �� ����

    }

    public void ResetDeckByInfo() // Shuffle Ȥ�� ���ο� ���� ���� �� Deck�� ��� ������ UnUsedDeck���� ���� �� ����
    {
        _usedDeckInfo.Clear();
        _unUsedDeckInfo.Clear();

        foreach (CardInfo card in _deckInfo)
        {
            _unUsedDeckInfo.Add(card);
        }

        Shuffle();
    }

    public void ResetDeck() // Shuffle Ȥ�� ���ο� ���� ���� �� Deck�� ��� ������ UnUsedDeck���� ���� �� ����
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

    public CardInfo PopCardByInfo()
    {
        if (_unUsedDeckInfo.Count <= 0)
        {
            ResetDeckByInfo(); // Todo resetdeck�� cardinfo ������� ����
        }

        CardInfo item = _unUsedDeckInfo[0];
        _unUsedDeckInfo.RemoveAt(0);
        return item;
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

    public void DoNothing() // ���� ���� �ʹ� Manager �ν��Ͻ� ������ ȣ�� �ڵ�
    {
        return;
    }
}