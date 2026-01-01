using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using static Define;
using System.Collections.Generic;
using System;

public class GamePlayer : MonoBehaviour
{

    NoteCreator noteCreator;

    // Json 파일의 채보 정보가 담길 class
    NoteJson noteJson;



    // Erase Later

    public TMP_Text text;
    public TMP_Text text2;
    public TMP_Text text3;

    

    // ���� ���� ����
    public bool play = true;
    // ���� ���۽� ������ (-144��° ���ڿ��� ����, �� 144���� ���ĸ� 0�� �ش��ϴ� ���ڸ� ó���ؾ� �ϴ� Ÿ�̹�)
    public float currentTime = -1000;
    // 이전 프레임 플레이 상태 플래그 -> 매 프레임마다 초기화 되는 것을 방지
    private bool _wasPlaying = true;

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
    int bpm = 120;
    // �÷��̾� ������
    float offSet = 0;
    // �÷��̾� ��Ʈ �������� �ӵ�
    float playerSpeed = 8;
    #endregion

    // Offset between chart_tool and this game
    float chartToolOffset = 6.4f;

    // �� ���� Note���� ���� List
    List<Note> inGameNote = new List<Note>();

    // ������ judge�� ����
    JudgementType[] judgeChecker;
    JudgementType[] secondJudgeChecker;

    // this is used on HoldNote to check whether pressed finger is taken off or not
    public int[] touchStart = new int[21];
    public int[] touchEnd = new int[21];
    public int[] touchCon = new int[21];

    // 임시 판정 텍스트 UI
    public TextMeshProUGUI judgeText;
    
    // �� �Լ��� ����� ���߿� Manager �� �ϳ��� SetUp�� ȣ���ϴ� ������ ���� ��Ź
    private void Start()
    {
        currentTime = -64;
        SetUp();

        Managers.Deck.DoNothing(); // For Manager Instantiate
        
        int totalNoteCount = basicNotes.Length + slideNotes.Length + flickNotes.Length + holdNoteCount; // 총 노트 수 계산
        Managers.Score.Init(totalNoteCount); // 점수 시스템 초기화
    }


    public void SetUp()
    {
        try
        {
            Managers.Json.LoadJson();
            noteJson = Managers.Json.ReturnJson();
            // noteCreator ������Ʈ �ʱ�ȭ (�� Scene�� �̸� �־���� ��)
            noteCreator = GameObject.Find("NoteCreator").GetComponent<NoteCreator>();


            // Json에서 받은 노트 정보들을 저장
            basicNotes = noteJson.NormalNotes;
            slideNotes = noteJson.SlideNotes;
            flickNotes = noteJson.FlickNotes;
            holdNotes = noteJson.HoldNotes;

            // �� �κп��� ��Ʈ�� �������� ������ 'position' �������� ������ �ʿ��� (�̱���)
            // �׷��� �ʴ´ٸ� ���� ���� ��Ʈ���� ����

            // holdNoteBody ���� �̸� count���� �з��Ͽ� ����
            #region holdNoteBodyInfoBind
            // holdBodyCount�� ����� Ȧ�� ��Ʈ���� �� ����
            int holdBodyCount = -1;
            for (int i = 0; i < holdNotes.Length; i++)
            {
                if (holdBodyCount < holdNotes[i].count)
                {
                    holdBodyCount = holdNotes[i].count;
                }
            }

            for (int i = 0; i <= holdBodyCount; i++)
            {
                holdBodyList.Add(new List<HoldNoteInfo>());
            }

            for (int i = 0; i < holdNotes.Length; i++)
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
            for (int i = 0; i < slideNoteCount; i++)
            {
                slideNoteTiming[i] = slideNotes[i].position;
            }
            for (int i = 0; i < flickNoteCount; i++)
            {
                flickNoteTiming[i] = flickNotes[i].position;
            }
            for (int i = 0; i < holdNoteCount; i++)
            {
                holdNoteTiming[i] = holdBodyList[i][0].position;
            }
            judgeChecker = new JudgementType[21];
            secondJudgeChecker = new JudgementType[21];
            #endregion
            GameSystem();
        }
        catch (Exception e) { 
            text.text = e.ToString();   
        }
    }

