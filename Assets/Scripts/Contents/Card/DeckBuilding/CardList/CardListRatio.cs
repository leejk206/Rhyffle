using UnityEngine;
using System.Collections;

public class CardListRatio : MonoBehaviour
{
    RectTransform rectTrans;
    Camera cam;
    public GameObject cardBoard;
    public GameObject UIBack;

    private void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        cam = GameObject.Find("MainCamera").GetComponent<Camera>();
        rectTrans.sizeDelta = new Vector2(cam.pixelWidth/100, cam.pixelHeight/100);
    }
}
