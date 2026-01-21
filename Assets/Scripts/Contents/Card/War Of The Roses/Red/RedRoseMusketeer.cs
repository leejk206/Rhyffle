using UnityEngine;
using static Define;

public class RedRoseMusketeer : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 62; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Four;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Red Rose Musketeer";
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
        GetRedkRoseRankBonus();
    }
}