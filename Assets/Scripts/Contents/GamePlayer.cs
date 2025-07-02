using UnityEngine;
using Cysharp.Threading.Tasks;

public class GamePlayer : MonoBehaviour
{
    //�׽�Ʈ�ϱ� ���ϰ� public���� ������ ���� ���� ����
    
    //120 ���� �⺻ ����ӵ��� 3��
    //���� �����, �� ��ü���� ��Ʈ �ӵ�
    public float songNoteSpeed;
    //�÷��̾ �����ϴ� ��Ʈ �ӵ�
    //�⺻ ����ӵ� 5
    public float playerNoteSpeed;

    //ä�� ����
    public int songLength;
    //���� ��ġ
    public int currentLength;
    //ä�� ���� ������


    //�Ͻ�����
    public bool pause;
    //�÷��� ���
    public bool cardMode;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    

    //��ǻ� �������� ������� ó���� �̰����� �Ѵٰ� ���� �ȴ�.
    //GameManager�� �� ��ũ��Ʈ�� ã�Ƽ� Ȱ��ȭ��Ű�� ������� �����Ѵ�.
    public async UniTask GameSystem()
    {
        //���� ��Ʈ��
        //�� ������ �����ؼ� �ٷ� ������ ����

        //���� ����
        //�� ����(ä�� ����)��ŭ ��Ʈ�� ������ ���� ����
        while (currentLength < songLength)
        {
            //���� pause��� ����
            if (!pause)
            {
                //pause�� countdown�� ��� ���� ������ ����
                await UniTask.WaitUntil(()=> pause == false);
            }
            else
            {
                //songNoteSpeed�� playerNoteSpeed�� �̿��ؼ� ������ ��Ʈ�� �������� �ӵ��� parameter�� ������ ��Ʈ�� ���� -->

            }
        }

        //���� ��
        //���� ���� �ܰ迡�� �� ����(ä�� ����)�� ������ �������� ���
        //�÷��̾� ������ ���� �߰����� UI
    }

}
