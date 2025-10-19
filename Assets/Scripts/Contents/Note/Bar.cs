using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

public class Bar : MonoBehaviour
{
    public int barNum;
    //���� GamePlayer�� �ٸ� Manager��  �̵�����
    //���� test����
    public GamePlayer gamePlayer;
    float beforeX, beforeY;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = touch.position;
            Ray ray = Camera.main.ScreenPointToRay(touchPos);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            gamePlayer.press[barNum] = true;
                            break;
                        case TouchPhase.Moved:
                        case TouchPhase.Stationary:
                            gamePlayer.intouch[barNum] = true;
                            break;
                        case TouchPhase.Ended:
                            gamePlayer.endtouch[barNum] = true;
                            break;
                    }
                }
            }
        }
    }
    
    #if UNITY_EDITOR
    private void OnMouseDown()
    {
        gamePlayer.press[barNum] = true;
        
    }
    private void OnMouseEnter()
    {
        if(Input.GetMouseButton(0)) gamePlayer.slide[barNum] = true;
    }
    private void OnMouseOver()
    {
        beforeX = Input.mousePosition.x;
        beforeY = Input.mousePosition.y;
        if (Input.GetMouseButton(0)) gamePlayer.intouch[barNum] = true;
        if (Input.GetMouseButtonUp(0)) gamePlayer.endtouch[barNum] = true;
    }
    private void OnMouseDrag()
    {
        gamePlayer.slide[barNum] = true;
        if (beforeY < Input.mousePosition.y) gamePlayer.flickUp[barNum] = true;
        else if (beforeY > Input.mousePosition.y) gamePlayer.flickDown[barNum] = true;
    }
    #endif
}
