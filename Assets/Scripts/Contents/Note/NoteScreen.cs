using UnityEngine;
using TouchPhase = UnityEngine.TouchPhase;

public class NoteScreen : MonoBehaviour
{
    int[] touchIDs = new int[5];
    


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++) {
                Touch touch = Input.GetTouch(i);
                if (touchIDs[i] != touch.fingerId)
                {
                    bool pass = false;
                    for(int j =0; i < 5; i++)
                    {
                        if (touchIDs[j] == touch.fingerId)
                        {
                            touchIDs[i] = touch.fingerId;
                            pass = true;
                        }
                    }
                }
            }
        }
    }
}
