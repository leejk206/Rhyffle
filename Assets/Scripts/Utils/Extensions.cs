using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extensions
{
    // ���� �Լ����� Extenstion Used�� �����ϰ� ���� Ȯ�� �Լ���
    // (Something).GetOrAddComponent<GameObject>() ������ ��� �����ϰ� �������.

    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(go);
    }

    public static bool IsValid(this GameObject go)
    {
        return go != null || go.activeSelf;
    }

}
