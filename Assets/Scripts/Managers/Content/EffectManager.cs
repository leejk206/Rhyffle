using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager
{
    

    public EffectData EffectData;

    public void Init()
    {

        #region SetEffectData
        EffectData = new()
        {
            BlackRoseMultiplier = 1,
            RedRoseMultiplier = 1
        };
        #endregion

    }

    public void GameEnd()
    {

    }
}

public abstract class EffectBase
{
    public bool shouldBeRemoved = false;
    public bool isPropertySetted = false;
    public EffectBase() { SetEffectProperty(); }
    public virtual void SetEffectProperty() { isPropertySetted = true; }

    public virtual void OnCardDraw() { Debug.Log($"{this.GetType()} OnCardDraw Called."); }
    public virtual void OnCardDrawComplete() { Debug.Log($"{this.GetType()} OnCardDrawComplete Called."); }
    public virtual void OnNoteTrigger() { Debug.Log($"{this.GetType()} OnNoteTrigger Called."); }
    public virtual void OnCardDestroy() { Debug.Log($"{this.GetType()} OnCardDestroy Called."); }
}

public class EffectData
{
    public int BlackRoseMultiplier;
    public int RedRoseMultiplier;
}