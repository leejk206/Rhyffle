using System.Collections.Generic;
using UnityEngine;

public class TouchVisualizer : MonoBehaviour
{
    public GameObject touchCircle;

    // 터치 ID로 각 터치의 원 오브젝트를 관리
    private Dictionary<int, GameObject> activeCircles = new Dictionary<int, GameObject>();

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(touch.position);
            worldPos.z = 0;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // // 원 생성
                    // GameObject circle = Instantiate(touchCircle, worldPos, Quaternion.identity);
                    // activeCircles.Add(touch.fingerId, circle);
                    // break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    // 터치 중 -> 위치 업데이트
                    if (activeCircles.ContainsKey(touch.fingerId))
                    {
                        activeCircles[touch.fingerId].transform.position = worldPos;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    // 원 제거
                    if (activeCircles.ContainsKey(touch.fingerId))
                    {
                        Destroy(activeCircles[touch.fingerId]);
                        activeCircles.Remove(touch.fingerId);
                    }
                    break;
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            GameObject circle = Instantiate(touchCircle, pos, Quaternion.identity);
            activeCircles[0] = circle;
        }
        else if (Input.GetMouseButton(0))
        {
            if (activeCircles.ContainsKey(0))
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                activeCircles[0].transform.position = pos;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (activeCircles.ContainsKey(0))
            {
                Destroy(activeCircles[0]);
                activeCircles.Remove(0);
            }
        }
#endif
    }
}
