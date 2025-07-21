using UnityEngine;
using UnityEngine.InputSystem;

public class Bar : MonoBehaviour
{
    public int barNum;
    //추후 GamePlayer나 다른 Manager로  이동예정
    //판정 test용임
    public GamePlayer gamePlayer;
    float beforeX, beforeY;

    private void OnMouseDown()
    {
        gamePlayer.press[barNum] = true;
        
    }
    private void OnMouseEnter()
    {
        if(Input.GetMouseButton(0))
        gamePlayer.slide[barNum] = true;
    }
    private void OnMouseOver()
    {
        beforeX = Input.mousePosition.x;
        beforeY = Input.mousePosition.y;
        if (Input.GetMouseButton(0))
        {
            gamePlayer.intouch[barNum] = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            gamePlayer.endtouch[barNum] = true;
        }
    }
    private void OnMouseDrag()
    {
        gamePlayer.slide[barNum] = true;
        if (beforeY < Input.mousePosition.y)
        {
            gamePlayer.flickUp[barNum] = true;
        }
        else if (beforeY > Input.mousePosition.y) { 
            gamePlayer.flickDown[barNum] = true;
        }
    }
}
