using UnityEngine;

public class Note : MonoBehaviour
{
    //노트 lane
    int lane;
    float lanePos = 0;
    //노트 길이
    int length;
    float height = 4;

    //노트 떨구는 함수, UniTask로 지속적으로 호출하는 방식
    //추후 너무 비효율적이라면 수정 예정
    public void drop(float speed)
    {
        //매 프레임마다 설정되는 높이
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
        //test용 이후 이 코드는 없어질 것임
        if (gameObject.transform.position.y < -2.6)
        {
            Destroy(gameObject);
        }
    }
    //길이 설정, 처음에 설정하고 건들지 않음
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
