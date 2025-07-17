using UnityEngine;
using DG.Tweening;

public class TempCard : CardBase
{


    public override void SetCardProperty()
    {
        // 실제로는 json으로 받아온 데이터를 이용하여 자동화.

        durability = 10;
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.Ace;
        cardRarity = Define.CardRarity.Common;
        cardName = "TempCard";
        collection = "Temp";
        uniqueAbilityId = 0;

        cardNameBack = "TempBack";
        uniqueAbilityIdBack = 0;
        collectionBack = "TempBack";
    }

    public override void OnCardDraw()
    {
        Debug.Log($"{cardName} Draw");
    }

    public override void OnNoteTrigger()
    {
        
    }
    public override void OnCardDestroy()
    {
        Debug.Log($"{cardName} Destroy");
    }

    
}
