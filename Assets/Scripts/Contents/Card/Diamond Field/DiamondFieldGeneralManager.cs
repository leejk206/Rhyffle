using UnityEngine;

public class DiamondFieldGeneralManager: DiamondFIeldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 124; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Queen;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Diamond Field General Manager";
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
