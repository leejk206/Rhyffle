using UnityEngine;

public class RedRoseNegotiator : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Jack;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Red Rose Negotiator";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

        Debug.Log("init");
    }

    public override void OnCardDrawComplete()
    {
        NegotiatorEffect();
    }
}