    public async UniTask GameSystem()
    {
        while (true)
        {

            currentTime += Time.deltaTime * (bpm / 60) * 16;
            // ����
            #region creation
            // basicNote Creation
            while (basicCur < basicNoteCount)
            {
                if (currentTime > basicNoteTiming[basicCur] * chartToolOffset - (64 * 4 / playerSpeed))
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
                if (currentTime > slideNoteTiming[slideCur] * chartToolOffset - (64 * 4 / playerSpeed))
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
                if (currentTime > flickNoteTiming[flickCur] * chartToolOffset - (64 * 4 / playerSpeed))
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
                if (currentTime > holdNoteTiming[holdCur] * chartToolOffset - (64 * 4 / playerSpeed))
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
                if (touchStart[i] != -1)
                {
                    text2.text = touchStart[i].ToString();
                }
                if (touchEnd[i] != -1)
                {
                    text3.text = touchEnd[i].ToString();
                }
                for (int j = 0; j < inGameNote.Count; j++)
                {
                    if (judgeChecker[i] != JudgementType.NotChecked)
                    {
                        break;
                    }
                    if (press[i])
                    {
                        judgeChecker[i] = inGameNote[j].ReadJudge(i, bpm, 1, currentTime);
                        if (judgeChecker[i] != JudgementType.Checked && judgeChecker[i] != JudgementType.NotChecked)
                        {
                            if (inGameNote[j].fingerID == -1)
                            {
                                inGameNote[j].fingerID = touchStart[i];
                            }
                            if (!(inGameNote[j].gameObject.tag == "HoldNote"))
                            {
                                // Need Pooling
                                GameObject temp = inGameNote[j].gameObject;
                                inGameNote.RemoveAt(j);
                                Destroy(temp);
                            }
                            else
                            {
                                if (judgeChecker[i] == JudgementType.Miss)
                                {
                                    inGameNote[j].gameObject.GetComponent<HoldNoteBody>().ResetNotes();
                                    GameObject temp = inGameNote[j].gameObject;
                                    inGameNote.RemoveAt(j);
                                    Destroy(temp);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    if (slide[i])
                    {
                        inGameNote[j].pressFingerID = touchCon[i];
                        judgeChecker[i] = inGameNote[j].ReadJudge(i, bpm, 2, currentTime);
                        if (judgeChecker[i] != JudgementType.Checked && judgeChecker[i] != JudgementType.NotChecked)
                        {
                            // Need Pooling
                            GameObject temp = inGameNote[j].gameObject;
                            inGameNote.RemoveAt(j);
                            Destroy(temp);
                            break;
                        }
                    }
                    if (intouch[i])
                    {
                        inGameNote[j].pressFingerID = touchCon[i];
                        judgeChecker[i] = inGameNote[j].ReadJudge(i, bpm, 3, currentTime);
                        if (judgeChecker[i] != JudgementType.Checked && judgeChecker[i] != JudgementType.NotChecked)
                        {
                            break;
                        }
                    }
                    if (endtouch[i])
                    {
                        inGameNote[j].endFingerID = touchEnd[i];
                        judgeChecker[i] = inGameNote[j].ReadJudge(i, bpm, 4, currentTime);
                        if (judgeChecker[i] != JudgementType.Checked && judgeChecker[i] != JudgementType.NotChecked)
                        {
                            if (judgeChecker[i] == JudgementType.SpMiss)
                            {
                                judgeChecker[i] = JudgementType.Checked;
                                secondJudgeChecker[i] = JudgementType.Miss;
                            }


                            // 삭제
                            inGameNote[j].gameObject.GetComponent<HoldNoteBody>().ResetNotes();
                            GameObject temp = inGameNote[j].gameObject;
                            inGameNote.RemoveAt(j);
                            Destroy(temp);
                            break;
                        }
                    }
                    if (flickUp[i])
                    {
                        judgeChecker[i] = inGameNote[j].ReadJudge(i, bpm, 5, currentTime);
                        if (judgeChecker[i] != JudgementType.Checked && judgeChecker[i] != JudgementType.NotChecked)
                        {
                            GameObject temp = inGameNote[j].gameObject;
                            inGameNote.RemoveAt(j);
                            Destroy(temp);
                            break;
                        }
                    }
                    if (flickDown[i])
                    {
                        judgeChecker[i] = inGameNote[j].ReadJudge(i, bpm, 6, currentTime);
                        if (judgeChecker[i] != JudgementType.Checked && judgeChecker[i] != JudgementType.NotChecked)
                        {
                            GameObject temp = inGameNote[j].gameObject;
                            inGameNote.RemoveAt(j);
                            Destroy(temp);
                            break;
                        }
                    }
                    judgeChecker[i] = inGameNote[j].ReadJudge(i, bpm, 0, currentTime);
                    if (judgeChecker[i] == JudgementType.Miss)
                    {
                        if (inGameNote[j].gameObject.tag == "HoldNote")
                        {
                            Debug.Log("Success");
                            // need pooling
                            inGameNote[j].gameObject.GetComponent<HoldNoteBody>().ResetNotes();
                            GameObject temp = inGameNote[j].gameObject;
                            inGameNote.RemoveAt(j);
                            Destroy(temp);
                            break;
                        }
                        else
                        {
                            // need pooling
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



                // This part need to fix
                // currently, cardRank is not initialized at the point 'GameSystem()' is called
                // 
                // so I put try-catch exception call and tested if cardRank is changing into rank of cards in 'Managers.Card.FieldCards'
                // 'cardRank' is initalized to rank of card in 'Managers.Card.FieldCards' only when "A" key pressed in game (Drawing All Cards)
                
                int cardRank = 0; 
                try
                {
                    cardRank = Managers.Card.FieldCards[i / 3].CardRank;
                }catch(Exception e)
                {

                }
                switch (judgeChecker[i])
                {
                    case JudgementType.Miss:
                        //judgeText.text = "Miss";
                        Managers.Score.ApplyNoteScore(judgeNoteIndex, Define.JudgementType.Miss, cardRank);
                        Debug.Log("Miss at " + currentTime + " | Current score: " + Managers.Score.totalScore);
                        text.text = "Miss";
                        break;
                    case JudgementType.Good:
                        //judgeText.text = "Good";
                        Managers.Score.ApplyNoteScore(judgeNoteIndex, Define.JudgementType.Good, cardRank);
                        Debug.Log("Good at " + currentTime + " | Current score: " + Managers.Score.totalScore);
                        text.text = "Good";
                        break;
                    case JudgementType.Great:
                        //judgeText.text = "Great";
                        Managers.Score.ApplyNoteScore(judgeNoteIndex, Define.JudgementType.Great, cardRank);
                        Debug.Log("Great at " + currentTime + " | Current score: " + Managers.Score.totalScore);
                        text.text = "Great";
                        break;
                    case JudgementType.Perfect:
                        //judgeText.text = "Perfect";
                        Managers.Score.ApplyNoteScore(judgeNoteIndex, Define.JudgementType.Perfect, cardRank);
                        Debug.Log("Perfect at " + currentTime + " | Current score: " + Managers.Score.totalScore);
                        text.text = "Perfect";
                        break;
                    case JudgementType.Checked:
                        break;
                    case JudgementType.NotChecked:
                        break;
                }
                switch (secondJudgeChecker[i])
                {
                    case JudgementType.Miss:
                        //judgeText.text = "Miss";
                        Managers.Score.ApplyNoteScore(judgeNoteIndex, Define.JudgementType.Miss, cardRank);
                        Debug.Log("Miss at " + currentTime + " | Current score: " + Managers.Score.totalScore);
                        text.text = "Miss";
                        break;
                    case JudgementType.NotChecked:
                        break;
                }


                press[i] = false;
                slide[i] = false;
                intouch[i] = false;
                endtouch[i] = false;
                flickUp[i] = false;
                flickDown[i] = false;
                
                touchStart[i] = -1;
                touchEnd[i] = -1;

                judgeChecker[i] = JudgementType.NotChecked;
                secondJudgeChecker[i] = JudgementType.NotChecked;
            }
            #endregion

            if (!play && _wasPlaying)
            {
                for (int i = 0; i < 21; i++)
                {
                    press[i] = false;
                    slide[i] = false;
                    intouch[i] = false;
                    endtouch[i] = false;
                    flickUp[i] = false;
                    flickDown[i] = false;

                    touchStart[i] = -1;
                    touchEnd[i] = -1;

                    secondJudgeChecker[i] = JudgementType.NotChecked;
                    judgeChecker[i] = JudgementType.NotChecked;
                }

                // Debug.Log("TouchBoolean 초기화");
            }
            
            _wasPlaying = play;

            // �Ͻ� ����
            await UniTask.WaitUntil(() => play);
            if (!Application.isPlaying)
            {
                break;
            }
        }
    }

}
