using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfoUI : MonoBehaviour
{
    // erase public later. It is for test
    public DeckCardInfo cardInfo;
    public CardBaseInfo cardBaseInfo;
    RectTransform rect;

    public TMP_Text cardName;
    public TMP_Text cardEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        float UIWid = gameObject.transform.parent.gameObject.GetComponent<RectTransform>().rect.width / 10 * 9;
        float UIHei = gameObject.transform.parent.gameObject.GetComponent<RectTransform>().rect.height;
        gameObject.SetActive(false);
    }

    public void SetCardInfo(DeckCardInfo cardInfo, CardBaseInfo cardBaseInfo)
    {
        this.cardInfo = cardInfo;
        this.cardBaseInfo = cardBaseInfo;
    }

    // Apply Information here
    public void ShowCardInfo()
    {
        cardName.text = cardBaseInfo.card_name;
        cardEffect.text = (cardBaseInfo.card_suit + "_" + cardBaseInfo.card_rank + "_" +cardBaseInfo.card_rarity);
    }

    public void CloseCardInfo()
    {
        gameObject.SetActive(false);
    }
}
