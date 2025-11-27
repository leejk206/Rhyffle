using UnityEngine;
using static Define;

public class RedRoseCavalryVanguard : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 59;
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Six;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Red Rose Cavarly Vanguard";
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
        Managers.Effect.EffectData.RedRoseMultiplier *= 2;
        Debug.Log($"{cardName} Draw Effected. Current Multiplier : {Managers.Effect.EffectData.BlackRoseMultiplier}");
    }

    public override void OnCardDestroy()
    {
        Managers.Effect.EffectData.RedRoseMultiplier /= 2;
        Debug.Log($"{cardName} Destroy Effected. Current Multiplier : {Managers.Effect.EffectData.BlackRoseMultiplier}");
    }
}