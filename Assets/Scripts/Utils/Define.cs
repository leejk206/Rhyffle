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
    }

}
