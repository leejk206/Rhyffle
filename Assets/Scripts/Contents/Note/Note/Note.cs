using UnityEngine;
using static Define;

public class Note : MonoBehaviour
{
    //�ܺο��� �� ��Ʈ�� ������ �� ��Ʈ
    public float judge;
    //��Ʈ lane
    public int line;
    float lanePos = 0;
    //��Ʈ ����
    public int length;
    public float height = 10;

    public int endFingerID = -1;
    public int pressFingerID = -1;
    public int fingerID = -1;

    Vector3 scaleVector;
    Vector3 posVector;


    //��Ʈ ������ �Լ�, UniTask�� ���������� ȣ���ϴ� ���
    //���� �ʹ� ��ȿ�����̶�� ���� ����

    //120bpm �⺻�ӵ� 4 ���� 2�� �ȿ� ������
    //�ӵ� ��� ����� (bpm)x(�����ӵ�)
    
    //���� ��ġ ����
    virtual public void SetJudge(int judge)
    {
        this.judge = judge * 6.4f;
    }

    virtual public void Drop(float speed)
    {
        //�� �����Ӹ��� �����Ǵ� ����
        height -= speed / 480 * 5 * Time.deltaTime;
        float cal = height * (-0.08f) + 1;
        if (height >= 0 && height <=10)
        {
            scaleVector.x = length * 0.8f * cal;
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
            posVector.y = 7.75f;
            gameObject.transform.localPosition = posVector;
        }
    }
    //���� ����, ó���� �����ϰ� �ǵ��� ����
    virtual public void Set(int lane, int length)
    {
        endFingerID = -1;
        fingerID = -1;
        height = 10;
        this.line = lane;
        this.length = length;
        lanePos = -10.5f + lane;
        //�ʱ� ��ġ ����
        scaleVector = new Vector3(length * 0.16f, 0.64f, 1);
        gameObject.transform.localScale = scaleVector;
        posVector = new Vector3(lanePos * 0.2f, 7.75f, 2);
        gameObject.transform.localPosition = posVector;
    }
    virtual public void Set(int lane, int length, float height)
    {
        endFingerID = -1;
        fingerID = -1;
        this.height = height;
        this.line = lane;
        this.length = length;
        lanePos = -10.5f + lane;
        //�ʱ� ��ġ ����
        scaleVector = new Vector3(length * 0.16f, 0.64f, 1);
        gameObject.transform.localScale = scaleVector;
        posVector = new Vector3(lanePos * 0.2f, height - 2.25f, 2);
        gameObject.transform.localPosition = posVector;
    }

    // return �� Define�� JudgeNum�� ���� int ��
    // �� �Լ��� ������ ���ؼ� ���� ������
    // bpm�� ���� ���� ��� �޶��� (�ӵ��� ������ curTime�� judge���� ������ Ŀ���� ��)
    // bpm/600�� 0.1sec��� ���� �ȴ�
    // 
    // checkType 0: MissCheck, 1: press, 2: slide, 3: intouch, 4: endtouch, 5: flickUp, 6: flickDown 100: Miss
    // GamePlayer�� TouchBoolean ���� + 0 --> MissCheck
    //
    virtual public JudgementType ReadJudge(int lane, float bpm, int checkType, float curTime)
    {
        JudgementType result;
        if (checkType == 0) { 
            if( lane >= line && lane <= line + length)
            {
                if (curTime > (float)bpm / 600 * 2f * 16 + judge)
                {
                    result = JudgementType.Miss;
                }
                else
                {
                    result = JudgementType.Checked;
                }
            }
            else
            {
                result = JudgementType.NotChecked;
            }

            return result;
        }
        if(checkType == 100)
        {
            return JudgementType.Miss;
        }
        if (lane >= line && lane <= line +length)
        {
            if (curTime < judge - (float)bpm / 600 * 3f * 16)
            {
                result = JudgementType.Checked;
            }
            else if (curTime < judge - (float)bpm / 600 * 2f * 16)
            {
                result = JudgementType.Miss;
            }
            else if (curTime < judge - (float)bpm / 600 * 1.5f * 16)
            {
                result = JudgementType.Good;
            }
            else if (curTime < judge - (float)bpm / 600 * 16)
            {
                result = JudgementType.Great;
            }else if(curTime < judge + (float)bpm / 600 * 16)
            {
                result = JudgementType.Perfect;
            }else if(curTime < judge + (float)bpm / 600 * 1.5f * 16)
            {
                result = JudgementType.Great;
            }else if(curTime < judge + (float)bpm / 600 * 2f * 16)
            {
                result = JudgementType.Good;
            }
            else
            {
                result = JudgementType.Miss;
            }
        }
        else
        {
            result = JudgementType.NotChecked;
        }

        return result;
    }
}
