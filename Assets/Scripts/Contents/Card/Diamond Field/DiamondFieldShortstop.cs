using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiamondFieldShortstop : DiamondFieldBase
{
    public enum InfieldCommander
    {
        Miss = 1,
        HighJumpCatch = 2,
        DoublePlay = 3,
        IronWallInfield = 4,
    }

    public InfieldCommander infieldCommander;

    public override void Init()
    {
        base.Init();


        cardBaseId = 118; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Six;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Diamond Field Shortstop";
        collection = "Diamond Field";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();

        property = Property.Infielder;
        infieldCommander = InfieldCommander.Miss;
    }

    public override void OnCardDrawComplete()
    {
        base.OnCardDrawComplete();

        int infielderCnt = 0;
        foreach (var item in Managers.Card.FieldCards)
        {
            if (item is DiamondFieldBase diamond) 
            { 
                if (diamond.property == DiamondFieldBase.Property.Infielder || diamond.property == DiamondFieldBase.Property.StoveLeague
                   || diamond.property == DiamondFieldBase.Property.MultiPosition) { infielderCnt++; } 
            }
        }

        switch (infielderCnt)
        {
            case 1:
                infieldCommander = InfieldCommander.Miss;
                break;
            case 2:
                infieldCommander = InfieldCommander.HighJumpCatch;
                if (Managers.Effect.Effects != null && !Managers.Effect.Effects.Any(e => e is HighJumpCatchEffect))
                {
                    Managers.Effect.Effects.Add(new HighJumpCatchEffect());
                }
                break;
            case 3:
                infieldCommander = InfieldCommander.DoublePlay;
                foreach (var card in Managers.Card.FieldCards)
                {
                    if (card == null) continue;
                    if (card.collection != "Diamond Field") continue;

                    if (card is DiamondFieldBase dia && (dia.property == Property.Infielder || dia.property == Property.MultiPosition))
                    {
                        card.CardRank += 73;
                    }
                }
                break;
            default: // Over 4
                infieldCommander = InfieldCommander.IronWallInfield;
                if (Managers.Effect.Brands != null && !Managers.Effect.Brands.Any(b => b is IronWallInfield))
                {
                    Managers.Effect.Brands.Add(new IronWallInfield());
                }
                break;
        }

    }

    public override void OnCardDestroy()
    {
        base.OnCardDestroy();
        Managers.Effect.Effects.RemoveAll(e => e is HighJumpCatchEffect);
        Managers.Score.isJudgementSetted = false;
        
    }

    // 1장(실책): "이 카드가 처리하는 노트"는 항상 Miss
    public override HitEvent OnNoteTrigger(HitEvent hitEvent)
    {
        var evt = base.OnNoteTrigger(hitEvent);

        if (infieldCommander == InfieldCommander.Miss)
        {
            evt.ChangeJudge(Define.JudgementType.Miss);
        }

        return evt;
    }
}

public class IronWallInfield : BrandBase
{

}

public class HighJumpCatchEffect : EffectBase
{

}

