using Unity.VisualScripting;
using UnityEngine;

public class DeckBuildSlot : MonoBehaviour
{
    public DeckBuildCard cardSlotCard;
    HardCodeFilter hardCodeFilter;
    DeckBuildBoard board;
    public bool isCardInSlot = false;
    
    public Define.CardRank rank;
    public Define.CardSuit suit;

    public void SetFilter()
    {

    }

    public void SetUpSlot(int rank, int suit)
    {
        this.rank = (Define.CardRank)rank;
        this.suit = (Define.CardSuit)suit;
        hardCodeFilter = GameObject.Find("HardCodeFilter").GetComponent<HardCodeFilter>();
        cardSlotCard.isInDeck = true;
    }

    public void ChangeCard(DeckCardInfo cardInfo)
    {
        cardSlotCard.SetCard(cardInfo); 
    }
}
