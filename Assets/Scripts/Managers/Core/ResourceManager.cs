using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
        // Ǯ�� ���� ������Ʈ�� ���� Load �� ���� ��� ���� Load�ϴ� �Լ�
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf("/");
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
        // Ǯ�� ���� ������Ʈ�� ���� Instantiate �� ���� ��� ���� Instantiate�ϴ� �Լ�
    {
        // 1. original�� �̹� ��� �ִٸ� �ٷ� ���
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"failed to load prefab : {path}");
            return null;
        }

        // 2. Ȥ�� Ǯ���� �ְ� �ִ��� �Ǵ�
        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;

        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        return go;
    }

    public GameObject Instantiate(GameObject go, Transform parent = null)
        // ������ ��� ���� ��� Instantiate�� �����ϵ��� ������ �Լ�
    {
        GameObject newGo = Object.Instantiate(go, parent);
        newGo.name = go.name;
        return newGo;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        // ���� Ǯ���� �ʿ��� Object��� Ǯ�� �Ŵ����� ��Ź
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(go);
    }

}
