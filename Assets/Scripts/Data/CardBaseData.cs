using System;
using System.Collections.Generic;

// 카드 기본 정보
[Serializable]
public struct CardBaseData
{
    public int card_baseid;
    public CardSuit card_suit;
    public CardRank card_rank;
    public CardRarity card_rarity;
    public string card_name;
    public int unique_ability_id;
    public string collections;

    public string card_name_back;
    public int unique_ability_id_back; 
    public string collections_back;
}

[Serializable]
public struct CardBaseWrapper
{
    public int cardBasePage;
    public List<CardBaseData> card_index;
}

public enum CardSuit
{
    Spade = 0, Heart = 1, Diamond = 2, Clover = 3
}
    
public enum CardRank
{
    A = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, J, Q, K
}

public enum CardRarity
{
    Common = 0, Rare = 1, Epic = 2, Legendary = 3
}