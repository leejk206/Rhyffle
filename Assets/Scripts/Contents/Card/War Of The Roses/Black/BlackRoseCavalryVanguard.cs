using UnityEngine;
using static Define;

public class BlackRoseCavalryVanguard : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 59;
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.Three;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Black Rose Cavarly Vanguard";
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
        Debug.Log($"Black Rose Cavarly Vanguard Draw Effected. Current Multiplier : {Managers.Effect.EffectData.BlackRoseMultiplier}");
    }

    public override void OnCardDestroy()
    {
        Managers.Effect.EffectData.BlackRoseMultiplier /= 2;
        Debug.Log($"Black Rose Cavarly Vanguard Destroy Effected. Current Multiplier : {Managers.Effect.EffectData.BlackRoseMultiplier}");
    }
}