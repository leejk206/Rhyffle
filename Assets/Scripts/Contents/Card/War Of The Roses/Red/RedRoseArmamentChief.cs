using System.Linq;
using UnityEngine;

public class RedRoseArmamentChief : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 60; // todo
        cardSuit = Define.CardSuit.Heart;
        cardRank = Define.CardRank.Seven;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Red Rose Armament Chief";
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
