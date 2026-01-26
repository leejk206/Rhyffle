using Unity.VisualScripting;
using UnityEngine;

public class DeckBuildSlot : MonoBehaviour
{
    public GameObject cardSlotCard;
    public bool isCardInSlot = false;
    
    public void UpdateCardStatus()
    {
        if (!isCardInSlot)
        {
            cardSlotCard.SetActive(false);
        }
        else
        {
            cardSlotCard.SetActive(true);
            // 효과 기제
        }
    }
}
