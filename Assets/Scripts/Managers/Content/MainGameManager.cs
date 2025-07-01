using UnityEngine;

public class MainGameManager
{
    GamePlayer gamePlayer;

    //������ ���������� ��������ִ� GamePlayer�� ã�� �Լ�
    //���� ���� ������ӿ����� ������Ѿ� ��
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
