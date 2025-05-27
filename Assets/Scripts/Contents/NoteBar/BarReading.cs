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
                    //��ġ�� ����� ����
                    if(touch1.phase == TouchPhase.Began)
                    {
                        hit.collider.gameObject.GetComponent<Bar>();
                    }
                    //��ġ���� �� ������ ��
                    else if(touch1.phase == TouchPhase.Ended)
                    {

                    }
                    //��ġ ��ġ�� �Ű��� ��
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
