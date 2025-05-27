using UnityEngine;

// 나중에 UIManager 같은 파일 있을 경우 충돌 방지로 임시로 지은 이름
// UIManger로 옮겨질 경우 region으로 구간 나누어 저장할 예정
public class GameUIManager
{
    // 스크린 가로 세로 길이
    float screenWidth = Screen.width;
    float screenHeight = Screen.height;

    //화면 해상도에 따라 UI 위치 재조정
    //우선은 화면 가로세로 비율에 따라서 위치와 크기만 맞춰둠
    //추후 변경 예정 
    void ResizeUI(){
        
    }

    //화면 일시정지 창 띄우기
    void Pause()
    {

    }

    //화면 일시정지 풀고 3초 카운팅
    void Resume()
    {

    }
}
