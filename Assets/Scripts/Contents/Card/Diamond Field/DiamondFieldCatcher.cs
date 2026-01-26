using System.Linq;
using UnityEngine;

public class DiamondFieldCatcher : DiamondFieldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 114; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Two;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Diamond Field Catcher";
        collection = "Diamond Field";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();
    }

    public override void OnCardDrawComplete()
    {
        base.OnCardDrawComplete();

        var card = Managers.Deck.UsedDeck.FirstOrDefault(x => x.cardName == "Diamond Field Ace");
        Managers.Deck.UnUsedDeck.Add(card);
        Managers.Deck.UsedDeck.Remove(card);
    }
}
