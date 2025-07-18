using UnityEngine;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;

public class GamePlayer : MonoBehaviour
{

    public GameObject noteCreator;



    // ���� ���� ����
    public bool play = true;
    // ���� ���۽� ������ (-144��° ���ڿ��� ����, �� 144���� ���ĸ� 0�� �ش��ϴ� ���ڸ� ó���ؾ� �ϴ� Ÿ�̹�)
    float currentTime = 144;

    // ��Ʈ�� ����, Json���� Serialize�Ǿ� ����� Info Class���� �迭�� ���� (NoteType.cs Ȯ��)
    #region noteInfos
    BasicNoteInfo[] basicNotes;
    SlideNoteInfo[] slideNotes;
    FlickNoteInfo[] flickNotes;
    HoldNoteInfo[] holdNotes;
    // holdNote�� �������� holdBody���� ���� ���� List
    List<List<HoldNoteInfo>> holdBodyList = new List<List<HoldNoteInfo>>();
    #endregion

    // noteInfo���� ������ ��Ʈ���� position ������ �̸� int �迭�� ����
    // holdNote�� ��� ó�� �����ϴ� ��Ʈ�� �������� ����
    #region spawnPositions
    int[] basicNoteTiming;
    int[] slideNoteTiming;
    int[] flickNoteTiming;
    int[] holdNoteTiming;
    #endregion

    // ��ġ ���� �����Ͽ� ���� ����
    #region TouchBoolean
    // ��ġ����
    public bool[] press = new bool[21];
    // �����̵�
    public bool[] slide = new bool[21];
    // ��ġ��
    public bool[] intouch = new bool[21];
    // ��ġ����
    public bool[] endtouch = new bool[21];
    // �ø���
    public bool[] flickUp = new bool[21];
    // �ø��ٿ�
    public bool[] flickDown = new bool[21];
    #endregion

    // ��Ʈ ������ �ӵ� ���� ���� ����
    #region noteSpeed
    // ��Ʈ �ӵ� (���� ��Ϳ� ��ȭ��)
    int bpm = 120;
    // �÷��̾� ������
    float offSet = 0;
    // �÷��̾� ��Ʈ �������� �ӵ�
    float currentSpeed = 4;
    #endregion

    // �� ���� Note���� ���� GameObject List
    #region inGameNotes
    List<GameObject> inGameBasic = new List<GameObject>();
    List<GameObject> inGameSlide = new List<GameObject>();
    List<GameObject> inGameFlick = new List<GameObject>();
    // Hold�� Ư���ϰ� HoldNoteBody�� ����
    List<GameObject> inGameHold = new List<GameObject>();
    #endregion

    public void SetUp()
    {
        // jsonManager Ȥ�� ������ ��ũ��Ʈ�� �̿��ؼ� noteInfo�� ����

        // �� �κп��� ��Ʈ�� �������� ������ 'position' �������� ������ �ʿ��� (�̱���)
        // �׷��� �ʴ´ٸ� ���� ���� ��Ʈ���� ����

        // holdNoteBody ���� �̸� count���� �з��Ͽ� ����
        #region holdNoteBodyInfoBind
        // holdBodyCount�� ����� Ȧ�� ��Ʈ���� �� ����
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

        // ������ ��Ʈ���� �����ؾ� �ϴ� �������� ����(�� �������� �̿��ؼ� ����߸��� Ÿ�̹� ����)
        // ������ ��Ʈ�� �������� ������ position�������� �������� �ʴ´ٸ� �̰����� ���ο� ����ü Ȥ�� Class�� ���� �����ص� ��
        // �ش� Class���� ��Ʈ�� position�̶� �ش� ��Ʈ�� noteInfo�� ���°�� �ִ��� üũ�ϴ� ������ ������... �������� �����...?
        // �׷��� �׳� ������ �����ϴ� ���� �ܼ��� ������ ��õ��


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
