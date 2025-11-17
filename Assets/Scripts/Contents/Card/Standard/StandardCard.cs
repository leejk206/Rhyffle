using UnityEngine;

public class StandardCard : CardBase
{

    public override void Init(GameCardInfo card)
    {
        durability = 5;

        card.cardInfo.card_baseid = 0;
        cardBaseInfo.card_suit = card.cardBaseInfo.card_suit;
        cardBaseInfo.card_rank = card.cardBaseInfo.card_rank;
        cardBaseInfo.card_rarity = Define.CardRarity.Common;

        cardBaseInfo.card_name = $"{card.cardBaseInfo.card_name}";
        cardBaseInfo.collections = "Standard";
        cardBaseInfo.unique_ability_id = 0;

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>($"Art/Card/Standard/{cardBaseInfo.card_name}");
        if (sprite != null)
        {
            sr.sprite = sprite;
        }
        else
        {
            Debug.Log($"{cardBaseInfo.card_name} sprite is null");
        }
    }

}
