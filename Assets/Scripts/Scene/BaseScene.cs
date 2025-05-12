using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;


    protected GameObject _root;
    public GameObject Root { get { return _root; } }

    void Awake()
    {
        Managers.Scene.SetCurrentScene(this);
        Init();
    }

    protected virtual void Init()
    {
        EventSystem eventSystem = Object.FindFirstObjectByType<EventSystem>();

        if (eventSystem == null)
        {
            // 2. Resources에서 프리팹을 로드
            GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/EventSystem");

            if (prefab == null)
            {
                Debug.LogError("EventSystem 프리팹을 찾을 수 없습니다: Resources/Prefabs/UI/EventSystem");
                return;
            }

            // 3. 프리팹 인스턴스화
            GameObject instance = Object.Instantiate(prefab);
            instance.name = "@EventSystem";
        }
    }

    public abstract void Clear();
}

