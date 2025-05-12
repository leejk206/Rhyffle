using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extensions
{
    // 기존 함수들을 Extenstion Used가 가능하게 만든 확장 함수들
    // (Something).GetOrAddComponent<GameObject>() 등으로 사용 가능하게 만들었음.

    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(go);
    }

    public static bool IsValid(this GameObject go)
    {
        return go != null || go.activeSelf;
    }

}
