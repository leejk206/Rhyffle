using UnityEngine;

public class DiamondFieldCenterFielder : DiamondFIeldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 120; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Eight;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Diamond Field Center Fielder";
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
