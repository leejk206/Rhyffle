using UnityEngine;
using static Define;

public class RedRosePriest : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Heart;
        cardRank = Define.CardRank.Jack;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Red Rose Priest";
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
        int black = 0;
        int red = 0;

        foreach (var c in Managers.Card.FieldCards)
        {
            if (c.cardSuit == CardSuit.Spade || c.cardSuit == CardSuit.Club)
                black++;
            else if (c.cardSuit == CardSuit.Diamond || c.cardSuit == CardSuit.Heart)
                red++;
        }

        if (black < red)
        {
            Managers.Score.CurrentMultiflier *= 1.2f;
        }
    }
}
