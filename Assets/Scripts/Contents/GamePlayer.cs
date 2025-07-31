using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;

public class GamePlayer : MonoBehaviour
{

    NoteCreator noteCreator;

    public NoteTest noteTester;

    // ���� ���� ����
    public bool play = true;
    // ���� ���۽� ������ (-144��° ���ڿ��� ����, �� 144���� ���ĸ� 0�� �ش��ϴ� ���ڸ� ó���ؾ� �ϴ� Ÿ�̹�)
    float currentTime = -144;

    // ��Ʈ�� ����, Json���� Serialize�Ǿ� ����� Info Class���� �迭�� ���� (NoteType.cs Ȯ��)
    #region noteInfos
    BasicNoteInfo[] basicNotes;
    SlideNoteInfo[] slideNotes;
    FlickNoteInfo[] flickNotes;
    HoldNoteInfo[] holdNotes;
    // holdNote�� �������� holdBody���� ���� ���� List
    List<List<HoldNoteInfo>> holdBodyList = new List<List<HoldNoteInfo>>();
    int basicNoteCount;
    int slideNoteCount;
    int flickNoteCount;
    int holdNoteCount;
    #endregion

    // noteInfo���� ������ ��Ʈ���� position ������ �̸� int �迭�� ����
    // holdNote�� ��� ó�� �����ϴ� ��Ʈ�� �������� ����
    #region spawnPositions
    int[] basicNoteTiming;
    int[] slideNoteTiming;
    int[] flickNoteTiming;
    int[] holdNoteTiming;
    int basicCur = 0;
    int slideCur = 0;
    int flickCur = 0;
    int holdCur = 0;
    #endregion
      
    // ��ġ ���� �����Ͽ� ���� ����
    #region TouchBoolean
    // ��ġ����
    public bool[] press = new bool[21];
    // �����̵�
    public bool[] slide = new bool[21];
    // ��ġ��
    public bool[] intouch = new bool[21];
    // ��ġ����
    public bool[] endtouch = new bool[21];
    // �ø���
    public bool[] flickUp = new bool[21];
    // �ø��ٿ�
    public bool[] flickDown = new bool[21];
    #endregion

    // ��Ʈ ������ �ӵ� ���� ���� ����
    #region noteSpeed
    // ��Ʈ �ӵ� (���� ��Ϳ� ��ȭ��)
    int bpm = 80;
    // �÷��̾� ������
    float offSet = 0;
    // �÷��̾� ��Ʈ �������� �ӵ�
    float playerSpeed = 8;
    #endregion

    // �� ���� Note���� ���� List
    List<Note> inGameNote = new List<Note>();

    // ������ judge�� ����
    int[] judgeChecker =new int[21];
    
    // 임시 판정 텍스트 UI
    public TextMeshProUGUI judgeText;
    
    // �� �Լ��� ����� ���߿� Manager �� �ϳ��� SetUp�� ȣ���ϴ� ������ ���� ��Ź
    private void Start()
    {
        SetUp();

        Managers.Deck.DoNothing(); // Manager Instantiate�� ���� �ӽ� �ڵ� - Manager�� �� ���� ȣ���ؾ� �ν��Ͻ��� ����
        
        int totalNoteCount = basicNotes.Length + slideNotes.Length + flickNotes.Length + holdNoteCount; // 총 노트 수 계산
        Managers.Score.Init(totalNoteCount); // 점수 시스템 초기화
    }


