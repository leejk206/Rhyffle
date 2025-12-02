using UnityEngine;

public abstract class DiamondFIeldBase : CardBase
{
    public bool isBlack;

    public override void Init(CardInfo cardInfo)
    {

        collection = "Diamond FIeld";
        uniqueAbilityId = 0;

        cardNameBack = "Diamond FIeld Back";
        uniqueAbilityIdBack = 0;
        collectionBack = "Diamond FIeld Back";

    }
}
