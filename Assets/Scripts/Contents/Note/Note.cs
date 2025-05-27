using UnityEngine;

public class Note : MonoBehaviour
{
    //��Ʈ ����
    int length;
    float height = 4;

    //��Ʈ ������ �Լ�, UniTask�� ���������� ȣ���ϴ� ���
    //���� �ʹ� ��ȿ�����̶�� ���� ����
    public void drop(float speed)
    {
        height -= speed / 480;
        //gameObject.transform.localScale;
    }
    //���� ����, ó���� �����ϰ� �ǵ��� ����
    public void setLength(int length)
    {
        length = this.length;
        gameObject.transform.localScale = new Vector3(length * 0.5f, 0.5f, 1);
    }

    virtual public void touched()
    {

    }
}