    public void SetUp()
    {
        // noteCreator ������Ʈ �ʱ�ȭ (�� Scene�� �̸� �־���� ��)
        noteCreator = GameObject.Find("NoteCreator").GetComponent<NoteCreator>();

        // jsonManager Ȥ�� ������ ��ũ��Ʈ�� �̿��ؼ� noteInfo�� ����
        // ������ NoteTester�� unity���� ������ ��Ƽ� Testing��
        basicNotes = noteTester.basicNotes;
        slideNotes = noteTester.slideNotes;
        flickNotes = noteTester.flickNotes;
        holdNotes = noteTester.holdNotes;

        // �� �κп��� ��Ʈ�� �������� ������ 'position' �������� ������ �ʿ��� (�̱���)
        // �׷��� �ʴ´ٸ� ���� ���� ��Ʈ���� ����

        // holdNoteBody ���� �̸� count���� �з��Ͽ� ����
        #region holdNoteBodyInfoBind
        // holdBodyCount�� ����� Ȧ�� ��Ʈ���� �� ����
        int holdBodyCount = -1;
        for (int i = 0; i < holdNotes.Length; i++)
        {
            if (holdBodyCount < holdNotes[i].count) {
                holdBodyCount = holdNotes[i].count;
            } 
        }

        for(int i = 0; i <= holdBodyCount; i++)
        {
            holdBodyList.Add(new List<HoldNoteInfo>());        
        }

        for(int i = 0; i < holdNotes.Length; i++)
        {
            holdBodyList[holdNotes[i].count].Add(holdNotes[i]);
        }
        #endregion

        // ������ ��Ʈ���� �����ؾ� �ϴ� �������� ����(�� �������� �̿��ؼ� ����߸��� Ÿ�̹� ����)
        // ������ ��Ʈ�� �������� ������ position�������� �������� �ʴ´ٸ� �̰����� ���ο� ����ü Ȥ�� Class�� ���� �����ص� ��
        // �ش� Class���� ��Ʈ�� position�̶� �ش� ��Ʈ�� noteInfo�� ���°�� �ִ��� üũ�ϴ� ������ ������... �������� �����...?
        // �׷��� �׳� ������ �����ϴ� ���� �ܼ��� ������ ��õ��
        #region setTiming

        basicNoteCount = basicNotes.Length;
        slideNoteCount = slideNotes.Length;
        flickNoteCount = flickNotes.Length;
        holdNoteCount = holdBodyCount + 1;

        basicNoteTiming = new int[basicNoteCount];
        slideNoteTiming = new int[slideNoteCount];
        flickNoteTiming = new int[flickNoteCount];
        if (holdBodyCount >= 0) holdNoteTiming = new int[holdNoteCount];
        for (int i = 0; i < basicNoteCount; i++)
        {
            basicNoteTiming[i] = basicNotes[i].position;
        }
        for(int i = 0; i < slideNoteCount; i++)
        {
            slideNoteTiming[i] = slideNotes[i].position;
        }
        for (int i = 0; i < flickNoteCount; i++)
        {
            flickNoteTiming[i] = flickNotes[i].position;
        }
        for(int i = 0; i < holdNoteCount; i++)
        {
            holdNoteTiming[i] = holdBodyList[i][0].position;
        }
        #endregion

        GameSystem();
    }

    public async UniTask GameSystem()
    {
        while (true)
        {

            currentTime += Time.deltaTime * bpm / 60 * 16;
            // ����
            #region creation
            // basicNote Creation
            while (basicCur < basicNoteCount)
            {
                if (currentTime > basicNoteTiming[basicCur] - 64 * 4 / playerSpeed)
                {
                    inGameNote.Add(noteCreator.CreateBasic(basicNotes[basicCur]).GetComponent<BasicNote>());
                    basicCur++;
                }
                else
                {
                    break;
                }
            }
            // slideNote Creation
            while (slideCur < slideNoteCount)
            {
                if (currentTime > slideNoteTiming[slideCur] - 64 * 4 / playerSpeed)
                {
                    inGameNote.Add(noteCreator.CreateSlide(slideNotes[slideCur]).GetComponent<SlideNote>());
                    slideCur++;
                }
                else
                {
                    break;
                }
            }

            // flickNote Creation
            while (flickCur < flickNoteCount)
            {
                if (currentTime > flickNoteTiming[flickCur] - 64 * 4 / playerSpeed)
                {
                    inGameNote.Add(noteCreator.CreateFlick(flickNotes[flickCur]).GetComponent<FlickNote>());
                    flickCur++;
                }
                else
                {
                    break;
                }
            }
            // holdNote Creation
            while (holdCur < holdNoteCount)
            {
                if (currentTime > holdNoteTiming[holdCur] - 64 * 4 / playerSpeed)
                {
                    inGameNote.Add(noteCreator.CreateHoldBody(holdBodyList[holdCur], playerSpeed).GetComponent<HoldNoteBody>());
                    holdCur++;
                }
                else
                {
                    break;
                }
            }

            #endregion
            // Drop
            #region Drop
            for (int i = 0; i < inGameNote.Count; i++)
            {
                inGameNote[i].Drop(playerSpeed * bpm);
            }
            #endregion

            // ����
            #region Judge
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < inGameNote.Count; j++)
                {
                    if (press[i])
                        judgeChecker[i] = inGameNote[j].ReadJudge(i, bpm, i, currentTime);
                    if (judgeChecker[i] > 0)
                    {
                        if (!(inGameNote[j].gameObject.tag == "HoldNote"))
                        {
                            // �� �κп��� pooling�� �ʿ���
                            GameObject temp = inGameNote[j].gameObject;
                            inGameNote.RemoveAt(j);
                            Destroy(temp);
                            break;
                        }
                        break;
                    }
                    if (slide[i]) judgeChecker[i] = inGameNote[j].ReadJudge(i, bpm, 2, currentTime);
                    if (judgeChecker[i] > 0)
                    {
                        // �� �κп��� pooling�� �ʿ���
                        GameObject temp = inGameNote[j].gameObject;
                        inGameNote.RemoveAt(j);
                        Destroy(temp);
                        break;
                    }
                    if (intouch[i]) judgeChecker[i] = inGameNote[j].ReadJudge(i, bpm, 3, currentTime);
                    if (judgeChecker[i] > 0)
                    {
                        break;
                    }
                    if (endtouch[i]) judgeChecker[i] = inGameNote[j].ReadJudge(i, bpm, 4, currentTime);
                    if (judgeChecker[i] > 0)
                    {
                        // �� �κп��� pooling�� �ʿ���
                        inGameNote[j].gameObject.GetComponent<HoldNoteBody>().ResetNotes();
                        GameObject temp = inGameNote[j].gameObject;
                        inGameNote.RemoveAt(j);
                        Destroy(temp);
                        break;
                    }
                    if (flickUp[i]) judgeChecker[i] = inGameNote[j].ReadJudge(i, bpm, 5, currentTime);
                    if (judgeChecker[i] > 0)
                    {
                        // �� �κп��� pooling�� �ʿ���
                        GameObject temp = inGameNote[j].gameObject;
                        inGameNote.RemoveAt(j);
                        Destroy(temp);
                        break;
                    }
                    if (flickDown[i]) judgeChecker[i] = inGameNote[j].ReadJudge(i, bpm, 6, currentTime);
                    if (judgeChecker[i] > 0)
                    {
                        // �� �κп��� pooling�� �ʿ���
                        GameObject temp = inGameNote[j].gameObject;
                        inGameNote.RemoveAt(j);
                        Destroy(temp);
                        break;
                    }
                    judgeChecker[i] = inGameNote[j].ReadJudge(i, bpm, 0, currentTime);
                    if (judgeChecker[i] > 0)
                    {
                        if (!(inGameNote[j].gameObject.tag == "HoldNote"))
                        {
                            // �� �κп��� pooling�� �ʿ���
                            GameObject temp = inGameNote[j].gameObject;
                            inGameNote.RemoveAt(j);
                            Destroy(temp);
                            break;
                        }
                        else
                        {
                            // �� �κп��� pooling�� �ʿ���
                            inGameNote[j].gameObject.GetComponent<HoldNoteBody>().ResetNotes();
                            GameObject temp = inGameNote[j].gameObject;
                            inGameNote.RemoveAt(j);
                            Destroy(temp);
                            break;
                        }
                    }
                }
            }
            #endregion

