using UnityEngine;

public class BlackRoseNegotiator : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Club;
        cardRank = Define.CardRank.Jack;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Black Rose Negotiator";
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
