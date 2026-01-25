using UnityEngine;
using Cysharp.Threading.Tasks;

public class GamePlayer : MonoBehaviour
{
    //테스트하기 편하게 public으로 설정함 추후 변경 가능
    
    //120 기준 기본 권장속도로 3초
    //변속 제어용, 곡 자체적인 노트 속도
    public float songNoteSpeed;
    //플레이어가 설정하는 노트 속도
    //기본 권장속도 5
    public float playerNoteSpeed;

    //채보 길이
    public int songLength;
    //현재 위치
    public int currentLength;
    //채보 정보 변수들


    //일시정지
    public bool pause;
    //플레이 모드
    public bool cardMode;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    

    //사실상 전반적인 리듬게임 처리를 이곳에서 한다고 봐도 된다.
    //GameManager는 이 스크립트를 찾아서 활성화시키는 방식으로 진행한다.
    public async UniTask GameSystem()
    {
        //게임 인트로
        //즉 게임을 시작해서 바로 입장한 시점

        //메인 게임
        //곡 길이(채보 길이)만큼 노트를 내리는 것을 진행
        while (currentLength < songLength)
        {
            //만약 pause라면 정지
            if (!pause)
            {
                //pause는 countdown이 모두 끝난 다음에 해제
                await UniTask.WaitUntil(()=> pause == false);
            }
            else
            {
                //songNoteSpeed와 playerNoteSpeed를 이용해서 각각의 노트의 떨어지는 속도를 parameter로 각각의 노트에 전달 -->

            }
        }

        //게임 끝
        //메인 게임 단계에서 곡 길이(채보 길이)의 끝까지 진행했을 경우
        //플레이어 성적에 따라서 추가적인 UI
    }

}
