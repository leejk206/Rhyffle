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

    public enum CardSuit
    {
        Unknown = 100, Spade = 0, Heart = 1, Diamond = 2, Clover = 3
    }

    public enum CardRank
    {
        Unknown = 100, Joker = 0, Ace = 1,
        Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10, 
        Jack = 11, Queen = 12, King = 13,
    }

    public enum CardRarity
    {
        Unknown = 100, Common = 0, Rare = 1, Epic = 2, Legendary = 3
    }

}
