using UnityEngine;

public abstract class WaroftheRosesBase : CardBase
{
    public bool isBlack;

    public override void Init(CardInfo cardInfo)
    {

        collection = "War Of The Roses";
        uniqueAbilityId = 0;

        cardNameBack = "War Of The Roses Back";
        uniqueAbilityIdBack = 0;
        collectionBack = "War Of The Roses Back";

    }
}
