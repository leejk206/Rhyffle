using UnityEngine;

public class MainGameManager
{
    GamePlayer gamePlayer;

    //게임을 직접적으로 실행시켜주는 GamePlayer를 찾는 함수
    //오직 메인 리듬게임에서만 실행시켜야 함
    public void FindGamePlayer()
    {
        gamePlayer = GameObject.Find("GamePlayer").GetComponent<GamePlayer>();
    }

    public void PlayGame()
    {
        gamePlayer.GameSystem();
    }

    public void PauseGame()
    {
        gamePlayer.pause = true;
    }

    public void ResumeGame()
    {
        gamePlayer.pause = false;
    }
}
