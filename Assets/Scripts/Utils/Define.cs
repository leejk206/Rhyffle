using UnityEngine;

public class Define
// ���� ���ǰ� �� ����. State ���� �̿��� ����.
{
    public enum Scene
    { 
        Unknown, // ����
        Lobby, // ����
        Game, // ����
        Loading, // �ε�
        LoadingGame, // ���� ���� �� �ε� (�� ����/���̵�/��� ����/ ���� ����)
        ScoreBoard, //���� ���ȭ��
    }

    public enum CardSuit
    {
        Unknown = 100, Spade = 0, Heart = 1, Diamond = 2, Club = 3
    }

    public enum CardRank
    {
        Unknown = 100, Joker = 0, Ace = 1,
        Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10, 
        Jack = 11, Queen = 12, King = 13,
    }

    public enum CardRarity
    {
        Unknown = 100, Common = 0, Rare = 1, Epic = 2, Legendary = 3
    }

    public enum HandRank
    {
        None = 0,
        OnePair = 1,
        TwoPair = 2,
        Triple = 3,
        Straight = 4,
        Flush = 5,
        ThreePair = 6,
        FullHouse = 7,
        FourCard = 8,
        Rainbow = 9,
        StraightFlush = 10,
        DoubleTriple = 11,
        LuckySeven = 12,
    }
}
