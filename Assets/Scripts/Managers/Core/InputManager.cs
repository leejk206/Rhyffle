using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action KeyAction = null;

    public void OnUpdate()
    {

        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke(); // KeyAction�� Delegate�� �߰��ϴ� ������� ���.
    }

    // Todo Slide, Hold, Flick �� ���� �߰�

    public void Clear()
    {
        KeyAction = null;
    }
}
