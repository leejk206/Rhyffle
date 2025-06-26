using UnityEngine;

public class Note : MonoBehaviour
{
    //��Ʈ lane
    int lane;
    float lanePos = 0;
    //��Ʈ ����
    int length;
    float height = 4;

    //��Ʈ ������ �Լ�, UniTask�� ���������� ȣ���ϴ� ���
    //���� �ʹ� ��ȿ�����̶�� ���� ����
    public void drop(float speed)
    {
        //�� �����Ӹ��� �����Ǵ� ����
        height -= speed / 480 * Time.deltaTime;
        if (height >= 0)
        {
            gameObject.transform.localScale = new Vector3((2.0f - 3.0f * height / 8.0f) * length, 2.0f - (3.0f * height) / 8.0f, -2);
            gameObject.transform.localPosition = new Vector3(lanePos * (1 - (3 * height / 16)), 2f * height - 2.25f, -2);
        }
        else
        {
            gameObject.transform.localPosition = new Vector3(lanePos, 2f * height - 2.25f, -2);
        }
        //test�� ���� �� �ڵ�� ������ ����
        if (gameObject.transform.position.y < -2.6)
        {
            Destroy(gameObject);
        }
    }
    //���� ����, ó���� �����ϰ� �ǵ��� ����
    public void set(int lane, int length)
    {
        height = 4;
        this.lane = lane;
        this.length = length;
        lanePos = -6.725f + 0.61f * lane;
        gameObject.transform.localScale = new Vector3(length * 0.5f, 0.5f, 1);
        gameObject.transform.localPosition = new Vector3(lanePos * 0.25f, 5.8f,2);
    }

    virtual public void touched()
    {

    }
}
