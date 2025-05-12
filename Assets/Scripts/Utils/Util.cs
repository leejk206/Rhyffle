using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
        // � ������Ʈ�� �ش� ������Ʈ�� ���� �ִ��� �Ǵ��ϰ�, ���ٸ� ���� �����ؼ� ����� �ִ´�.
        // Extension�� Ȱ���Ͽ� (Something).GetOrAddComponent<T>() ���� Ȱ�� �����ϴ�.
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }
}