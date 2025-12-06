using UnityEngine;
using TMPro;
using TouchPhase = UnityEngine.TouchPhase;

public class NoteScreen : MonoBehaviour
{
    public GamePlayer gamePlayer;
    int[] touchIDs = new int[5];
    int[] lastLane = new int[5];
    Vector2[] touchPos = new Vector2[5];
    float flickMinDis = 0.2f;
    float distancePx;
    public TMP_Text text;
    // Update is called once per frame
    private void Start()
    {
        touchPos = new Vector2[5];
        distancePx = Screen.dpi;
        if(distancePx < 100)
        {
            distancePx = 300;
        }

        distancePx = distancePx * 0.393701f * 0.1f;
    }

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
                            this.touchPos[i] = this.touchPos[j];
                            lastLane[i] = lastLane[j];
                            pass = true;
                            break;
                        }
                    }
                    if (!pass)
                    {
                        touchIDs[i] = touch.fingerId;
                        this.touchPos[i] = touch.position;
                        lastLane[i] = -1;
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

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            gamePlayer.press[targetBar] = true;
                            lastLane[i] = targetBar;
                            break;
                        case TouchPhase.Moved:
                            gamePlayer.intouch[targetBar] = true;
                            Vector2 direction = (touch.position - this.touchPos[i]).normalized;
                            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                            if (touch.deltaPosition.y > 0)
                            {
                                if (this.touchPos[i].y < touch.position.y)
                                {
                                    if (angle > 30 && angle < 150)
                                    {
                                        // Flick 판정 확인
                                        if (touch.position.y - this.touchPos[i].y > distancePx)
                                        {
                                            gamePlayer.flickUp[targetBar] = true;
                                            this.touchPos[i] = touch.position;
                                            text.text = "Flicked Up at " + gamePlayer.currentTime;
                                        }
                                    }
                                    else
                                    {
                                        this.touchPos[i] = touch.position;
                                    }
                                    // 아닐경우 그냥 + 
                                }
                                else
                                {
                                    this.touchPos[i] = touch.position;
                                }
                            }
                            else if (touch.deltaPosition.y < 0)
                            {
                                if (this.touchPos[i].y > touch.position.y)
                                {
                                    if (angle > -150 && angle < -30)
                                    {
                                        if (this.touchPos[i].y - touch.position.y > distancePx)
                                        {
                                            gamePlayer.flickDown[targetBar] = true;
                                            this.touchPos[i] = touch.position;
                                            text.text = "Flicked Down at " + gamePlayer.currentTime; 
                                        }
                                    }
                                    else
                                    {
                                        this.touchPos[i] = touch.position;
                                    }
                                }
                                else
                                {
                                    this.touchPos[i] = touch.deltaPosition;
                                }
                            }
                            else
                            {
                                this.touchPos[i] = touch.position;
                            }
                            
                            if (lastLane[i] != targetBar)
                            {
                                lastLane[i] = targetBar;
                                gamePlayer.slide[targetBar] = true;
                            }

                            break;
                        case TouchPhase.Stationary:
                            gamePlayer.intouch[targetBar] = true;
                            this.touchPos[i] = touch.position;
                            break;
                        case TouchPhase.Ended:
                            gamePlayer.endtouch[targetBar] = true;
                            this.touchPos[i] = Vector2.zero;
                            lastLane[i] = -1;
                            break;
                    }
                }
                else
                {
                    this.touchPos[i]= Vector2.zero; 
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
