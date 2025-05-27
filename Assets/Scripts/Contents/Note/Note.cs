using UnityEngine;

public class Note : MonoBehaviour
{
    //노트 길이
    int length;
    float height = 4;

    //노트 떨구는 함수, UniTask로 지속적으로 호출하는 방식
    //추후 너무 비효율적이라면 수정 예정
    public void drop(float speed)
    {
        height -= speed / 480;
        //gameObject.transform.localScale;
    }
    //길이 설정, 처음에 설정하고 건들지 않음
    public void setLength(int length)
    {
        length = this.length;
        gameObject.transform.localScale = new Vector3(length * 0.5f, 0.5f, 1);
    }

    virtual public void touched()
    {

    }
}
