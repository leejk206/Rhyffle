using UnityEngine;

public class RedRoseDeckSoldier : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Heart;
        cardRank = Define.CardRank.Four;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Red Rose Deck Solider";
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
        GetRedkRoseRankBonus();
    }
}
