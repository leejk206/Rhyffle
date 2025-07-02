using UnityEngine;

public class Note : MonoBehaviour
{
    //외부에서 이 노트를 참조할 때 노트
    public int noteTime;
    //노트 lane
    public int line;
    float lanePos = 0;
    //노트 길이
    public int length;
    float height = 10;

    //노트 떨구는 함수, UniTask로 지속적으로 호출하는 방식
    //추후 너무 비효율적이라면 수정 예정

    //120bpm 기본속도 4 기준 2초 안에 떨어짐
    //속도 계산 방법은 (bpm)x(설정속도)
    virtual public void drop(float speed)
    {
        //매 프레임마다 설정되는 높이
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
    //길이 설정, 처음에 설정하고 건들지 않음
    virtual public void set(int lane, int length)
    {
        height = 10;
        this.line = lane;
        this.length = length;
        lanePos = -10.5f + lane;
        //초기 위치 설정
        gameObject.transform.localScale = new Vector3(length * 0.64f, 0.64f, 1);
        gameObject.transform.localPosition = new Vector3(lanePos * 0.2f, 7.75f ,2);
    }

    //판정 되었을 때 비주얼적으로
    virtual public void touched()
    {

    }
}
