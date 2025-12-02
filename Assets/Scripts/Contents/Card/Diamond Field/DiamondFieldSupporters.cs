using UnityEngine;

public class DiamondFieldSupporters : DiamondFIeldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 122; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Ten;
        cardRarity = Define.CardRarity.Epic;
        cardName = "Diamond Field Supporters";
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
