using UnityEngine;
using TMPro;
using TouchPhase = UnityEngine.TouchPhase;

public class NoteScreen : MonoBehaviour
{
    public GamePlayer gamePlayer;
    int[] touchIDs = new int[5];
    Vector2[] touchPos = new Vector2[5];
    float flickMinDis = 0.2f;
    float speedPx;
    float dpi;
    public TMP_Text text;
    // Update is called once per frame
    private void Start()
    {
        speedPx = 900f;
        dpi = Screen.dpi / 300f;
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
                            pass = true;
                            break;
                        }
                    }
                    if (!pass)
                    {
                        touchIDs[i] = touch.fingerId;
                        this.touchPos[i] = touch.position;
                    }
                }
                // 터치의 RayCast 사용하여 위치 확인
                Vector2 touchPos = touch.position;
                Ray ray = Camera.main.ScreenPointToRay(touchPos);
                bool rayHit = Physics.Raycast(ray, out RaycastHit hit);

                bool isOnBar = false;

                float deltaTime = touch.deltaTime;
                Vector2 deltaPos = touch.deltaPosition;

                float speed = deltaPos.magnitude / deltaTime;
                float angle = Mathf.Atan2(deltaPos.y, deltaPos.x) * Mathf.Rad2Deg;
                int targetBar = -1;
                // RayCast에 의한 transform으로 bar 위치 확인
                if (hit.transform.gameObject.tag == "Bar")
                {
                    targetBar = hit.transform.gameObject.GetComponent<Bar>().barNum;
                    if(hit.transform.gameObject.tag == "Bar")
                    {
                        isOnBar = true;
                    }
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            if (isOnBar)
                            {
                                gamePlayer.press[targetBar] = true;
                                gamePlayer.touchStart[targetBar] = touch.fingerId;
                                gamePlayer.slide[targetBar] = true;
                            }
                            break;
                        case TouchPhase.Moved:
                            if (isOnBar)
                            {
                                gamePlayer.intouch[targetBar] = true;
                                gamePlayer.slide[targetBar] = true;
                                if (speed * dpi > speedPx)
                                {
                                    if(angle > 30 && angle < 150)
                                    {
                                        text.text = "FlickedUP at " + gamePlayer.currentTime; 
                                        gamePlayer.flickUp[targetBar] = true;
                                    }else if (angle < -30 && angle > -150)
                                    {
                                        text.text = "FlickedDOWN at " + gamePlayer.currentTime;
                                        gamePlayer.flickDown[targetBar] = true;
                                    }
                                }
                            }
                            break;
                        case TouchPhase.Stationary:
                            if (isOnBar)
                            {
                                gamePlayer.intouch[targetBar] = true;
                            }
                            break;
                        case TouchPhase.Ended:
                            if (isOnBar)
                            {
                                gamePlayer.endtouch[targetBar] = true;
                                gamePlayer.touchEnd[targetBar] = touch.fingerId;
                            }
                            this.touchPos[i] = Vector2.zero;

                            break;
                    }
                }
                else
                {
                    this.touchPos[i]= Vector2.zero; 
                }
                
                // Touch의 이동 경로 확인 -->
                // y 값 변화가 반대로 바뀐다면 이동 경로 리셋
                // 그 외에는 그대로 이동

                // 

            }
        }
    }
}
