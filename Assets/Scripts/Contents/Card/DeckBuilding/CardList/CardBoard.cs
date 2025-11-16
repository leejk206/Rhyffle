using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using System;

public class CardBoard : MonoBehaviour
{
    //하나의 CardBoard에 들어갈 카드 열 수
    public int cardsPerLine;
    public List<GameObject> cardOnBoard;
    public GameObject cardListCard;
    RectTransform rect;
    private void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        float scrWid = gameObject.transform.parent.gameObject.GetComponent<RectTransform>().rect.width;
        float scrHei = gameObject.transform.parent.gameObject.GetComponent<RectTransform>().rect.height;
        float boardHei = scrHei / 6 * 5;
        float boardWid = scrWid / 10 * 9;
        float boardY = - (scrHei /24 );
        Debug.Log(boardHei + "_" + boardWid);
        rect.sizeDelta = new Vector2(boardWid, boardHei);
        rect.localPosition = new Vector3(0, boardY, 0);

        float cardHei = boardHei / 40 * 17;
        float cardWid = (cardHei * 5) / 8;
        Debug.Log(cardHei + "_" + cardWid);
        cardsPerLine = Mathf.FloorToInt((boardWid - (boardHei / 20))/(cardWid + (boardHei/20)));


        float cardY = boardHei / 40 + cardHei / 2; ;

        float leftMostCardPos = boardWid/2 - boardHei/10 - cardWid / 2;

        for (int i = 0; i < 2; i++)
        {
            for(int j = 0; j < cardsPerLine; j++)
            {
                float cardX = -leftMostCardPos + (j * (leftMostCardPos * 2) / (cardsPerLine - 1));
                GameObject temp = Instantiate(cardListCard);
                temp.transform.SetParent(gameObject.transform);
                temp.GetComponent<RectTransform>().localPosition = new Vector2(cardX, cardY * (2* i - 1));
                temp.GetComponent<RectTransform>().sizeDelta = new Vector2(cardWid, cardHei);
                temp.GetComponent<RectTransform>().localScale = new Vector2(1,1);
                cardOnBoard.Add(temp);
            }
        }
    }

}
