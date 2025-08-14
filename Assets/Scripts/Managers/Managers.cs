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
    CardManager _card = new CardManager();
    DeckManager _deck = new DeckManager();
    GameUIManager _uiManager = new GameUIManager();
    HandManager _hand = new HandManager();
    MainGameManager _mainGameManager = new MainGameManager();
    ScoreManager _score = new ScoreManager();

    // public static CardManager Card { get { return Instance._card } }
    public static CardManager Card { get { return Instance._card; } }
    public static DeckManager Deck { get { return Instance._deck; } }
    public static GameUIManager GameUI { get { return Instance._uiManager; } }
    public static HandManager Hand { get { return Instance._hand; } }
    public static ScoreManager Score { get { return Instance._score; } }
    #endregion

    #region Core
    // ���� ���� ������ �ʿ��� �Ŵ��� ����
    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    UIManager _ui = new UIManager();

    public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static UIManager UI { get { return Instance._ui; } }
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

        if (UnityEngine.Input.GetKeyDown(KeyCode.E))
        {
            _ui.ShowExtraLoadingUI();
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.H))
        {
            _ui.ShowHardLoadingUI();
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.C))
        {
            _ui.CloseLoadingUI();
        }
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
