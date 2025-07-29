using UnityEngine;

public class Define
{
    public enum Scene
    {
        Unknown = -1, 
        Lobby, 
        Game, 
        Loading, 
        LoadingGame, 
        ScoreBoard, 
    }

    public enum CardSuit
    {
        Unknown = -1, Spade = 0, Heart = 1, Diamond = 2, Club = 3
    }

    public enum CardRank
    {
        Unknown = -1, Joker = 0, Ace = 1,
        Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10,
        Jack = 11, Queen = 12, King = 13,
    }

    public enum CardRarity
    {
        Unknown = -1, Common = 0, Rare = 1, Epic = 2, Legendary = 3
    }

    public enum HandRank
    {
        Unknown = -1,
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

    public enum Collections
    {
        Unknown = -1,
        Standard = 0,
    }

    public enum CardName
    {
        // Each collection is assigned a unique hundred-level base (e.g., 100, 200...),
        // and each card within a collection is assigned a one-digit offset (e.g., +1, +2...).
        Unknown = -1,

        Standard = 0,

        #region NewContinent
        NewContinentScout = 101,
        NewContinentExplorer = 102,
        NewContinentArchivist = 103,
        NewContinentEccentricArchivist = 104,
        NewContinentEngineer = 105,
        NewContinentEccentricEngineer = 106,
        NewContinentSovereign = 107,
        #endregion

    }
}
