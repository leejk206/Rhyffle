using UnityEngine;

public class DiamondFieldGeneralManager: DiamondFieldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 124; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Queen;
        cardRarity = Define.CardRarity.Normal;
        cardName = "Diamond Field General Manager";
        collection = "Diamond Field";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();
        property = Property.StoveLeague;
    }
}
