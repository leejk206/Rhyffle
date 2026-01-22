using UnityEngine;

public class RedRoseQuatermaster : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Heart;
        cardRank = Define.CardRank.Eight;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Red Rose Quatermaster";
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
