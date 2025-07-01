using UnityEngine;

public class Managers : MonoBehaviour
    // Singleton 패턴 이용
    // 모든 매니저 관련 호출 시 Managers.(ManagerName).(FeatureName) 등으로 호출 가능
    // ex) Managers.Resource.Instantiate(path, transform) 등
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    #region Content
    // 게임 컨텐츠 구현에 필요한 매니저 선언
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
    // 게임 기초 구현에 필요한 매니저 선언
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
        // 각 매니저별 Update가 필요한 코드가 있다면 구현 후 이곳에서 통합 실행
    {
        _card.OnUpdate();

        _input.OnUpdate();
    }

    static void Init()
    {
        // s_instance 존재 체크
        if (s_instance != null)
            return;

        // 씬 내에 @Managers 오브젝트가 있는지 찾음
        GameObject go = GameObject.Find("@Managers");
        if (go == null)
        {
            // 프리팹에서 로드해서 인스턴스화
            go = Resources.Load<GameObject>("Prefabs/@Managers");
            go = Instantiate(go);
            go.name = "@Managers";

            // 씬 전환 시 파괴되지 않도록 설정
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
        // 씬 전환 시 제거할 요소들 추가
    {
        s_instance._pool.Clear();
        s_instance._scene.Clear();

    }

    
}
