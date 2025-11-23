using UnityEngine;
using System.Collections.Generic;

public class CardMechUI : MonoBehaviour
{
    public FilterUI filterUI;
    public CardInfoUI cardInfoUI;

    protected RectTransform rect;
    protected int cardPerWidth = 0;
    protected int cardPerHeight = 0;
    protected int cardPage = 0;

    protected GameObject[] boardArray = new GameObject[3];
    protected List<CardInfo> cards = new List<CardInfo>();

    // Later need to be erased when using server system;
    protected string searchListPath;

    // filters
    protected List<Define.CardRank> rank;
    protected List<Define.CardRarity> rarity;
    protected List<Define.CardSuit> suit;
    protected bool owned;
    protected bool bookmarked;
    
    public GameObject boardPrefab;
    public void Start()
    {
        searchListPath = Application.persistentDataPath + "/SearchList.json";
        rect = gameObject.GetComponent<RectTransform>();
        float UIWid = gameObject.transform.parent.gameObject.GetComponent<RectTransform>().rect.width;
        float UIHei = gameObject.transform.parent.gameObject.GetComponent<RectTransform>().rect.height;
        rect.sizeDelta = new Vector2(UIWid, UIHei);

        // Board Creation

        // 여기서 보드 크기 확인

        CreateBoards(0);
        CreateBoards(1);
        CreateBoards(2);
    }
    public virtual void CreateBoards(int pos) { }
    public virtual void NextCards() { }
    public virtual void PrevCards() { }

    public virtual void SetCards() { }

    public virtual void OpenFilter() { filterUI.gameObject.SetActive(true); }

    public virtual void OpenCardInfo(CardInfo cardInfo, CardBaseInfo cardBaseInfo) { 
        cardInfoUI.SetCardInfo(cardInfo, cardBaseInfo);
        cardInfoUI.gameObject.SetActive(true);
        cardInfoUI.ShowCardInfo();
    }
    public virtual void ApplyFilter(List<Define.CardRank> rank, List<Define.CardRarity> rarity, List<Define.CardSuit> suit, bool owned, bool bookmark) { }
}
