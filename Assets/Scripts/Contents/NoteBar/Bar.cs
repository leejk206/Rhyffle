using UnityEngine;

public class Bar : MonoBehaviour
{
    public int barNum;
    //���� GamePlayer�� �ٸ� Manager��  �̵�����
    //���� test����
    public NoteCreationTester tester;
    float beforePos;
    bool checkPos;

    private void OnMouseDown()
    {
        tester.touched[barNum] = true;
        tester.entered[barNum] = true;
    }
    private void OnMouseEnter()
    {
        tester.entered[barNum] = true;
    }

    private void OnMouseExit()
    {
        checkPos = false;
    }
    private void OnMouseDrag()
    {
        if (checkPos)
        {
            if(Input.mousePosition.y > beforePos)
                tester.flickUp[barNum] = true;
            if(Input.mousePosition.y < beforePos)
                tester.flickDown[barNum] = true;
        }
        checkPos = true;
        beforePos = Input.mousePosition.y;
    }
}
