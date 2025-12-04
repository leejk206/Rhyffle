using UnityEngine;
using TMPro;
using TouchPhase = UnityEngine.TouchPhase;

public class NoteScreen : MonoBehaviour
{
    public GamePlayer gamePlayer;
    int[] touchIDs = new int[5];
    int[] lastLane = new int[5];
    float[] touchY = new float[5];
    float flickMinDis = 0.2f;
    public TMP_Text text;
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            
            for (int i = 0; i < Input.touchCount && i < 5; i++) {
                Touch touch = Input.GetTouch(i);
                // 기존에 있는 터치인지 확인
                if (touchIDs[i] != touch.fingerId)
                {
                    bool pass = false;
                    for(int j =0; j < 5; j++)
                    {
                        if (touchIDs[j] == touch.fingerId)
                        {
                            touchIDs[i] = touch.fingerId;
                            touchY[i] = touchY[j];
                            lastLane[i] = lastLane[j];
                            pass = true;
                            break;
                        }
                    }
                    if (!pass)
                    {
                        touchIDs[i] = touch.fingerId;
                        touchY[i] = 0;
                        lastLane[i] = -1;
                    }
                }
                if (Input.touchCount > 0)
                {
                    text.text = touchY[0].ToString();
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

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            gamePlayer.press[targetBar] = true;
                            gamePlayer.slide[targetBar] = true;
                            lastLane[i] = targetBar;
                            break;
                        case TouchPhase.Moved:
                            gamePlayer.intouch[targetBar] = true;
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
                            }
                            else if (touch.deltaPosition.y < 0)
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
                            if (touchY[i] > flickMinDis)
                            {
                                gamePlayer.flickUp[targetBar] = true;
                            }
                            if (touchY[i] < -flickMinDis)
                            {
                                gamePlayer.flickDown[targetBar] = true;
                            }
                            if (lastLane[i] != targetBar)
                            {
                                lastLane[i] = targetBar;
                                gamePlayer.slide[targetBar] = true;
                            }

                            break;
                        case TouchPhase.Stationary:
                            gamePlayer.intouch[targetBar] = true;
                            touchY[i] = 0;
                            break;
                        case TouchPhase.Ended:
                            gamePlayer.endtouch[targetBar] = true;
                            touchY[i] = 0;
                            lastLane[i] = -1;
                            break;
                    }
                }
                else
                {
                    touchY[i] = 0;
                    lastLane[i] = -1;
                }
                
                // Touch의 이동 경로 확인 -->
                // y 값 변화가 반대로 바뀐다면 이동 경로 리셋
                // 그 외에는 그대로 이동

                // 

            }
        }
    }
}
