using UnityEngine;

public class DiamondFieldFirstBaseman : DiamondFIeldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 115; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Three;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Diaond Field First Baseman";
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
