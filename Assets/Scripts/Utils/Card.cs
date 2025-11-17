using System;
using UnityEngine;

[Serializable]
public class CardInfo
{
    public int card_id;
    public int card_baseid;
    public int pack_id;
    public int card_particle_id;
    public int ability_id1;
    public int ability_id2;
    public int ability_id3;
    public int ability_level1;
    public int ability_level2;
    public int ability_level3;
    public int unique_ability_level;
    public bool bookmarked;
}

[Serializable]
public class CardBaseInfo {
    public int card_baseid;
    public Define.CardSuit card_suit;
    public Define.CardRank card_rank;
    public Define.CardRarity card_rarity;
    public int unique_ability_id;
    public string card_name;
    public string collections;
}

