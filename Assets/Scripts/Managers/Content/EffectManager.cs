using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<EffectBase> EffectOnCardDraw;
    public List<EffectBase> EffectOnNoteTrigger;
    public List<EffectBase> EffectOnCardDestroy;

    public void Init()
    {
        EffectOnCardDraw = new();
        EffectOnNoteTrigger = new();
        EffectOnCardDestroy = new();
    }

    public void GameEnd()
    {
        EffectOnCardDraw = new();
        EffectOnNoteTrigger = new();
        EffectOnCardDestroy = new();
    }
}

public abstract class EffectBase
{
    public bool shouldBeRemoved = false;
    public bool isPropertySetted = false;
    public EffectBase() { SetEffectProperty(); }
    public virtual void SetEffectProperty() { isPropertySetted = true; }

    public virtual void OnCardDraw() { Debug.Log($"{this.GetType()} OnCardDraw Called."); }
    public virtual void OnNoteTrigger() { Debug.Log($"{this.GetType()} OnNoteTrigger Called."); }
    public virtual void OnCardDestroy() { Debug.Log($"{this.GetType()} OnCardDestroy Called."); }
}