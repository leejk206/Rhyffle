using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiamondFieldCenterFielder : DiamondFieldBase
{
    public enum OutfieldCommander
    {
        Miss = 1,
        DivingCatch = 2,
        IronWallOutfield = 3,
    }

    public OutfieldCommander outfieldCommander;

    public override void Init()
    {
        base.Init();


        cardBaseId = 120; // todo
        cardSuit = Define.CardSuit.Diamond;
        cardRank = Define.CardRank.Eight;
        cardRarity = Define.CardRarity.Rare;
        cardName = "Diamond Field Center Fielder";
        collection = "Diamond Field";
        uniqueAbilityId = 0; // Todo

        LoadCardSprite();
        property = Property.Outfielder;
        outfieldCommander = OutfieldCommander.Miss;
    }

    public override void OnCardDrawComplete()
    {
        base.OnCardDrawComplete();

        int outfielderCnt = 0;
        foreach (var item in Managers.Card.FieldCards)
        {
            if (item is DiamondFieldBase diamond)
            {
                if (diamond.property == DiamondFieldBase.Property.Outfielder || diamond.property == DiamondFieldBase.Property.StoveLeague
                   || diamond.property == DiamondFieldBase.Property.MultiPosition) { outfielderCnt++; }
            }
        }

        switch (outfielderCnt)
        {
            case 1:
                outfieldCommander = OutfieldCommander.Miss;
                break;
            case 2:
                outfieldCommander = OutfieldCommander.DivingCatch;
                if (Managers.Effect.Effects != null && !Managers.Effect.Effects.Any(e => e is DivingCatchEffect))
                {
                    Managers.Effect.Effects.Add(new DivingCatchEffect());
                }
                break;
            default: // 3장 이상
                outfieldCommander = OutfieldCommander.IronWallOutfield;
                if (Managers.Effect.Brands != null && !Managers.Effect.Brands.Any(b => b is IronWallOutfield))
                {
                    Managers.Effect.Brands.Add(new IronWallOutfield());
                }
                break;

        }
    }

    public override void OnCardDestroy()
    {
        base.OnCardDestroy();
        Managers.Effect.Effects.RemoveAll(e => e is DivingCatchEffect);
    }

    // 1장(실책): "이 카드가 처리하는 노트"는 항상 Miss
    public override HitEvent OnNoteTrigger(HitEvent hitEvent)
    {
        var evt = base.OnNoteTrigger(hitEvent);

        if (outfieldCommander == OutfieldCommander.Miss)
        {
            evt.ChangeJudge(Define.JudgementType.Miss);
        }

        return evt;
    }
}

public class IronWallOutfield : BrandBase
{

}

public class DivingCatchEffect : EffectBase
{

}
