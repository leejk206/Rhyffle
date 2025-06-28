using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;

// ���߿� UIManager ���� ���� ���� ��� �浹 ������ �ӽ÷� ���� �̸�
// UIManger�� �Ű��� ��� region���� ���� ������ ������ ����
public class GameUIManager:MonoBehaviour
{
    //�׽�Ʈ�� ���� ���� Ȥ�� �ٸ� ��ũ��Ʈ�� ���� ����
    public NoteCreationTester tester;


    // ��ũ�� ���� ���� ����
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

    //�ӽ÷� ���� ���� ���� ���� ����
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

    //�� �̸� ������Ʈ
    public void UpdateSong(string songName)
    {
        songInfoText.GetComponent<TextMeshProUGUI>().text = songName;
    }

    //���� ������Ʈ
    public void UpdateScore(int score)
    {
        scoreBoardText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    //ȭ�� �� UI �Ҵ�
    void AllocateGameUI(){
        pauseButton = GameObject.Find("PauseButton");
        songInfoBoard = GameObject.Find("SongInfoBoard");
        scoreBoard = GameObject.Find("ScoreBoard");
    }

    //ȭ�� �Ͻ����� â ����
    public void Pause()
    {
        scoreBoard.SetActive(false);
        pauseButton.SetActive(false);
        songInfoBoard.SetActive(false);
        pauseUI.SetActive(true);
        tester.play = false;
    }

    //ȭ�� �Ͻ����� Ǯ�� 3�� ī����
    public void ResumeCounting()
    {
        pauseUI.SetActive(false);
        counting.SetActive(true);
        Count3();
    }

    //�÷��� ȭ�� UI
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
