using UnityEngine;

public class Note : MonoBehaviour
{
    //외부에서 이 노트를 참조할 때 노트
    public int judge;
    //노트 lane
    public int line;
    float lanePos = 0;
    //노트 길이
    public int length;
    float height = 10;

    Vector3 scaleVector;
    Vector3 posVector;

    //노트 떨구는 함수, UniTask로 지속적으로 호출하는 방식
    //추후 너무 비효율적이라면 수정 예정

    //120bpm 기본속도 4 기준 2초 안에 떨어짐
    //속도 계산 방법은 (bpm)x(설정속도)
    
    //판정 위치 저장
    virtual public void SetJudge(int judge)
    {
        this.judge = judge;
    }
    
    virtual public void Drop(float speed)
    {
        //매 프레임마다 설정되는 높이
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
            posVector.y = height - 2.25f;
            gameObject.transform.localPosition = posVector;
        }
    }
    //길이 설정, 처음에 설정하고 건들지 않음
    virtual public void Set(int lane, int length)
    {
        height = 10;
        this.line = lane;
        this.length = length;
        lanePos = -10.5f + lane;
        //초기 위치 설정
        scaleVector = new Vector3(length * 0.64f, 0.64f, 1);
        gameObject.transform.localScale = scaleVector;
        posVector = new Vector3(lanePos * 0.2f, 7.75f, 2);
        gameObject.transform.localPosition = posVector;
    }
    virtual public void Set(int lane, int length, float height)
    {
        this.height = height;
        this.line = lane;
        this.length = length;
        lanePos = -10.5f + lane;
        //초기 위치 설정
        scaleVector = new Vector3(length * 0.64f, 0.64f, 1);
        gameObject.transform.localScale = scaleVector;
        posVector = new Vector3(lanePos * 0.2f, height - 2.25f, 2);
        gameObject.transform.localPosition = posVector;
    }

    // return 값 Define의 JudgeNum에 따른 int 값
    // 이 함수는 판정을 통해서 값을 내보냄
    // bpm에 따라서 판정 계산 달라짐 (속도가 빠르면 curTime과 judge와의 판정이 커져야 함)
    // bpm/600은 0.1sec라고 보면 된다
    // 
    // checkType 0: MissCheck, 1: press, 2: slide, 3: intouch, 4: endtouch, 5: flickUp, 6: flickDown
    // GamePlayer의 TouchBoolean 순서 + 0 --> MissCheck
    //
    virtual public int ReadJudge(int lane, int bpm, int checkType, float curTime)
    {
        int result;

        if (checkType == 0) { 
            if(curTime > (float)bpm/600 * 2f * 16 + judge)
            {
                result = 1;
            }
            else
            {
                result = 0;
            }

            if (result != 0)
            {
                Managers.Effect.EffectOnNoteTrigger.RemoveAll(effect => effect.shouldBeRemoved == true); // 드로우 시 실행되는 다른 효과들의 리스트 정리
                foreach (var effect in Managers.Effect.EffectOnNoteTrigger) { effect.OnNoteTrigger(); }
            }

            return result;
        }
        if (lane >= line && lane <= line +length)
        {
            if (curTime < judge - (float)bpm / 600 * 3f * 16)
            {
                result = 0;
            }
            else if (curTime < judge - (float)bpm / 600 * 2f * 16)
            {
                result = 1;
            }
            else if (curTime < judge - (float)bpm / 600 * 1.5f * 16)
            {
                result = 2;
            }
            else if (curTime < judge - (float)bpm / 600 * 16)
            {
                result = 3;
            }else if(curTime < judge + (float)bpm / 600 * 16)
            {
                result = 4;
            }else if(curTime < judge + (float)bpm / 600 * 1.5f * 16)
            {
                result = 3;
            }else if(curTime < judge + (float)bpm / 600 * 2f * 16)
            {
                result = 2;
            }
            else
            {
                result = 1;
            }
        }
        else
        {
            result = 0;
        }
        if (result != 0)
        {
            Managers.Effect.EffectOnNoteTrigger.RemoveAll(effect => effect.shouldBeRemoved == true); // 드로우 시 실행되는 다른 효과들의 리스트 정리
            foreach (var effect in Managers.Effect.EffectOnNoteTrigger) { effect.OnNoteTrigger(); }
        }

        return result;
    }
}
