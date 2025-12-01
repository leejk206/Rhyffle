using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

using TMPro;

public class Bar : MonoBehaviour
{
    public int barNum;
    //���� GamePlayer�� �ٸ� Manager��  �̵�����
    //���� test����
    public GamePlayer gamePlayer;
    float beforeX, beforeY;

    public TMP_Text text;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                
                    Touch touch = Input.GetTouch(i);
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
                                    text.text = "Began" + barNum + " at " + gamePlayer.currentTime;
                                    gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                                    break;
                                case TouchPhase.Moved:
                                    break;
                                case TouchPhase.Stationary:
                                    gamePlayer.intouch[barNum] = true;
                                    break;
                                case TouchPhase.Ended:
                                    gamePlayer.endtouch[barNum] = true;
                                    gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                                    break;
                            }
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
