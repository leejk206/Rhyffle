using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuildBoard : MonoBehaviour
{
    // canvas gameobject
    public int boardPos = 0;
    public int cardsPerLine;
    public List<GameObject> cardOnBoard;
    public GameObject deckBuildCard;
    public RectTransform rect;
    public RectTransform canvas;


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
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        scrWid = canvas.GetComponent<RectTransform>().rect.width;
        scrHei = canvas.GetComponent<RectTransform>().rect.height;
        boardHei = scrHei / 3;
        boardWid = scrWid / 10 * 9;
        boardY = -(scrHei / 4);
        rect.sizeDelta = new Vector2(boardWid, boardHei);
        rect.localPosition = new Vector3(0, boardY, 0);

        cardHei = boardHei / 5 * 4;
        cardWid = (cardHei * 5) / 8;
        cardsPerLine = Mathf.FloorToInt((boardWid - (boardHei / 20)) / (cardWid + (boardHei / 20)));

        cardY = boardY;

        leftMostCardPos = boardWid / 2 - boardHei / 10 - cardWid / 2;
        for (int i = 0; i < cardsPerLine; i++) { 
            cardX = - leftMostCardPos + (i * (leftMostCardPos * 2) / (cardsPerLine - 1));
            GameObject temp = Instantiate(deckBuildCard);
            temp.transform.SetParent(gameObject.transform);
            temp.GetComponent<RectTransform>().localPosition = new Vector2(cardX, 0);
            temp.GetComponent<RectTransform>().sizeDelta = new Vector2(cardWid, cardHei);
            temp.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
            cardOnBoard.Add(temp);
        }
    }

    public void ApplyCard(List<DeckCardInfo> cards)
    {

        for (int i = 0; i < cards.Count; i++)
        {
            cardOnBoard[i].SetActive(true);
            cardOnBoard[i].GetComponent<DeckBuildCard>().SetCard(cards[i]);
        }
        for (int i = cards.Count; i < cardsPerLine; i++)
        {
            cardOnBoard[i].SetActive(false);
        }

    }
}
