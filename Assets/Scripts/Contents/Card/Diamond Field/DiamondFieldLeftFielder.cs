using UnityEngine;

public class DiamondFieldLeftFielder : DiamondFieldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 119; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Seven;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Diamond Field Left Fielder";
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
        property = Property.Outfielder;
    }
}
