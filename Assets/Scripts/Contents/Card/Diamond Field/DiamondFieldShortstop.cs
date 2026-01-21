using System.Collections.Generic;
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

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>($"Art/Card/Diamond Field/{cardName}");
        if (sprite != null)
        {
            sr.sprite = sprite;
        }
        else
        {
            Debug.Log($"{cardName} sprite is null");
        }

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
                infieldCommander = (InfieldCommander)1;
                presetJudgementType = new List<Define.JudgementType>() { Define.JudgementType.Miss, Define.JudgementType.Miss,Define.JudgementType.Miss,Define.JudgementType.Miss };
                break;
            case 2:
                infieldCommander = (InfieldCommander)2;
                Managers.Score.isJudgementSetted = true;
                foreach (var item in Managers.Card.FieldCards)
                {
                    if (item.collection == "Diamond Field")
                    {
                        var card = item as DiamondFieldBase;
                        if (card != null && card.property == Property.Infielder || card != null && card.property == Property.MultiPosition)
                        {
                            card.presetJudgementType = new List<Define.JudgementType>() 
                            { Define.JudgementType.Miss, Define.JudgementType.Perfect, Define.JudgementType.Perfect, Define.JudgementType.Perfect };
                        }
                    }
                }
                break;
            case 3:
                infieldCommander = (InfieldCommander)3;
                foreach (var item in Managers.Card.FieldCards)
                {
                    if (item.collection == "Diamond Field")
                    {
                        var card = item as DiamondFieldBase;
                        if (card != null && card.property == Property.Infielder || card != null && card.property == Property.MultiPosition)
                        {
                            card.CardRank += 73;
                        }
                    }
                }
                break;
            default: // 4장을 넘어가는 모든 케이스 처리
                infieldCommander = (InfieldCommander)4;
                Managers.Effect.Brands.Add(new IronWallInfield());
                break;
        }

    }

    public override void OnCardDestroy()
    {
        base.OnCardDestroy();
        ResetJudgementType();
        Managers.Score.isJudgementSetted = false;
        
    }
}

public class IronWallInfield : BrandBase
{

}