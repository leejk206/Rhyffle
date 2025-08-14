using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;

// ���߿� UIManager ���� ���� ���� ��� �浹 ������ �ӽ÷� ���� �̸�
// UIManger�� �Ű��� ��� region���� ���� ������ ������ ����
public class GameUIManager:MonoBehaviour
{
    //�׽�Ʈ�� ���� ���� Ȥ�� �ٸ� ��ũ��Ʈ�� ���� ����
    public GamePlayer gamePlayer;
    
    // 카운팅 숫자 스프라이트
    public Sprite[] countSprites;

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
    GameObject countSprite;
    GameObject countBackground;
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
        countSprite = GameObject.Find("CountSprite");
        countBackground = GameObject.Find("CountBackground");
        pauseUI = GameObject.Find("PauseUI");
        counting.SetActive(false);
        countBackground.SetActive(false);
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
        gamePlayer.play = false;
    }

    //ȭ�� �Ͻ����� Ǯ�� 3�� ī����
    public void ResumeCounting()
    {
        pauseUI.SetActive(false);
        countBackground.SetActive(true);
        counting.SetActive(true);
        Count3();
    }

    //�÷��� ȭ�� UI
    public void Resume()
    {
        scoreBoard.SetActive(true);
        songInfoBoard.SetActive(true);
        pauseButton.SetActive(true);
        gamePlayer.play = true;
    }

    public async UniTask Count3()
    {
        Image countImage = countSprite.GetComponent<Image>();
        for(int i = 3; i > 0; i--)
        {
            // countNum.GetComponent<TextMeshProUGUI>().text = i.ToString();
            countImage.sprite = countSprites[i - 1];
            await UniTask.WaitForSeconds(1);
        }
        countBackground.SetActive(false);
        counting.SetActive(false);
        Resume();
    }
}
