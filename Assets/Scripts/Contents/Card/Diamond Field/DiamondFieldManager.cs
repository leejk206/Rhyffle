using System.Linq;
using UnityEngine;

public class DiamondFieldManager : DiamondFieldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 123; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Jack;
        cardRarity = Define.CardRarity.Legendary;
        cardName = "Diamond Field Manager";
        collection = "Diamond Field";
        uniqueAbilityId = 0; // Todo

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>($"Art/Card/Diamond Field/{cardName}");
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
        base.OnCardDrawComplete();

        // 1) Diamond Field 카드 필터링 + 정렬
        var diamondCards = Managers.Deck.UnUsedDeck
            .Where(card => card.collection == "Diamond Field")
            .OrderBy(card => card.cardRank)
            .ToList();

        // 2) 덱에서 제거
        foreach (var card in diamondCards)
        {
            Managers.Deck.UnUsedDeck.Remove(card);
        }

        // 3) 맨 위로 이동
        Managers.Deck.UnUsedDeck.InsertRange(0, diamondCards);
    }
}
