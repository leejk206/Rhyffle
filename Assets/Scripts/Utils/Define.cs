using UnityEngine;

public class Define
// ���� ���ǰ� �� ����. State ���� �̿��� ����.
{
    public enum Scene
    { 
        Unknown, // ����
        Lobby, // ����
        Game, // ����
        Loading, // �ε�
        LoadingGame, // ���� ���� �� �ε� (�� ����/���̵�/��� ����/ ���� ����)
        ScoreBoard, //���� ���ȭ��
    }

    public enum JudgeNum
    {
        No_hit, // ó�� ����
        Miss,
        Good,
        Great,
        Perfect
    }

}
