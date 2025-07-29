using UnityEngine;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;

public class GamePlayer : MonoBehaviour
{

    NoteCreator noteCreator;

    public NoteTest noteTester;

    // 게임 진행 여부
    public bool play = true;
    // 게임 시작시 포지션 (-144번째 박자에서 시작, 즉 144박자 이후면 0에 해당하는 박자를 처리해야 하는 타이밍)
    float currentTime = -144;

    // 노트들 정보, Json에서 Serialize되어 저장된 Info Class들을 배열로 저장 (NoteType.cs 확인)
    #region noteInfos
    BasicNoteInfo[] basicNotes;
    SlideNoteInfo[] slideNotes;
    FlickNoteInfo[] flickNotes;
    HoldNoteInfo[] holdNotes;
    // holdNote를 바탕으로 holdBody들을 묶어 놓은 List
    List<List<HoldNoteInfo>> holdBodyList = new List<List<HoldNoteInfo>>();
    int basicNoteCount;
    int slideNoteCount;
    int flickNoteCount;
    int holdNoteCount;
    #endregion

    // noteInfo에서 각각의 노트들의 position 값들을 미리 int 배열로 저장
    // holdNote의 경우 처음 등장하는 노트를 기준으로 저장
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
      
    // 터치 판정 관련하여 정보 저장
    #region TouchBoolean
    // 터치시작
    public bool[] press = new bool[21];
    // 슬라이드
    public bool[] slide = new bool[21];
    // 터치중
    public bool[] intouch = new bool[21];
    // 터치끝냄
    public bool[] endtouch = new bool[21];
    // 플릭업
    public bool[] flickUp = new bool[21];
    // 플릭다운
    public bool[] flickDown = new bool[21];
    #endregion

    // 노트 떨구는 속도 관련 정보 저장
    #region noteSpeed
    // 노트 속도 (변속 기믹에 변화함)
    int bpm = 120;
    // 플레이어 오프셋
    float offSet = 0;
    // 플레이어 노트 내려오는 속도
    float playerSpeed = 8;
    #endregion

    // 인 게임 Note들을 담은 List
    List<Note> inGameNote = new List<Note>();

    // 판정된 judge를 저장
    int[] judgeChecker =new int[21];
    
    // 이 함수는 지우고 나중에 Manager 중 하나가 SetUp을 호출하는 식으로 수정 부탁
    private void Start()
    {
        SetUp();

        Managers.Deck.DoNothing(); // Manager Instantiate를 위한 임시 코드 - Manager를 한 번은 호출해야 인스턴스가 생김
    }


    public void SetUp()
    {
        // noteCreator 컴포넌트 초기화 (단 Scene에 미리 있어줘야 함)
        noteCreator = GameObject.Find("NoteCreator").GetComponent<NoteCreator>();

        // jsonManager 혹은 별도의 스크립트를 이용해서 noteInfo들 저장
        // 지금은 NoteTester에 unity에서 정보를 담아서 Testing함
        basicNotes = noteTester.basicNotes;
        slideNotes = noteTester.slideNotes;
        flickNotes = noteTester.flickNotes;
        holdNotes = noteTester.holdNotes;

        // 이 부분에서 노트가 떨어지는 순서를 'position' 기준으로 정리가 필요함 (미구현)
        // 그렇지 않는다면 이후 판정 파트에서 꼬임

        // holdNoteBody 정보 미리 count별로 분류하여 저장
        #region holdNoteBodyInfoBind
        // holdBodyCount는 연결된 홀드 노트들의 총 개수
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

        // 각각의 노트들을 판정해야 하는 시점들을 저장(이 시점들을 이용해서 떨어뜨리는 타이밍 결정)
        // 위에서 노트가 떨어지는 순서를 position기준으로 정렬하지 않는다면 이곳에서 새로운 구조체 혹은 Class를 만들어서 적용해도 됨
        // 해당 Class에서 노트의 position이랑 해당 노트가 noteInfo에 몇번째에 있는지 체크하는 정보를 가지면... 가능하지 않을까여...?
        // 그래도 그냥 위에서 정렬하는 것이 단순해 보여서 추천함
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
            // 생성
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

            // 판정
            #region Judge
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < inGameNote.Count; j++)
                {
                    if (press[i])
                        judgeChecker[i] = inGameNote[j].ReadJudge(i, bpm, 1, currentTime);
                    if (judgeChecker[i] > 0)
                    {
                        if (!(inGameNote[j].gameObject.tag == "HoldNote"))
                        {
                            // 이 부분에서 pooling이 필요함
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
                        // 이 부분에서 pooling이 필요함
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
                        // 이 부분에서 pooling이 필요함
                        inGameNote[j].gameObject.GetComponent<HoldNoteBody>().ResetNotes();
                        GameObject temp = inGameNote[j].gameObject;
                        inGameNote.RemoveAt(j);
                        Destroy(temp);
                        break;
                    }
                    if (flickUp[i]) judgeChecker[i] = inGameNote[j].ReadJudge(i, bpm, 5, currentTime);
                    if (judgeChecker[i] > 0)
                    {
                        // 이 부분에서 pooling이 필요함
                        GameObject temp = inGameNote[j].gameObject;
                        inGameNote.RemoveAt(j);
                        Destroy(temp);
                        break;
                    }
                    if (flickDown[i]) judgeChecker[i] = inGameNote[j].ReadJudge(i, bpm, 6, currentTime);
                    if (judgeChecker[i] > 0)
                    {
                        // 이 부분에서 pooling이 필요함
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
                            // 이 부분에서 pooling이 필요함
                            GameObject temp = inGameNote[j].gameObject;
                            inGameNote.RemoveAt(j);
                            Destroy(temp);
                            break;
                        }
                        else
                        {
                            // 이 부분에서 pooling이 필요함
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

            // 판정에 따른 점수 및 카드 효과 적용이 들어갈 부분
            // judgeCheck에 판정 저장 Define의 판정 부분 참조(enum은 적어 놓았으나 적용 못 했음...)

            // reset
            #region resetForFrame
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
                        Debug.Log("Miss at " + currentTime);
                        break;
                    case 2:
                        Debug.Log("Good at " + currentTime);
                        break;
                    case 3:
                        Debug.Log("Great at " + currentTime);
                        break;
                    case 4:
                        Debug.Log("Perfect at " + currentTime);
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

            // 일시 정지
            await UniTask.WaitUntil(() => play);
            await UniTask.WaitForFixedUpdate();
            if (!Application.isPlaying)
            {
                break;
            }
        }
    }

}
