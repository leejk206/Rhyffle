using UnityEngine;
using TMPro;

public class JudgementText : MonoBehaviour
{
    public TextMeshProUGUI judge;

    public void JudgeText(Define.JudgementType judgement)
    {
        switch (judgement) {
            case Define.JudgementType.Perfect:
                judge.color = Color.red;
                judge.text = "Perfect";
                judge.alpha = 0.5f;
                break;
            case Define.JudgementType.Great:
                judge.color = Color.yellow;
                judge.text = "Great";
                judge.alpha = 0.5f;
                break;
            case Define.JudgementType.Good:
                judge.color = Color.blue;
                judge.text = "Good";
                judge.alpha = 0.5f;
                break;
            case Define.JudgementType.Miss:
                judge.color = Color.gray;
                judge.text = "Miss";
                judge.alpha = 0.5f;
                break;
        }
    }

    private void Update()
    {
        judge.alpha -= Time.deltaTime;
    }
}
