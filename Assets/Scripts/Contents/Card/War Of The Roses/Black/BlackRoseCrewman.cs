using UnityEngine;
using static Define;

public class BlackRoseCrewman : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Club;
        cardRank = Define.CardRank.Five;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Black Rose Crewman";
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

        int cnt = 0;

        foreach (CardBase item in Managers.Card.FieldCards)
        {

            if (item == null)
                continue;

            if (item.cardSuit != Define.CardSuit.Spade && item.cardSuit != Define.CardSuit.Club)
                continue;

            cnt += (item.collection == "War Of The Roses") ? 2 : 1;
        }

        this.CardRank += cnt * Managers.Effect.EffectData.BlackRoseMultiplier;
    }
}
