using UnityEngine;

public class FilterUI : MonoBehaviour
{
    RectTransform rect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        float UIWid = gameObject.transform.parent.gameObject.GetComponent<RectTransform>().rect.width / 10 * 9;
        float UIHei = gameObject.transform.parent.gameObject.GetComponent<RectTransform>().rect.height;
        rect.sizeDelta = new Vector2(UIWid, UIHei);
        gameObject.SetActive(false);
    }

}
