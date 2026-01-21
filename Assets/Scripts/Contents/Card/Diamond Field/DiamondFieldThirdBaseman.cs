using UnityEngine;

public class DiamondFieldThirdBaseman : DiamondFieldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 117; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Five;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Diaond Field Third Baseman";
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

        property = Property.Infielder;
    }
}
