using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
        // 풀링 가능 오브젝트를 먼저 Load 후 없을 경우 새로 Load하는 함수
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
        // 풀링 가능 오브젝트를 먼저 Instantiate 후 없을 경우 새로 Instantiate하는 함수
    {
        // 1. original을 이미 들고 있다면 바로 사용
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"failed to load prefab : {path}");
            return null;
        }

        // 2. 혹시 풀링된 애가 있는지 판단
        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;

        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        return go;
    }

    public GameObject Instantiate(GameObject go, Transform parent = null)
        // 원본을 들고 있을 경우 Instantiate가 가능하도록 구현한 함수
    {
        GameObject newGo = Object.Instantiate(go, parent);
        newGo.name = go.name;
        return newGo;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        // 만약 풀링이 필요한 Object라면 풀링 매니저에 위탁
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(go);
    }

}
