using System;
using UnityEngine;

[Serializable]
public class CardPage
{
    public int card_page;
    public CardInfo[] card_index;
}

[Serializable]
public class CardInfo
{
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
public class CardBasePage
{
    public int card_base_page;
    public CardBaseInfo[] card_index;
}

[Serializable]
public class CardBaseInfo {
    public int card_baseid;
    public string card_name;
    public Define.CardRank card_rank;
    public Define.CardRarity card_rarity;
    public Define.CardSuit card_suit;
    public string collections;
    public int unique_ability_id;
}

