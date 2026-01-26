using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuildSlot : MonoBehaviour
{
    public DeckBuildCard cardSlotCard;
    HardCodeFilter hardCodeFilter;
    DeckBuildBoard board;
    public bool isCardInSlot = false;
    CardMechUI cardMechUI;

    public Define.CardRank rank;
    public Define.CardSuit suit;

    List<Define.CardRank> ranks = new List<Define.CardRank>();
    List<Define.CardSuit> suits = new List<Define.CardSuit>();
    List<Define.CardRarity> rarities = new List<Define.CardRarity>();

    // 클릭 짧게 하면 실행
    public void SetFilter()
    {
        hardCodeFilter.ApplyFilter(ranks, rarities, suits, true, false);
        cardMechUI.SetCards();
    }

    public void SetUpSlot(int rank, int suit)
    {
        this.rank = (Define.CardRank)rank;
        this.suit = (Define.CardSuit)suit;
        ranks.Add(this.rank);
        suits.Add(this.suit);
        cardMechUI = GameObject.Find("DeckBuildingUI").GetComponent<CardMechUI>();
        hardCodeFilter = GameObject.Find("HardCodeFilter").GetComponent<HardCodeFilter>();
        cardSlotCard.isInDeck = true;
    }

    public void ChangeCard(DeckCardInfo cardInfo)
    {
        cardSlotCard.SetCard(cardInfo); 
    }
}
