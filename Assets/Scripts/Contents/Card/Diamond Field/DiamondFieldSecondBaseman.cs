using UnityEngine;

public class DiamondFieldSecondBaseman : DiamondFIeldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 116; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Four;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Diaond Field Second Baseman";
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
