using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action KeyAction = null;

    public void OnUpdate()
    {

        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke(); // KeyAction을 Delegate로 추가하는 방식으로 사용.
    }

    // Todo Slide, Hold, Flick 등 동작 추가

    public void Clear()
    {
        KeyAction = null;
    }
}
