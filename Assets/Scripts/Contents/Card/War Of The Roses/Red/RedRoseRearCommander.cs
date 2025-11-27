using System.Linq;
using UnityEngine;
using static Define;

public class RedRoseRearCommander : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 60; // todo
        cardSuit = Define.CardSuit.Heart;
        cardRank = Define.CardRank.Ace;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Red Rose Rear Commander";
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

    }

    public override void OnCardDrawComplete()
    {
        for (int i = 0; i < 5; i++)
        {
            CardInfo card = Managers.Deck.UsedDeck
                .FirstOrDefault(c => c.collection == "War of the Roses" && (c.cardSuit == CardSuit.Heart || c.cardSuit == CardSuit.Diamond));

            if (card == null)
                break;

            // UsedDeck에서 제거
            Managers.Deck.UsedDeck.Remove(card);

            // UnUsedDeck에 추가
            Managers.Deck.UnUsedDeck.Add(card);
        }
    }
}
