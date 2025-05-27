using UnityEngine;
using UnityEngine.UIElements;

public class BarReading : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch1 = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch1.position);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider != null && hit.collider.gameObject.tag == "Bar")
                {
                    //터치가 진행된 순간
                    if(touch1.phase == TouchPhase.Began)
                    {
                        hit.collider.gameObject.GetComponent<Bar>();
                    }
                    //터치에서 손 떼었을 때
                    else if(touch1.phase == TouchPhase.Ended)
                    {

                    }
                    //터치 위치가 옮겼을 때
                    else if(touch1.phase == TouchPhase.Moved)
                    {

                    }
                }
            }

            if(Input.touchCount > 1)
            {

            }
        }
    }
}
