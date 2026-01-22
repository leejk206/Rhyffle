using System.Linq;
using UnityEngine;
using static Define;

public class BlackRoseJokerClub : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 57; // todo
        cardSuit = Define.CardSuit.Club;
        cardRank = Define.CardRank.Joker;
        cardRarity = Define.CardRarity.Epic;
        cardName = "Black Rose Joker Club";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

    }

    public override void OnCardDrawComplete()
    {

        Debug.Log("Black Rose Joker Club Effect");

        if (Managers.Effect.Brands.Any(b => b is BlackRoseInfoGather))
        {
            var target = Managers.Effect.Brands.FirstOrDefault(b => b is BlackRoseInfoGather);

            if (target != null)
            {
                Managers.Effect.Brands.Remove(target);
            }
            Managers.Effect.Brands.Add(new BlackRoseInfoWithdraw());
        }


    }
}

public class BlackRoseInfoWithdraw : BrandBase
{

}