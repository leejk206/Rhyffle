using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
        // 어떤 오브젝트가 해당 컴포넌트를 갖고 있는지 판단하고, 없다면 새로 생성해서 만들어 넣는다.
        // Extension을 활용하여 (Something).GetOrAddComponent<T>() 으로 활용 가능하다.
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }
}