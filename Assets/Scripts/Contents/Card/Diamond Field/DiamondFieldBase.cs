using UnityEngine;

public abstract class DiamondFieldBase : CardBase
{
    public enum Property
    {
        None = 0,
        Infielder = 1,
        Outfielder = 2,
        StoveLeague = 3,
        MultiPosition = 4,
    }

    public Property property;

    public override void Init(CardInfo cardInfo)
    {
        property = Property.None;

        collection = "Diamond FIeld";
        uniqueAbilityId = 0;

        cardNameBack = "Diamond FIeld Back";
        uniqueAbilityIdBack = 0;
        collectionBack = "Diamond FIeld Back";

    }
}
