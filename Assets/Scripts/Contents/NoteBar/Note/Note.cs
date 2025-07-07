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

    Vector3 scaleVector;
    Vector3 posVector;

    //��Ʈ ������ �Լ�, UniTask�� ���������� ȣ���ϴ� ���
    //���� �ʹ� ��ȿ�����̶�� ���� ����

    //120bpm �⺻�ӵ� 4 ���� 2�� �ȿ� ������
    //�ӵ� ��� ����� (bpm)x(�����ӵ�)
    virtual public void drop(float speed)
    {
        //�� �����Ӹ��� �����Ǵ� ����
        height -= speed / 480 * 5 * Time.deltaTime;
        float cal = height * (-0.08f) + 1;
        if (height >= 0 && height <=10)
        {
            scaleVector.x = length * 3.2f * cal;
            scaleVector.y = 3.2f * cal;
            posVector.x = lanePos * cal;
            posVector.y = height - 2.25f;
            gameObject.transform.localScale = scaleVector;
            gameObject.transform.localPosition = posVector;
        }
        else if(height < 0)
        {
            posVector.x = lanePos;
            posVector.y = height - 2.25f;
            posVector.z = -2;
            gameObject.transform.localPosition = posVector;
        }
        else
        {

        }
    }
    //���� ����, ó���� �����ϰ� �ǵ��� ����
    virtual public void Set(int lane, int length)
    {
        height = 10;
        this.line = lane;
        this.length = length;
        lanePos = -10.5f + lane;
        //�ʱ� ��ġ ����
        scaleVector = new Vector3(length * 0.64f, 0.64f, 1);
        gameObject.transform.localScale = scaleVector;
        posVector = new Vector3(lanePos * 0.2f, 7.75f, 2);
        gameObject.transform.localPosition = posVector;
    }
    virtual public void Set(int lane, int length, int height)
    {
        this.height = height;
        this.line = lane;
        this.length = length;
        lanePos = -10.5f + lane;
        //�ʱ� ��ġ ����
        scaleVector = new Vector3(length * 0.64f, 0.64f, 1);
        gameObject.transform.localScale = scaleVector;
        posVector = new Vector3(lanePos * 0.2f, height - 2.25f, 2);
        gameObject.transform.localPosition = posVector;
    }
}
