using UnityEngine;
using System.Linq;
using static Define;

public class BlackRoseLongbowman : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 0; // todo
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.Ace;
        cardRarity = Define.CardRarity.Legendary;
        cardName = "Black Rose Longbowman";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>($"Art/Card/War Of The Roses/{cardName}");
        if (sprite != null)
        {
            sr.sprite = sprite;
        }
        else
        {
            Debug.Log($"{cardName} sprite is null");
        }

        Debug.Log("Longbowman init");
    }

    public override void OnCardDraw()
    {

        Debug.Log("Black Rose Longbowman Effect");

        foreach (CardBase item in Managers.Card.FieldCards)
        {
            if (item.collection == "War Of The Roses")
            {
                int warCount = Managers.Card.FieldCards.Count(card => card.collection == "War Of The Roses");
                item.CardRank = (int)item.cardRank + warCount * 2;
            }

            
        }


    }
}