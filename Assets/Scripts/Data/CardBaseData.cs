using System;
using System.Collections.Generic;

// 카드 기본 정보
[Serializable]
public struct CardBaseData
{
    public int card_baseid;
    public Define.CardSuit card_suit;
    public Define.CardRank card_rank;
    public Define.CardRarity card_rarity;
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
