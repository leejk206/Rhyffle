using UnityEngine;

public class Note : MonoBehaviour
{
    //�ܺο��� �� ��Ʈ�� ������ �� ��Ʈ
    public int noteTime;
    //��Ʈ lane
    public int line;
    float lanePos = 0;
    //��Ʈ ����
    public int length;
    float height = 10;

    //��Ʈ ������ �Լ�, UniTask�� ���������� ȣ���ϴ� ���
    //���� �ʹ� ��ȿ�����̶�� ���� ����

    //120bpm �⺻�ӵ� 4 ���� 2�� �ȿ� ������
    //�ӵ� ��� ����� (bpm)x(�����ӵ�)
    virtual public void drop(float speed)
    {
        //�� �����Ӹ��� �����Ǵ� ����
        height -= speed / 480 * 5 * Time.deltaTime;
        float cal = height * (-0.08f) + 1;
        if (height >= 0)
        {
            gameObject.transform.localScale = new Vector3(length * 3.2f * cal,3.2f * cal,1);
            gameObject.transform.localPosition = new Vector3(lanePos * cal, height - 2.25f,0);
        }
        else
        {
            gameObject.transform.localPosition = new Vector3(lanePos, height - 2.25f, -2);
        }
    }
    //���� ����, ó���� �����ϰ� �ǵ��� ����
    virtual public void set(int lane, int length)
    {
        height = 10;
        this.line = lane;
        this.length = length;
        lanePos = -10.5f + lane;
        //�ʱ� ��ġ ����
        gameObject.transform.localScale = new Vector3(length * 0.64f, 0.64f, 1);
        gameObject.transform.localPosition = new Vector3(lanePos * 0.2f, 7.75f ,2);
    }

    //���� �Ǿ��� �� ���־�������
    virtual public void touched()
    {

    }
}
