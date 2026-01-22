using UnityEngine;
using static Define;

public class BlackRoseJokerSpade : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.Joker;
        cardRarity = Define.CardRarity.Epic;
        cardName = "Black Rose Joker Spade";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

    }

    public override void OnCardDrawComplete()
    {

        Debug.Log("Black Rose Joker Spade Effect");

        foreach (CardBase item in Managers.Card.FieldCards)
        {
            if (item == null)
                continue;

            if (item is WaroftheRosesBase wotr && wotr.isBlack) 
            { 
                if (wotr.cardRarity == CardRarity.Epic || wotr.cardRarity == CardRarity.Legendary)
                {
                    Managers.Effect.Brands.Add(new BlackRoseInfoGather());
                    break;
                }
            } 
        }


    }
}

public class BlackRoseInfoGather : BrandBase
{

}