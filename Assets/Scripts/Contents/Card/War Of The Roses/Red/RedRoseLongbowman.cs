using UnityEngine;
using System.Linq;
using static Define;

public class RedRoseLongbowman : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 58; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Two;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Red Rose Longbowman";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>($"Art/Card/War Of The Roses/{cardName}");
        if (sprite != null)
        {
            // sr.sprite = sprite;
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