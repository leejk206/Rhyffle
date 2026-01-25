using System.Linq;
using UnityEngine;

public class DiamondFieldManager : DiamondFieldBase
{
    public override void Init()
    {
        base.Init();


        cardBaseId = 123; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Jack;
        cardRarity = Define.CardRarity.Legendary;
        cardName = "Diamond Field Manager";
        collection = "Diamond Field";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

    }

    public override void OnCardDrawComplete()
    {
        base.OnCardDrawComplete();

        // 1) Diamond Field ī�� ���͸� + ����
        var diamondCards = Managers.Deck.UnUsedDeck
            .Where(card => card.collection == "Diamond Field")
            .OrderBy(card => card.cardRank)
            .ToList();

        // 2) ������ ����
        foreach (var card in diamondCards)
        {
            Managers.Deck.UnUsedDeck.Remove(card);
        }

        // 3) �� ���� �̵�
        Managers.Deck.UnUsedDeck.InsertRange(0, diamondCards);
    }
}
