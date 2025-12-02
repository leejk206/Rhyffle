using UnityEngine;
using static Define;

public class BlackRoseAxeman : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.Seven;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Black Rose Axeman";
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

        Debug.Log("Black Rose Axeman Effect");

        int cnt = 0;

        foreach (CardBase item in Managers.Card.FieldCards)
        {

            if (item == null)
                continue;

            if (item.cardSuit != CardSuit.Spade && item.cardSuit != CardSuit.Club)
                continue;

            cnt += (item.collection == "War Of The Roses") ? 2 : 1;
        }

        this.CardRank += cnt * Managers.Effect.EffectData.BlackRoseMultiplier;
    }
}