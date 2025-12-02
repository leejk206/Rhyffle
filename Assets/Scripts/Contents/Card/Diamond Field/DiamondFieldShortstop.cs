using UnityEngine;

public class DiamondFieldShortstop : DiamondFIeldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 118; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Six;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Diamond Field Shortstop";
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
