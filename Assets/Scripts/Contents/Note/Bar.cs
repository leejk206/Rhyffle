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
    
    // touch flags
    private Vector2 previousTouchPos;
    private bool isTouching = false;

    void Update()
    {
        HandleTouch();
    }
    
#if UNITY_EDITOR
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
#endif
    
    void HandleTouch()
    {
        if (Input.touchCount == 0) return;

        foreach (Touch touch in Input.touches)
        {
            Vector2 touchPos = touch.position;
            Ray ray = Camera.main.ScreenPointToRay(touchPos);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        gamePlayer.press[barNum] = true;
                        previousTouchPos = touchPos;
                        isTouching = true;
                        break;

                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        gamePlayer.intouch[barNum] = true;
                        gamePlayer.slide[barNum] = true;
                        
                        gamePlayer.slide[barNum] = true;
                        if (beforeY < Input.mousePosition.y)
                        {
                            gamePlayer.flickUp[barNum] = true;
                        }
                        else if (beforeY > Input.mousePosition.y) { 
                            gamePlayer.flickDown[barNum] = true;
                        }

                        previousTouchPos = touchPos;
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        gamePlayer.endtouch[barNum] = true;
                        isTouching = false;
                        break;
                }
            }
        }
    }
}
