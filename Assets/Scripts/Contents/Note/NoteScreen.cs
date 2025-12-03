using UnityEngine;
using TouchPhase = UnityEngine.TouchPhase;

public class NoteScreen : MonoBehaviour
{
    public GamePlayer gamePlayer;
    int[] touchIDs = new int[5];
    float[] touchY = new float[5];


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++) {
                Touch touch = Input.GetTouch(i);
                // 기존에 있는 터치인지 확인
                if (touchIDs[i] != touch.fingerId)
                {
                    bool pass = false;
                    for(int j =0; i < 5; i++)
                    {
                        if (touchIDs[j] == touch.fingerId)
                        {
                            touchIDs[i] = touch.fingerId;
                            touchY[i] = touchY[j];
                            pass = true;
                            break;
                        }
                    }
                    if (!pass)
                    {
                        touchIDs[i] = touch.fingerId;
                        touchY[i] = 0;
                    }
                }

                // 터치의 RayCast 사용하여 위치 확인
                Vector2 touchPos = touch.position;
                Ray ray = Camera.main.ScreenPointToRay(touchPos);
                bool rayHit = Physics.Raycast(ray, out RaycastHit hit);
                int targetBar = -1;
                // RayCast에 의한 transform으로 bar 위치 확인
                if (hit.transform.gameObject.tag == "Bar")
                {
                    targetBar = hit.transform.gameObject.GetComponent<Bar>().barNum;
                }
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        gamePlayer.press[targetBar] = true;
                        gamePlayer.slide[targetBar] = true;
                        break;
                    case TouchPhase.Moved:
                        gamePlayer.slide[targetBar] = true;
                        if (touch.deltaPosition.y > 0)
                        {
                            if (touchY[i] > 0)
                            {
                                touchY[i] += touch.deltaPosition.y;
                            }
                            else
                            {
                                touchY[i] = touch.deltaPosition.y;
                            }
                        }else if(touch.deltaPosition.y < 0)
                        {
                            if (touchY[i] < 0)
                            {
                                touchY[i] += touch.deltaPosition.y;
                            }
                            else
                            {
                                touchY[i] = touch.deltaPosition.y;
                            }
                        }
                        else
                        {
                            touchY[i] = 0;
                        }
                        if (touchY[i] > 0.3)
                        {
                            gamePlayer.flickUp[targetBar] = true;
                        }
                        if(touchY[i] < -0.3)
                        {
                            gamePlayer.flickDown[targetBar] = true;
                        }
                        break;
                    case TouchPhase.Stationary:

                        break;
                    case TouchPhase.Ended:
                        gamePlayer.endtouch[targetBar] = true;
                        break;
                }
                // Touch의 이동 경로 확인 -->
                // y 값 변화가 반대로 바뀐다면 이동 경로 리셋
                // 그 외에는 그대로 이동

                // 

            }
        }
    }
}
