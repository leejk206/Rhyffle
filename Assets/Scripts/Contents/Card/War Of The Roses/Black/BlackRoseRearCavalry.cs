using UnityEngine;

public class BlackRoseRearCavalry : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 60; // todo
        cardSuit = Define.CardSuit.Club;
        cardRank = Define.CardRank.Three;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Black Rose Rear Cavalry";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>($"Art/Card/War Of The Roses/{cardName}");
        if (sprite != null)
        {
            sr.sprite = sprite;
        }
        else
        {
            Debug.Log($"{cardName} sprite is null");
        }

    }

    public override void OnCardDraw()
    {
        Managers.Effect.EffectData.BlackRoseMultiplier *= 2;
        Debug.Log($"{cardName} Draw Effected. Current Multiplier : {Managers.Effect.EffectData.BlackRoseMultiplier}");
    }

    public override void OnCardDestroy()
    {
        Managers.Effect.EffectData.BlackRoseMultiplier /= 2;
        Debug.Log($"{cardName} Destroy Effected. Current Multiplier : {Managers.Effect.EffectData.BlackRoseMultiplier}");
    }
}
