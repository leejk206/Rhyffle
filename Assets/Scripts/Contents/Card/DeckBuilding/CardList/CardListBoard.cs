using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using System;

public class CardListBoard : MonoBehaviour
{
    //하나의 CardBoard에 들어갈 카드 열 수
    // BoardPos shows this board is before or current or next board showing up
    public int boardPos = 0;
    public int cardsPerLine;
    public List<GameObject> cardOnBoard;
    public GameObject cardListCard;
    RectTransform rect;

    #region sizeInfo
    float scrWid;
    float scrHei;
    float boardHei;
    float boardWid;
    float boardY;
    float cardHei;
    float cardWid;
    float cardY;
    float leftMostCardPos;
    float cardX;
    #endregion

    public void SetBoard()
    {
        rect = gameObject.GetComponent<RectTransform>();
        scrWid = gameObject.transform.parent.parent.gameObject.GetComponent<RectTransform>().rect.width;
        scrHei = gameObject.transform.parent.parent.gameObject.GetComponent<RectTransform>().rect.height;
        boardHei = scrHei / 6 * 5;
        boardWid = scrWid / 10 * 9;
        boardY = - (scrHei /24 );
        rect.sizeDelta = new Vector2(boardWid, boardHei);
        rect.localPosition = new Vector3(0, boardY, 0);

        cardHei = boardHei / 40 * 17;
        cardWid = (cardHei * 5) / 8;
        cardsPerLine = Mathf.FloorToInt((boardWid - (boardHei / 20))/(cardWid + (boardHei/20)));


        cardY = boardHei / 40 + cardHei / 2; ;

        leftMostCardPos = boardWid/2 - boardHei/10 - cardWid / 2;

        for (int i = 0; i < 2; i++)
        {
            for(int j = 0; j < cardsPerLine; j++)
            {
                cardX = -leftMostCardPos + (j * (leftMostCardPos * 2) / (cardsPerLine - 1));
                GameObject temp = Instantiate(cardListCard);
                temp.transform.SetParent(gameObject.transform);
                temp.GetComponent<RectTransform>().localPosition = new Vector2(cardX, cardY * (- 2* i + 1));
                temp.GetComponent<RectTransform>().sizeDelta = new Vector2(cardWid, cardHei);
                temp.GetComponent<RectTransform>().localScale = new Vector2(1,1);
                cardOnBoard.Add(temp);
            }
        }
        Move(boardPos);
    }
    public void Move(int pos)
    {
        rect.localPosition = new Vector3(pos * transform.parent.gameObject.GetComponent<RectTransform>().rect.width, boardY,0);
    }

    public void ApplyCard(List<CardInfo> cards)
    {

        for(int i = 0; i < cards.Count; i++)
        {
            cardOnBoard[i].SetActive(true);
            cardOnBoard[i].GetComponent<CardListCard>().SetCard(cards[i]);
        }
        for(int i = cards.Count; i <cardsPerLine * 2; i++)
        {
            cardOnBoard[i].SetActive(false);
        }

    }

}
