using UnityEngine;
using System.Linq;
using static Define;

public class RedRoseCrossbowman : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 61; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Three;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Red Rose Crossbowman";
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

        Debug.Log($"{cardName} Effect");

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