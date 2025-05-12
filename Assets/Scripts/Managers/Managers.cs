using UnityEngine;

public class Managers : MonoBehaviour
    // Singleton ���� �̿�
    // ��� �Ŵ��� ���� ȣ�� �� Managers.(ManagerName).(FeatureName) ������ ȣ�� ����
    // ex) Managers.Resource.Instantiate(path, transform) ��
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    #region Content
    // ���� ������ ������ �ʿ��� �Ŵ��� ����
    // CardManager _card = new CardManager();

    // public static CardManager Card { get { return Instance._card } }
    #endregion

    #region Core
    // ���� ���� ������ �ʿ��� �Ŵ��� ����
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();

    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    #endregion

    void Start()
    {
        Init();
    }

    void Update()
        // �� �Ŵ����� Update�� �ʿ��� �ڵ尡 �ִٸ� ���� �� �̰����� ���� ����
    {
        // Card.Onupdate();
    }

    static void Init()
    {
        // s_instance ���� üũ
        if (s_instance != null)
            return;

        // �� ���� @Managers ������Ʈ�� �ִ��� ã��
        GameObject go = GameObject.Find("@Managers");
        if (go == null)
        {
            // �����տ��� �ε��ؼ� �ν��Ͻ�ȭ
            go = Resources.Load<GameObject>("Prefabs/@Managers");
            go = Instantiate(go);
            go.name = "@Managers";

            // �� ��ȯ �� �ı����� �ʵ��� ����
            DontDestroyOnLoad(go);
        }

        #region ManagersInitiate
        s_instance._pool.Init();
        s_instance._scene.Init();

        #endregion
    }

    public static void Clear()
        // �� ��ȯ �� ������ ��ҵ� �߰�
    {
        s_instance._pool.Clear();
        s_instance._scene.Clear();

    }
}
