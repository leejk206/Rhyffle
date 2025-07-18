using UnityEngine;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;

public class GamePlayer : MonoBehaviour
{

    public GameObject noteCreator;



    // 게임 진행 여부
    public bool play = true;
    // 게임 시작시 포지션 (-144번째 박자에서 시작, 즉 144박자 이후면 0에 해당하는 박자를 처리해야 하는 타이밍)
    float currentTime = 144;

    // 노트들 정보, Json에서 Serialize되어 저장된 Info Class들을 배열로 저장 (NoteType.cs 확인)
    #region noteInfos
    BasicNoteInfo[] basicNotes;
    SlideNoteInfo[] slideNotes;
    FlickNoteInfo[] flickNotes;
    HoldNoteInfo[] holdNotes;
    // holdNote를 바탕으로 holdBody들을 묶어 놓은 List
    List<List<HoldNoteInfo>> holdBodyList = new List<List<HoldNoteInfo>>();
    #endregion

    // noteInfo에서 각각의 노트들의 position 값들을 미리 int 배열로 저장
    // holdNote의 경우 처음 등장하는 노트를 기준으로 저장
    #region spawnPositions
    int[] basicNoteTiming;
    int[] slideNoteTiming;
    int[] flickNoteTiming;
    int[] holdNoteTiming;
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
    float currentSpeed = 4;
    #endregion

    // 인 게임 Note들을 담은 GameObject List
    #region inGameNotes
    List<GameObject> inGameBasic = new List<GameObject>();
    List<GameObject> inGameSlide = new List<GameObject>();
    List<GameObject> inGameFlick = new List<GameObject>();
    // Hold만 특이하게 HoldNoteBody를 담음
    List<GameObject> inGameHold = new List<GameObject>();
    #endregion

    public void SetUp()
    {
        // jsonManager 혹은 별도의 스크립트를 이용해서 noteInfo들 저장

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

        for(int i = 0; i < holdBodyCount; i++)
        {
            holdBodyList.Add(new List<HoldNoteInfo>());        
        }

        for(int i = 0; i < holdNotes.Length; i++)
        {
            if (holdNotes[i].count == i) holdBodyList[i].Add(holdNotes[i]);
        }
        #endregion

        // 각각의 노트들을 판정해야 하는 시점들을 저장(이 시점들을 이용해서 떨어뜨리는 타이밍 결정)
        // 위에서 노트가 떨어지는 순서를 position기준으로 정렬하지 않는다면 이곳에서 새로운 구조체 혹은 Class를 만들어서 적용해도 됨
        // 해당 Class에서 노트의 position이랑 해당 노트가 noteInfo에 몇번째에 있는지 체크하는 정보를 가지면... 가능하지 않을까여...?
        // 그래도 그냥 위에서 정렬하는 것이 단순해 보여서 추천함


        #region setTiming
        basicNoteTiming = new int[basicNotes.Length];
        slideNoteTiming = new int[slideNotes.Length];
        flickNoteTiming = new int[flickNotes.Length];
        if (holdBodyCount >= 0) holdNoteTiming = new int[holdBodyCount + 1];
        for (int i = 0; i < basicNotes.Length; i++)
        {
            basicNoteTiming[i] = basicNotes[i].position;
        }
        for(int i = 0; i < slideNotes.Length; i++)
        {
            slideNoteTiming[i] = slideNotes[i].position;
        }
        for (int i = 0; i < flickNotes.Length; i++)
        {
            flickNoteTiming[i] = flickNotes[i].position;
        }
        for(int i = 0; i < holdBodyCount; i++)
        {

        }
        #endregion
    }

    public async UniTask GameSystem()
    {



        if (play)
        {

        }
    }


}
