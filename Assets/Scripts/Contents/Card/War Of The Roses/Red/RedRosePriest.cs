using UnityEngine;
using static Define;

public class RedRosePriest : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Heart;
        cardRank = Define.CardRank.Jack;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Red Rose Priest";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

        Debug.Log("init");
    }

    public override void OnCardDrawComplete()
    {
        PriestEffect();
    }
}
