using UnityEngine;

public class DiamondFieldManager : DiamondFIeldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 123; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Jack;
        cardRarity = Define.CardRarity.Legendary;
        cardName = "Diamond Field Manager";
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
