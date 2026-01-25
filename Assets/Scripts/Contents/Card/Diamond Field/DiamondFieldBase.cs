using System.Linq;
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

    public override HitEvent OnNoteTrigger(HitEvent hitEvent)
    {
        var evt = base.OnNoteTrigger(hitEvent);

        // 내야: 하이점프캐치 - Good 이상(=Miss가 아니면) → Perfect
        if ((property == Property.Infielder || property == Property.MultiPosition)
            && Managers.Effect.Effects != null
            && Managers.Effect.Effects.Any(e => e is HighJumpCatchEffect))
        {
            if (evt.GetJudgement() != Define.JudgementType.Miss)
            {
                evt.ChangeJudge(Define.JudgementType.Perfect);
            }
        }

        // 외야: 다이빙 캐치 - Miss → Good
        if ((property == Property.Outfielder || property == Property.MultiPosition)
            && Managers.Effect.Effects != null
            && Managers.Effect.Effects.Any(e => e is DivingCatchEffect))
        {
            if (evt.GetJudgement() == Define.JudgementType.Miss)
            {
                evt.ChangeJudge(Define.JudgementType.Good);
            }
        }

        return evt;
    }
}
