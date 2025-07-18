using UnityEngine;

public class Bar : MonoBehaviour
{
    public int barNum;
    //���� GamePlayer�� �ٸ� Manager��  �̵�����
    //���� test����
    public NoteCreationTester tester;
    float beforePos;
    bool checkPos;

    public void TouchPress()
    {

    }

    public void TouchMove()
    {

    }
    public void TouchEnd()
    {

    }



    private void OnMouseDown()
    {

        tester.slide[barNum] = true;
        tester.touched[barNum] = true;
    }
    private void OnMouseEnter()
    {
        tester.slide[barNum] = true;
        tester.touched[barNum] = true;
    }

    private void OnMouseOver()
    {
        tester.touched[barNum] = true;
    }

    private void OnMouseExit()
    {
        tester.off[barNum] = true;
        checkPos = false;
    }
    private void OnMouseUp()
    {
        tester.off[barNum] = true;
    }
    private void OnMouseDrag()
    {
        tester.pressed[barNum] = true;
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
