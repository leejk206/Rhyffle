using System.Collections.Generic;
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
                outfieldCommander = (OutfieldCommander)1;
                presetJudgementType = new List<Define.JudgementType>() { Define.JudgementType.Miss, Define.JudgementType.Miss, Define.JudgementType.Miss, Define.JudgementType.Miss };
                break;
            case 2:
                outfieldCommander = (OutfieldCommander)2;
                Managers.Score.isJudgementSetted = true;
                foreach (var item in Managers.Card.FieldCards)
                {
                    if (item.collection == "Diamond Field")
                    {
                        var card = item as DiamondFieldBase;
                        if (card != null && card.property == Property.Outfielder || card != null && card.property == Property.MultiPosition)
                        {
                            card.presetJudgementType = new List<Define.JudgementType>()
                            { Define.JudgementType.Miss, Define.JudgementType.Perfect, Define.JudgementType.Perfect, Define.JudgementType.Perfect };
                        }
                    }
                }
                break;
            default: // 3���� �Ѿ�� ��� ���̽� ó��
                outfieldCommander = (OutfieldCommander)3;
                Managers.Effect.Brands.Add(new IronWallOutfield());
                break;

        }
    }
}

public class IronWallOutfield : BrandBase
{

}
