using UnityEngine;

public class DiamondFieldUtilityPlayer : DiamondFIeldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 126; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Joker;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Diaond Field Ace";
        collection = "Diamond Field";
        uniqueAbilityId = 0; // Todo

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>($"Art/Card/Diamond Field/{cardName}");
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
