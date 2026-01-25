using UnityEngine;

public class BlackRoseGunner : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Club;
        cardRank = Define.CardRank.Seven;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Black Rose Gunner";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

        Debug.Log("init");
    }

    public override void OnCardDrawComplete()
    {

        Debug.Log($"{cardName} Effect");
        GetRankBonus();
    }
}
