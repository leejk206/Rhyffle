using NUnit.Framework;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using System.Collections.Generic;

public class FilterUI : MonoBehaviour
{
    RectTransform rect;
    #region filterButtons
    public RectTransform[] rankRect;
    public RectTransform[] rarityRect;
    public RectTransform[] suitRect;
    public RectTransform ownedRect;
    public RectTransform bookmarkRect;
    public RectTransform applyRect;
    public RectTransform canvas;
    #endregion

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
    float UIWid;
    float UIHei;

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
        UIWid = canvas.rect.width / 10 * 9;
        UIHei = canvas.rect.height;
        rect.sizeDelta = new Vector2(UIWid, UIHei);
        Debug.Log(UIWid +"_" + UIHei);
        SetFilterPos();
        gameObject.SetActive(false);

        //이거 이외에도 다른 필터들 위치 정렬하는 함수가 필요할 듯

    }

    public void SetFilterPos()
    {
        float wid, hei;
        //suit
        wid = UIWid / 30;
        hei = UIHei / 30;
        for(int i = 0; i< 4; i++)
        {
            suitRect[i].sizeDelta = new Vector2(wid, hei);
            suitRect[i].anchoredPosition = new Vector2((UIWid / 30) * (5 * (i+1)), - (UIHei / 30 * 5));
        }

        // rank
        wid = UIWid / 90;
        hei = UIHei / 30;
        for(int i = 0; i< 14; i++)
        {
            rankRect[i].sizeDelta = new Vector2(wid, hei);
            rankRect[i].anchoredPosition = new Vector2((UIWid / 90) * (6 * (i + 1)), -UIHei / 30 * 10);
        }
        // rarity
        wid = UIWid / 30;
        hei = UIHei / 30;
        for (int i = 0; i < 4; i++)
        {
            rarityRect[i].sizeDelta = new Vector2(wid, hei);
            rarityRect[i].anchoredPosition = new Vector2((UIWid / 30) * (5 * (i + 1)), -UIHei / 30 * 15);
        }

        // bookmark
        wid = UIWid / 15;
        hei = UIHei / 30;
        bookmarkRect.sizeDelta = new Vector2(wid, hei);
        bookmarkRect.anchoredPosition = new Vector2(UIWid / 30 * 4, -UIHei / 30 * 20);

        // apply

        wid = UIWid / 10;
        hei = UIHei / 15;
        applyRect.sizeDelta = new Vector2(wid, hei);
        applyRect.anchoredPosition = new Vector2(-UIWid / 5, UIHei / 15 * 2);
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
