using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;

// 나중에 UIManager 같은 파일 있을 경우 충돌 방지로 임시로 지은 이름
// UIManger로 옮겨질 경우 region으로 구간 나누어 저장할 예정
public class GameUIManager:MonoBehaviour
{
    //테스트용 추후 삭제 혹은 다른 스크립트를 지정 예정
    public NoteCreationTester tester;


    // 스크린 가로 세로 길이
    float screenWidth = Screen.width;
    float screenHeight = Screen.height;

    #region uiVariables
    GameObject pauseButton;
    GameObject songInfoBoard;
    GameObject songInfoText;
    GameObject scoreBoard;
    GameObject scoreBoardText;
    GameObject counting;
    GameObject countNum;
    GameObject pauseUI;
    #endregion

    //임시로 만든 거임 추후 지울 예정
    private void Start()
    {
        init();
    }

    public void init()
    {
        pauseButton = GameObject.Find("PauseButton");
        songInfoBoard = GameObject.Find("SongInfoBoard");
        songInfoText = GameObject.Find("SongInfoText");
        scoreBoard = GameObject.Find("ScoreBoard");
        scoreBoardText = GameObject.Find("ScoreBoardText");
        counting = GameObject.Find("CountingUI");
        countNum = GameObject.Find("CountNum");
        pauseUI = GameObject.Find("PauseUI");
        counting.SetActive(false);
        pauseUI.SetActive(false);
    }

    //곡 이름 업데이트
    public void UpdateSong(string songName)
    {
        songInfoText.GetComponent<TextMeshProUGUI>().text = songName;
    }

    //점수 업데이트
    public void UpdateScore(int score)
    {
        scoreBoardText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    //화면 속 UI 할당
    void AllocateGameUI(){
        pauseButton = GameObject.Find("PauseButton");
        songInfoBoard = GameObject.Find("SongInfoBoard");
        scoreBoard = GameObject.Find("ScoreBoard");
    }

    //화면 일시정지 창 띄우기
    public void Pause()
    {
        scoreBoard.SetActive(false);
        pauseButton.SetActive(false);
        songInfoBoard.SetActive(false);
        pauseUI.SetActive(true);
        tester.play = false;
    }

    //화면 일시정지 풀고 3초 카운팅
    public void ResumeCounting()
    {
        pauseUI.SetActive(false);
        counting.SetActive(true);
        Count3();
    }

    //플레이 화면 UI
    public void Resume()
    {
        scoreBoard.SetActive(true);
        songInfoBoard.SetActive(true);
        pauseButton.SetActive(true);
        tester.play = true;
    }

    public async UniTask Count3()
    {
        for(int i = 3; i > 0; i--)
        {
            countNum.GetComponent<TextMeshProUGUI>().text = i.ToString();
            await UniTask.WaitForSeconds(1);
        }
        counting.SetActive(false);
        Resume();
    }
}
