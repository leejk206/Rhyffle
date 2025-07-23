using System;
using UnityEngine;

public class NewContientExplorer : CardBase
{
    public override void Init(CardInfo cardInfo)
    {
        durability = 5;

        cardBaseId = 0;
        cardSuit = cardInfo.cardSuit;
        cardRank = cardInfo.cardRank;
        cardRarity = Define.CardRarity.Common;

        cardName = $"{cardInfo.cardName}";
        collection = "NewContinent";
        uniqueAbilityId = 0;

        cardNameBack = "StandardBack";
        uniqueAbilityIdBack = 0;
        collectionBack = "StandardBack";

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>($"Art/Card/NewContinent/{cardName}");
        if (sprite != null)
        {
            sr.sprite = sprite;
        }
        else
        {
            Debug.Log($"{cardName} sprite is null");
        }

    }

    public override void OnCardDestroy()
    {
        int i = Math.Max(Managers.Deck.UnUsedDeck.Count - 7, 0);
        for (; i < Managers.Deck.UnUsedDeck.Count; i++)
        {
            if (Managers.Deck.UnUsedDeck[i].collection == "NewContinent")
            {
                Managers.Card.DrawCard(i);
                return;
            }
        }

        Debug.Log("7장 이내에 신대륙 카드 없음");
    }
}
