using UnityEngine;

public class Define
// 여러 정의가 들어간 파일. State 패턴 이용을 위함.
{
    public enum Scene
    { 
        Unknown, // 오류
        Lobby, // 메인
        Game, // 게임
        Loading, // 로딩
        LoadingGame, // 게임 시작 전 로딩 (곡 설명/난이도/모드 정보/ 점수 정보)
        ScoreBoard, //게임 결과화면
    }

}
