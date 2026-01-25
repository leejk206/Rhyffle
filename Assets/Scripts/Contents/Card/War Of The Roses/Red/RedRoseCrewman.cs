using UnityEngine;
using static Define;

public class RedRoseCrewman : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Heart;
        cardRank = Define.CardRank.Three;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Red Rose Crewman";
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