            // ������ ���� ���� �� ī�� ȿ�� ������ �� �κ�
            // judgeCheck�� ���� ���� Define�� ���� �κ� ����(enum�� ���� �������� ���� �� ����...)

            // reset
            #region resetForFrame

            int judgeNoteIndex = 0; // noteIndex 추적
            for (int i = 0; i < 21; i++)
            {
                /*
                if (press[i]) Debug.Log("press " + i);
                if (slide[i]) Debug.Log("slide" + i);
                if (intouch[i]) Debug.Log("intouch" + i);
                if (endtouch[i]) Debug.Log("endtouch" + i);
                if (flickDown[i]) Debug.Log("flickDown" + i);
                if (flickUp[i]) Debug.Log("flickUp" + i);
                */
                switch (judgeChecker[i])
                {
                    case 1:
                        judgeText.text = "Miss";
                        Managers.Score.ApplyNoteScore(judgeNoteIndex, Define.JudgementType.Miss, 0);
                        Debug.Log("Miss at " + currentTime + " | Current score: " + Managers.Score.totalScore);
                        break;
                    case 2:
                        judgeText.text = "Good";
                        Managers.Score.ApplyNoteScore(judgeNoteIndex, Define.JudgementType.Good, 0);
                        Debug.Log("Good at " + currentTime + " | Current score: " + Managers.Score.totalScore);
                        break;
                    case 3:
                        judgeText.text = "Great";
                        Managers.Score.ApplyNoteScore(judgeNoteIndex, Define.JudgementType.Great, 0);
                        Debug.Log("Great at " + currentTime + " | Current score: " + Managers.Score.totalScore);
                        break;
                    case 4:
                        judgeText.text = "Perfect";
                        Managers.Score.ApplyNoteScore(judgeNoteIndex, Define.JudgementType.Perfect, 0);
                        Debug.Log("Perfect at " + currentTime + " | Current score: " + Managers.Score.totalScore);
                        break;
                }
                press[i] = false;
                slide[i] = false;
                intouch[i] = false;
                endtouch[i] = false;
                flickUp[i] = false;
                flickDown[i] = false;
                judgeChecker[i] = 0;
            }
            #endregion

            // �Ͻ� ����
            await UniTask.WaitUntil(() => play);
            await UniTask.WaitForFixedUpdate();
            if (!Application.isPlaying)
            {
                break;
            }
        }
    }

}
