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

    public void Init()
    {
        _deck = new(); _unUsedDeck = new(); _usedDeck = new();

        #region SetInitialDeck
        SetupDeck();
        #endregion
    }

    public void SetInitialDeck() // �⺻ 52���� ���Ĵٵ� ī�� �߰�
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
