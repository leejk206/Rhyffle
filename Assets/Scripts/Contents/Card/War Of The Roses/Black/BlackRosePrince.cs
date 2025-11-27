using System.Linq;
using UnityEngine;
using static Define;

public class BlackRosePrince : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.King;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Black Rose Prince";
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

        Debug.Log("init");
    }

    public override void OnCardDraw()
    {

        Debug.Log("Black Rose Prince Effect");

        Managers.Card.isCardSetted = true;
        for (int i = 0; i < Managers.Card.SettedCards.Count; i++)
        {
            if (Managers.Card.SettedCards[i] == null)
            {
                Managers.Card.SettedCards[i] = "Black Rose King";
                break;
            }
        }

        for (int i = 0; i < Managers.Card.SettedCards.Count; i++)
        {
            if (Managers.Card.SettedCards[i] == null)
            {
                var card = Managers.Deck.UnUsedDeck
                    .FirstOrDefault(x =>
                        x.collection == "War of the Roses" &&
                        (x.cardSuit == CardSuit.Spade || x.cardSuit == CardSuit.Club));

                // 찾은 카드가 없으면 아무것도 하지 않음
                if (card != null)
                {
                    Managers.Card.SettedCards[i] = card.cardName;
                }

                break;
            }
        }


    }
}