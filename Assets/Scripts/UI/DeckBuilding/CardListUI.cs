using Unity.VisualScripting;
using UnityEngine;

public class CardListUI : MonoBehaviour
{
    RectTransform rect;

    private void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        float UIWid = gameObject.transform.parent.gameObject.GetComponent<RectTransform>().rect.width;
        float UIHei = gameObject.transform.parent.gameObject.GetComponent<RectTransform>().rect.height;
        rect.sizeDelta = new Vector2 (UIWid, UIHei);
    }
    
}
