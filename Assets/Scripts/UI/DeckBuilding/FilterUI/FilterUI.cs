using NUnit.Framework;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using System.Collections.Generic;

public class FilterUI : MonoBehaviour
{
    RectTransform rect;

    #region filters
    public bool[] rank;
    public bool[] rarity;
    public bool[] suit;
    public bool owned = false;
    public bool bookmark = false;
    #endregion
    List<Define.CardRank> cardRank = new List<Define.CardRank>();
    List<Define.CardRarity> cardRarity = new List<Define.CardRarity>();
    List<Define.CardSuit> cardSuit = new List<Define.CardSuit>();


    // We'll erase this later when adding server connection on filter
    public HardCodeFilter hardCodeFilter;

    public CardMechUI cardMechUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rank = new bool[14];
        rarity = new bool[4];
        suit = new bool[4];
        rect = gameObject.GetComponent<RectTransform>();
        float UIWid = gameObject.transform.parent.gameObject.GetComponent<RectTransform>().rect.width / 10 * 9;
        float UIHei = gameObject.transform.parent.gameObject.GetComponent<RectTransform>().rect.height;
        rect.sizeDelta = new Vector2(UIWid, UIHei);
        gameObject.SetActive(false);
    }



    // Later we need to rebuild this to Online Connecting version
    public void ApplyFilter()
    {
        // 온라인이면 필터만 전달

        // 하드 코딩이면 hardCodeFilter를 이용해서 데이터를 변환 후 적용 전달
        cardRank.Clear();
        cardRarity.Clear();
        cardSuit.Clear();
        for (int i = 0; i < 4; i++) {
            if (rarity[i])
            {
                cardRarity.Add((Define.CardRarity)i);
            }
            if (suit[i])
            {
                cardSuit.Add((Define.CardSuit)i);
            }
        }
        for(int i = 0; i< 14; i++)
        {
            if (rank[i])
            {
                cardRank.Add((Define.CardRank)i);
            }
        }
        hardCodeFilter.ApplyFilter(cardRank, cardRarity, cardSuit, owned, bookmark);
        cardMechUI.SetCards();
        gameObject.SetActive(false);
    }
}
