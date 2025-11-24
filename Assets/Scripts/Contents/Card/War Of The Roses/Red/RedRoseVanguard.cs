using UnityEngine;
using static Define;

public class RedRoseVanguard : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Ace;
        cardRarity = Define.CardRarity.Legendary;
        cardName = "Red Rose Vanguard";
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

    public override void OnCardDrawComplete()
    {

        Debug.Log($"{cardName} Effect");

        int left = 0;
        int right = Managers.Deck.UnUsedDeck.Count - 1;
        int swaps = 0;

        while (left < right && swaps < 5)
        {
            // 왼쪽에서 다음 '검은 카드'(빨간 카드를 건너뜀) 찾기
            while (left < right && (Managers.Deck.UnUsedDeck[left].cardSuit == Define.CardSuit.Heart || Managers.Deck.UnUsedDeck[left].cardSuit == Define.CardSuit.Diamond)) left++;

            // 오른쪽에서 다음 '빨간 카드'(검은 카드를 건너뜀) 찾기
            while (left < right && (Managers.Deck.UnUsedDeck[right].cardSuit == Define.CardSuit.Spade || Managers.Deck.UnUsedDeck[right].cardSuit == Define.CardSuit.Club)) right--;

            if (left < right)
            {
                // 교체
                var tmp = Managers.Deck.UnUsedDeck[left];
                Managers.Deck.UnUsedDeck[left] = Managers.Deck.UnUsedDeck[right];
                Debug.Log($"{tmp.cardName} changed into {Managers.Deck.UnUsedDeck[right].cardName}");
                Managers.Deck.UnUsedDeck[right] = tmp;

                swaps++;
                left++;
                right--;
            }
        }


    }
}