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
<<<<<<< Updated upstream
    // CardManager _card = new CardManager();

    // public static CardManager Card { get { return Instance._card } }
=======
    CardManager _card = new CardManager();
    DeckManager _deck = new DeckManager();
    GameUIManager _uiManager = new GameUIManager();
    MainGameManager _mainGameManager = new MainGameManager();

    public static CardManager Card { get { return Instance._card; } }
    public static DeckManager Deck { get { return Instance._deck; } }
    public static GameUIManager UI { get { return Instance._uiManager; } }
>>>>>>> Stashed changes
    #endregion

    #region Core
    // ���� ���� ������ �ʿ��� �Ŵ��� ����
    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();

    public static InputManager Input { get { return Instance._input; } }
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
        _card.OnUpdate();

        _input.OnUpdate();
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

        s_instance = go.GetComponent<Managers>();

        #region ManagersInitiate
        s_instance._card.Init();
        s_instance._deck.Init();


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
