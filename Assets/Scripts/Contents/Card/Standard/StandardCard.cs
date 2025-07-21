using UnityEngine;

public class StandardCard : CardBase
{

    public override void Init(CardInfo cardInfo)
    {
        durability = 5;

        cardBaseId = 0;
        cardSuit = cardInfo.cardSuit;
        cardRank = cardInfo.cardRank;
        cardRarity = Define.CardRarity.Common;

        cardName = $"{cardInfo.cardName}";
        collection = "Standard";
        uniqueAbilityId = 0;

        cardNameBack = "StandardBack";
        uniqueAbilityIdBack = 0;
        collectionBack = "StandardBack";

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>($"Art/Card/Standard/{cardName}");
        if (sprite != null)
        {
            sr.sprite = sprite;
        }
        else
        {
            Debug.Log($"{cardName} sprite is null");
        }
    }

}
