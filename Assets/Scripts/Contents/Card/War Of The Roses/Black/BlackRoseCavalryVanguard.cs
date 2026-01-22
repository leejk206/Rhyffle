using UnityEngine;
using static Define;

public class BlackRoseCavalryVanguard : WaroftheRosesBase
{
    public override void Init()
    {
        base.Init();

        isBlack = true;

        cardBaseId = 59;
        cardSuit = Define.CardSuit.Spade;
        cardRank = Define.CardRank.Three;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Black Rose Cavarly Vanguard";
        collection = "War Of The Roses";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

    }

    public override void OnCardDraw()
    {
        GetRankDouble();
    }

    public override void OnCardDestroy()
    {
        RemoveRankDouble();
    }
}