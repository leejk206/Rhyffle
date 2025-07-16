using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class NoteCreationTester : MonoBehaviour
{
    //임시로 만든 prefab 함수들
    public GameObject basicNotePrefab;
    public GameObject slideNotePrefab;
    public GameObject flickNotePrefab;
    public GameObject holdNotePrefab;
    public GameObject holdNoteBody;

    //임시로 만든 노트 리스트들
    //Basic note [num][pos,lane,length] 
    BasicNoteInfo[] basicNote = new BasicNoteInfo[15];
    SlideNoteInfo[] slideNote = new SlideNoteInfo[10];
    FlickNoteInfo[] flickNote = new FlickNoteInfo[5];
    HoldNoteInfo[] holdNote = new HoldNoteInfo[7];
    //아래는 실제로 사용할 dropNote들
    //계속해서 Note배열을 참조하기보단 단순히 숫자비교를 해서 빠르게 생성하기 위함
    List<int> dropBasic = new List<int>();
    List<int> dropSlide = new List<int>();
    List<int> dropFlick = new List<int>();
    List<int> dropHold = new List<int>();


    List<List<int>> holdNoteList = new List<List<int>>();

    #region totalNoteCount
    //총 노트의 개수라고 보면 편하다
    int basicNoteCount = 0;
    int slideNoteCount = 0;
    int flickNoteCount = 0;
    // holdNote만 holdNote 전체를 기준으로 개수를 센다
    int holdNoteCount = 0;
    #endregion

    #region currentNote
    //현재 생성해야하는 리스트의 값
    int createBasicNote = 0;
    int createSlideNote = 0;
    int createFlickNote = 0;
    int createHoldNote = 0;
    #endregion

    //현재 참조해야하는 리스트의 값
    //사용할 지 말지 고민중임
    int curBasicNote = 0;
    int curSlideNote = 0;
    int curFlickNote = 0;

    //현재 생성되어 있는 Note GameObject들의 리스트를 담음
    //pooling이 적용되어 있지 않으며 통합할 지에대한 논의가 필요함
    //개인적으로는 HoldNote를 제외한 다른 모든 노트들은 통합해도 될 것 같음
    List<GameObject> basicNoteObject = new List<GameObject>();
    List<GameObject> slideNoteObject = new List<GameObject>();
    List<GameObject> flickNoteObject = new List<GameObject>();

    List<GameObject> holdNoteBodies = new List<GameObject>();
    
    
    #region touchBoolean
    //그 블록을 터치한 경우
    public bool[] pressed = new bool[21];
    //그 블록으로 들어온 경우(터치 시작할 경우에도 켜짐)
    public bool[] slide = new bool[21];
    //그 블록을 누르고 있는 경우
    public bool[] touched = new bool[21];
    //그 블록에서 손을 뗀 경우
    public bool[] off = new bool[21];
    //그 블록에서 위로 움직인 경우
    public bool[] flickUp = new bool[21];
    //그 블록에서 아래로 움직인 경우
    public bool[] flickDown = new bool[21];
    #endregion

    //이것이 현재 판정되어야 눌렀을 때 판정하는 노트의 시간
    int bpm = 120;
    float currentTime = -144;
    float currentSpeed = 4;
    float offSet = 0;

    public bool play = true;

    private void Start()
    {
        #region noteSetup

        basicNote[0] = new BasicNoteInfo(0,0,3);
        basicNote[1] = new BasicNoteInfo(16, 6, 3);
        basicNote[2] = new BasicNoteInfo(32, 18, 3);
        basicNote[3] = new BasicNoteInfo(40, 0, 2);
        basicNote[4] = new BasicNoteInfo(48, 2, 2);
        basicNote[5] = new BasicNoteInfo(52, 4, 2);
        basicNote[6] = new BasicNoteInfo(56, 6, 2);
        basicNote[7] = new BasicNoteInfo(60, 8, 2);
        basicNote[8] = new BasicNoteInfo(64, 10, 2);
        basicNote[9] = new BasicNoteInfo(96, 0, 4);
        basicNote[10] = new BasicNoteInfo(96, 8, 4);
        basicNote[11] = new BasicNoteInfo(112, 2, 5);
        basicNote[12] = new BasicNoteInfo(112, 7, 3);
        basicNote[13] = new BasicNoteInfo(128, 5, 3);
        basicNote[14] = new BasicNoteInfo(129, 8, 3);

        slideNote[0] = new SlideNoteInfo(144,0,2);
        slideNote[1] = new SlideNoteInfo(152, 2, 2);
        slideNote[2] = new SlideNoteInfo(160, 4, 2);
        slideNote[3] = new SlideNoteInfo(168, 6, 2);
        slideNote[4] = new SlideNoteInfo(176, 8, 2);
        slideNote[5] = new SlideNoteInfo(184, 10, 2);
        slideNote[6] = new SlideNoteInfo(192, 12, 2);
        slideNote[7] = new SlideNoteInfo(200, 14, 2);
        slideNote[8] = new SlideNoteInfo(208, 16, 2);
        slideNote[9] = new SlideNoteInfo(216, 18, 2);

        flickNote[0] = new FlickNoteInfo(248, 4, 3,0);
        flickNote[1] = new FlickNoteInfo(264, 7, 3, 1);
        flickNote[2] = new FlickNoteInfo(280, 10, 3, 0);
        flickNote[3] = new FlickNoteInfo(312, 0, 3, 0);
        flickNote[4] = new FlickNoteInfo(312, 12, 3, 0);

        holdNote[0] = new HoldNoteInfo(328, 0, 0, 0, 3);
        holdNote[1] = new HoldNoteInfo(344, 10, 0, 1, 5);
        holdNote[2] = new HoldNoteInfo(352, 17, 1, 0, 2);
        holdNote[3] = new HoldNoteInfo(360, 20, 2, 0, 1);
        holdNote[4] = new HoldNoteInfo(372, 8, 1, 1, 7);
        holdNote[5] = new HoldNoteInfo(380, 6, 1, 1, 2);
        holdNote[6] = new HoldNoteInfo(444, 18, 2, 1, 2);


        //

        // 이 부분에 노트들을 등장 순서대로 정렬하는 코드가 필요함
        // Basic, Slide, Flick은 쉽게 할 수 있으나
        // Hold에 대해서는 논의가 필요할 것 같음
        // Hold노트의 등장하는 시점이 빠른 HoldNote묶음 부터 낮은 count를 입력하는 방식으로 정령 및 count 바꾸는 것
        // 혹은 Count를 Hold를 묶는 데에만 사용하고 HoldNote의 첫 노트의 등장시점 순서대로 정렬하는 방법도 있다

        //

        //holdNote 사전 세팅(노트들 묶기)
        holdNoteCount = -1;
        for(int i = 0; i < holdNote.Length; i++)
        {
            if (holdNote[i].count > holdNoteCount)
            {
                holdNoteCount = holdNote[i].count;
            }
        }
        for(int i = 0; i <= holdNoteCount; i++)
        {
            holdNoteList.Add(new List<int>());
        }
        for(int i = 0; i <= holdNoteCount; i++)
        {
            for(int j = 0; j < holdNote.Length; j++)
            {
                if (holdNote[j].count == i)
                {
                    holdNoteList[i].Add(j);
                }
            }
        }





        #endregion
        basicNoteCount = basicNote.Length;
        slideNoteCount = slideNote.Length;
        flickNoteCount = flickNote.Length;
        //생성할 때의 타이밍만 잡는 List 초기화
        for(int i = 0; i < basicNoteCount; i++)
        {
            dropBasic.Add(basicNote[i].position);
        }
        for (int i = 0; i < slideNoteCount; i++)
        {
            dropSlide.Add(slideNote[i].position);
        }
        for (int i = 0; i < flickNoteCount; i++)
        {
            dropFlick.Add(flickNote[i].position);
        }
        for(int i = 0; i <= holdNoteCount; i++)
        {
            dropHold.Add(holdNote[holdNoteList[i][0]].position);
        }
        TestPlay();
    }

    public async UniTask TestPlay()
    {
        while (true)
        {
            if (!play)
            {
                await UniTask.WaitUntil(() => play);
                //정지 이후 개재 시 초기화
                for (int i = 0; i < 21; i++)
                {
                    pressed[i] = false;
                    slide[i] = false;
                    flickUp[i] = false;
                    flickDown[i] = false;
                    touched[i] = false;
                    off[i] = false;
                }
                continue;
            }

            currentTime += 16 * bpm / 60 * Time.deltaTime;
            //저장된 노트 생성
            //Basic Note
            while (true)
            {
                if (createBasicNote < basicNoteCount)
                {
                    if (dropBasic[createBasicNote] <= currentTime + 64) {
                        //생성하고 다음 index로 넘어감
                        GameObject temp = Instantiate(basicNotePrefab);
                        temp.GetComponent<BasicNote>().Set(basicNote[createBasicNote].line, basicNote[createBasicNote].length);
                        temp.GetComponent<BasicNote>().judge = basicNote[createBasicNote].position;
                        basicNoteObject.Add(temp);
                        createBasicNote++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            //Slide Note
            while (true)
            {
                if (createSlideNote < slideNoteCount)
                {
                    if (dropSlide[createSlideNote] <= currentTime + 64)
                    {
                        //생성하고 다음 index로 넘어감
                        GameObject temp = Instantiate(slideNotePrefab);
                        temp.GetComponent<SlideNote>().Set(slideNote[createSlideNote].line, slideNote[createSlideNote].length);
                        temp.GetComponent<SlideNote>().judge = slideNote[createSlideNote].position;
                        slideNoteObject.Add(temp);
                        createSlideNote++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            //Flick Note
            while (true)
            {
                if (createFlickNote < flickNoteCount)
                {
                    if (dropFlick[createFlickNote] <= currentTime + 64)
                    {
                        //생성하고 다음 index로 넘어감
                        GameObject temp = Instantiate(flickNotePrefab);
                        var tempFlickNote = temp.GetComponent<FlickNote>();
                        tempFlickNote.Set(flickNote[createFlickNote].line, flickNote[createFlickNote].length);
                        tempFlickNote.SetDir(flickNote[createFlickNote].dir);
                        tempFlickNote.judge = flickNote[createFlickNote].position;
                        flickNoteObject.Add(temp);
                        createFlickNote++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            //Hold Note
            while (true)
            {
                if(createHoldNote <= holdNoteCount)
                {
                    if(dropHold[createHoldNote] <= currentTime + 64)
                    {
                        //생성하고 다음 index로 넘어감
                        GameObject tempBody = Instantiate(holdNoteBody);
                        var tempHoldNoteBody = tempBody.GetComponent<HoldNoteBody>();
                        int noteListNum = holdNoteList[createHoldNote].Count;
                        for(int i = 0; i < noteListNum; i++)
                        {
                            GameObject tempNote = Instantiate(holdNotePrefab);
                            var tempHoldNote = tempNote.GetComponent<HoldNote>();
                            int noteNum = holdNoteList[createHoldNote][i];
                            tempHoldNote.Set(holdNote[noteNum].line, holdNote[noteNum].length, (holdNote[noteNum].position - currentTime) * 0.15625f);
                            tempHoldNote.judge = holdNote[noteNum].position;
                            tempHoldNoteBody.ConnectNotes(tempNote);
                        }
                        holdNoteBodies.Add(tempBody);
                        createHoldNote++;
                        Debug.Log("Stop");
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            


            //매턴 노트 내리기 및 판정
            //지금 현재 코드는 땜빵용임 GetComponent를 너무 많이 사용함
            //추후 완성할 코드에서는 List와 같은 것으로 정보들을 담아두고 List를 참조하여 판정을 함
            //그 이후 노트를 삭제시켜야 할 경우에만 GetComponent를 실행시키는 방식으로 수정할 예정
            //Pooling도 아직 미적용상태
            //Basic Note
            for(int i = 0; i < basicNoteObject.Count; i++)
            {
                var tempBasic = basicNoteObject[i].GetComponent<BasicNote>();
                //미스 판정
                tempBasic.Drop(currentSpeed * bpm);
                if(16 * bpm/600 * 2 + tempBasic.judge < currentTime)
                {
                    GameObject temp = basicNoteObject[i];
                    basicNoteObject.RemoveAt(i);
                    Destroy(temp);
                    i--;
                    Debug.Log("Miss");
                    continue;
                }
                for (int j = tempBasic.line; j < tempBasic.line + tempBasic.length; j++)
                {
                    if (pressed[j])
                    {
                        if (tempBasic.judge - (16 * bpm / 600f * 3) < currentTime)
                        {
                            GameObject temp = basicNoteObject[i];
                            basicNoteObject.RemoveAt(i);
                            Destroy(temp);
                            i--;
                            if (tempBasic.judge + (16 * bpm / 600f * 1.5f) < currentTime)
                            {
                                Debug.Log("Good");
                            }
                            else if (tempBasic.judge + (16 * bpm / 600f) < currentTime)
                            {
                                Debug.Log("Great");
                            }
                            else if (tempBasic.judge - (16 * bpm / 600f) < currentTime)
                            {
                                Debug.Log("Perfect");
                            }
                            else if (tempBasic.judge - (16 * bpm / 600f * 1.5f) < currentTime)
                            {
                                Debug.Log("Great");
                            }
                            else if (tempBasic.judge - (16 * bpm / 600f * 2f) < currentTime)
                            {
                                Debug.Log("Good");
                            }
                            else
                            {
                                Debug.Log("Miss");
                            }
                        }
                        pressed[j] = false;
                        break;
                    }
                }
            }
            //SlideNote
            for (int i = 0; i < slideNoteObject.Count; i++)
            {
                var tempSlide = slideNoteObject[i].GetComponent<SlideNote>();
                tempSlide.Drop(currentSpeed * bpm);
                if (16 * bpm / 600f * 2 + tempSlide.judge < currentTime)
                {
                    GameObject temp = slideNoteObject[i];
                    slideNoteObject.RemoveAt(i);
                    Destroy(temp);
                    i--;
                    Debug.Log("Miss");
                    continue;
                }
                for (int j = tempSlide.line; j < tempSlide.line + tempSlide.length; j++)
                {
                    if (slide[j])
                    {
                        if (tempSlide.judge - (16 * bpm / 600f * 3) < currentTime)
                        {
                            GameObject temp = slideNoteObject[i];
                            slideNoteObject.RemoveAt(i);
                            Destroy(temp);
                            i--;
                            if (tempSlide.judge + (16 * bpm / 600f * 1.5f) < currentTime)
                            {
                                Debug.Log("Good");
                            }
                            else if (tempSlide.judge + (16 * bpm / 600f) < currentTime)
                            {
                                Debug.Log("Great");
                            }
                            else if (tempSlide.judge - (16 * bpm / 600f) < currentTime)
                            {
                                Debug.Log("Perfect");
                            }
                            else if (tempSlide.judge - (16 * bpm / 600f * 1.5f) < currentTime)
                            {
                                Debug.Log("Great");
                            }
                            else if (tempSlide.judge - (16 * bpm / 600f * 2f) < currentTime)
                            {
                                Debug.Log("Good");
                            }
                            else
                            {
                                Debug.Log("Miss");
                            }
                        }
                        slide[j] = false;
                        break;
                    }
                }
            }
            //FlickNote
            for (int i = 0; i < flickNoteObject.Count; i++)
            {
                var tempFlick = flickNoteObject[i].GetComponent<FlickNote>();
                tempFlick.Drop(currentSpeed * bpm);
                //넘어가서 miss 판정
                if (16 * bpm / 600f * 2 + tempFlick.judge < currentTime)
                {
                    GameObject temp = flickNoteObject[i];
                    flickNoteObject.RemoveAt(i);
                    Destroy(temp);
                    i--;
                    Debug.Log("Miss");
                    continue;
                }
                //down flick
                if (tempFlick.flicDir == 0) {
                    for (int j = tempFlick.line; j < tempFlick.line + tempFlick.length; j++)
                    {
                        if (flickDown[j])
                        {
                            if(tempFlick.judge - (16 * bpm / 600f * 3) < currentTime)
                            {
                                GameObject temp = flickNoteObject[i];
                                flickNoteObject.RemoveAt(i);
                                Destroy (temp);
                                i--;
                                if(tempFlick.judge + (16*bpm/600f * 1.5f) < currentTime)
                                {
                                    Debug.Log("Good");
                                }else if(tempFlick.judge  + (16*bpm/600f) < currentTime)
                                {
                                    Debug.Log("Great");
                                }else if(tempFlick.judge - (16*bpm/600f) < currentTime)
                                {
                                    Debug.Log("Perfect");
                                }else if (tempFlick.judge - (16 * bpm / 600f * 1.5f) < currentTime)
                                {
                                    Debug.Log("Great");
                                }else if(tempFlick.judge - (16*bpm/600f * 2f) < currentTime)
                                {
                                    Debug.Log("Good");
                                }
                                else
                                {
                                    Debug.Log("Miss");
                                }
                            }
                            flickDown[j] = false;
                            break;
                        }
                    } 
                }
                //up flick
                else
                {
                    for (int j = tempFlick.line; j < tempFlick.line + tempFlick.length; j++)
                    {
                        if (flickUp[j])
                        {
                            if (tempFlick.judge - (16 * bpm / 600f * 3) < currentTime)
                            {
                                GameObject temp = flickNoteObject[i];
                                flickNoteObject.RemoveAt(i);
                                Destroy(temp);
                                i--;
                                if (tempFlick.judge + (16 * bpm / 600f * 1.5f) < currentTime)
                                {
                                    Debug.Log("Good");
                                }
                                else if (tempFlick.judge + (16 * bpm / 600f) < currentTime)
                                {
                                    Debug.Log("Great");
                                }
                                else if (tempFlick.judge - (16 * bpm / 600f) < currentTime)
                                {
                                    Debug.Log("Perfect");
                                }
                                else if (tempFlick.judge - (16 * bpm / 600f * 1.5f) < currentTime)
                                {
                                    Debug.Log("Great");
                                }
                                else if (tempFlick.judge - (16 * bpm / 600f * 2f) < currentTime)
                                {
                                    Debug.Log("Good");
                                }
                                else
                                {
                                    Debug.Log("Miss");
                                }
                            }
                            flickUp[j] = false;
                            break;
                        }
                    }
                }
            }

            //HoldNote
            for(int i = 0; i < holdNoteBodies.Count; i++)
            {
                var tempHold = holdNoteBodies[i].GetComponent<HoldNoteBody>();
                tempHold.Drop(currentSpeed * bpm);
                tempHold.DrawLine();
                HoldNote tempHoldNote = tempHold.CurrentJudge();

                if(16 *bpm/600 *2 + tempHoldNote.judge < currentTime)
                {
                    tempHold.MissNote();
                    Debug.Log("MissHold");
                    continue;
                }

                switch (tempHold.HoldType())
                {
                    // 처음 터치하는 경우
                    case 0:
                        for(int j = tempHoldNote.line;  j < tempHoldNote.length + tempHoldNote.line; j++)
                        {
                            if (pressed[j])
                            {
                                if (tempHoldNote.judge - (16 * bpm / 600f * 3) < currentTime)
                                {
                                    GameObject temp = basicNoteObject[i];
                                    tempHoldNote.HideNote();
                                    
                                    if (tempHoldNote.judge + (16 * bpm / 600f * 1.5f) < currentTime)
                                    {
                                        Debug.Log("GoodHold");
                                    }
                                    else if (tempHoldNote.judge + (16 * bpm / 600f) < currentTime)
                                    {
                                        Debug.Log("GreatHold");
                                    }
                                    else if (tempHoldNote.judge - (16 * bpm / 600f) < currentTime)
                                    {
                                        Debug.Log("PerfectHold");
                                    }
                                    else if (tempHoldNote.judge - (16 * bpm / 600f * 1.5f) < currentTime)
                                    {
                                        Debug.Log("GreatHold");
                                    }
                                    else if (tempHoldNote.judge - (16 * bpm / 600f * 2f) < currentTime)
                                    {
                                        Debug.Log("GoodHold");
                                    }
                                    else
                                    {
                                        Debug.Log("MissHold");
                                        tempHold.MissNote();
                                    }
                                }
                                tempHold.NextNote();
                                slide[j] = false;
                                break;
                            }
                        }       
                        break;
                    // 누르고 있어야 하는 경우
                    case 1:
                        for (int j = tempHoldNote.line; j < tempHoldNote.length + tempHoldNote.line; j++)
                        {
                            if (touched[j])
                            {
                                if (tempHoldNote.judge - (16 * bpm / 600f) < currentTime)
                                {
                                    GameObject temp = basicNoteObject[i];
                                    tempHoldNote.HideNote();

                                    if (tempHoldNote.judge + (16 * bpm / 600f * 1.5f) < currentTime)
                                    {
                                        Debug.Log("GoodHold");
                                    }
                                    else if (tempHoldNote.judge + (16 * bpm / 600f) < currentTime)
                                    {
                                        Debug.Log("GreatHold");
                                    }
                                    else
                                    {
                                        Debug.Log("PerfectHold");
                                    }
                                }
                                tempHold.NextNote();
                                break;
                            }
                        }
                        break;
                    // 떼야 하는 경우
                    case 2:
                        for (int j = tempHoldNote.line; j < tempHoldNote.length + tempHoldNote.line; j++)
                        {
                            if (off[j])
                            {
                                if (tempHoldNote.judge - (16 * bpm / 600f * 3) < currentTime)
                                {
                                    Debug.Log("Help");
                                    GameObject temp = basicNoteObject[i];
                                    tempHoldNote.HideNote();

                                    if (tempHoldNote.judge + (16 * bpm / 600f * 1.5f) < currentTime)
                                    {
                                        Debug.Log("GoodHold");
                                    }
                                    else if (tempHoldNote.judge + (16 * bpm / 600f) < currentTime)
                                    {
                                        Debug.Log("GreatHold");
                                    }
                                    else if (tempHoldNote.judge - (16 * bpm / 600f) < currentTime)
                                    {
                                        Debug.Log("PerfectHold");
                                    }
                                    else if (tempHoldNote.judge - (16 * bpm / 600f * 1.5f) < currentTime)
                                    {
                                        Debug.Log("GreatHold");
                                    }
                                    else if (tempHoldNote.judge - (16 * bpm / 600f * 2f) < currentTime)
                                    {
                                        Debug.Log("GoodHold");
                                    }
                                    else
                                    {
                                        Debug.Log("MissHold");
                                        tempHold.MissNote();
                                    }
                                }
                                tempHold.NextNote();
                                off[j] = false;
                                break;
                            }
                        }
                        break;
                }

                if (tempHold.IsRemove())
                {
                    Debug.Log("ExecutedRemove");
                    holdNoteBodies.RemoveAt(i);
                    tempHold.DeleteNotes();
                    Destroy(tempHold.gameObject);
                }

            }

            //노트 위치 따라서
            //지금은 판정 테스트 때문에 click을 사용하지만 추후에는 touch를 사용할 예정
            

            //판정 리셋
            for (int i = 0; i < 21; i++)
            {
                pressed[i] = false;
                slide[i] = false;
                flickUp[i] = false;
                flickDown[i] = false;
                touched[i] = false;
                off[i] = false;
            }
            await UniTask.WaitForFixedUpdate();
        }
    }
    
}
