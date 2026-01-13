using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class DeckBuildDeck : MonoBehaviour
{
    public int cardsPerLine;
    public RectTransform rect;
    public RectTransform canvas;
    public GameObject deckBuildSlot;
    public List<GameObject> cardInDeck;

    #region sizeInfo
    float scrWid;
    float scrHei;
    float boardHei;
    float boardWid;
    float boardY;
    float boardX;
    float cardHei;
    float cardWid;
    float cardY;
    float leftLimit;
    float rightLimit;
    float cardX;
    #endregion

    // use path parameter to get deck json
    // then load deck from that json
    public void LoadDeck(string path)
    {

    }

    public void NewDeck()
    {
        SetBoard();
    }

    private void Start()
    {
        SetBoard();
    }

    public void SetBoard()
    {
        rect = gameObject.GetComponent<RectTransform>();
        scrWid = canvas.GetComponent<RectTransform>().rect.width;
        scrHei = canvas.GetComponent<RectTransform>().rect.height;
        boardHei = scrHei / 3;
        cardHei = boardHei / 5 * 4;
        cardWid = (cardHei * 5) / 8;

        boardWid = cardWid * 56 + cardWid * 5.7f;
        
        boardY = (scrHei / 16 * 3);
        boardX = boardWid / 2 - scrWid/2;

        leftLimit = boardWid/2 - scrWid/2;
        rightLimit = - boardWid/2 + scrWid/2;

        rect.sizeDelta = new Vector2(boardWid, boardHei);
        rect.localPosition = new Vector3(boardX, boardY, 0);


        cardY = boardY;

        for(int i = 0; i < 56; i++)
        {
            cardX = -cardWid * (i - 27.5f) * 1.1f;
            GameObject temp = Instantiate(deckBuildSlot);
            temp.transform.SetParent(transform);
            temp.GetComponent<DeckBuildSlot>().cardSlotCard.GetComponent<RectTransform>().localPosition = Vector2.zero;
            temp.GetComponent<DeckBuildSlot>().cardSlotCard.GetComponent<RectTransform>().sizeDelta = new Vector2(cardWid, cardHei);
            temp.GetComponent<RectTransform>().localPosition = new Vector2(cardX, 0);
            temp.GetComponent<RectTransform>().sizeDelta = new Vector2(cardWid, cardHei);
            temp.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
            cardInDeck.Add(temp);
        }

        SetDeckPos(CardSuit.Spade, CardRank.Ten);
    }

    // Deck의 카드가 들어갈 위치에 따라서 달라짐
    public void SetDeckPos(CardSuit suit, CardRank rank) {
        int cardPos = (int)suit * 14 + (int)rank;
        if(cardPos < 0) cardPos = 0;
        float cardX = (-cardWid) * (cardPos - 27.5f) * 1.1f;
        Debug.Log(cardX);
        if (cardX < rect.localPosition.x - scrWid / 2 + cardWid || cardX > rect.localPosition.x + scrWid / 2 - cardWid)
        {
            if (cardX > leftLimit)
            {
                if (cardX < rightLimit)
                {
                    rect.localPosition = new Vector3(cardX, boardY, 0);
                }
                else
                {
                    rect.localPosition = new Vector3(rightLimit, boardY, 0);
                }
            }
            else
            {
                rect.localPosition = new Vector3(leftLimit, boardY, 0);
            }
        }
        else
        {

        }
    }

    public void DeckTestHeartTwo()
    {
        SetDeckPos(CardSuit.Heart, CardRank.Two);
    }
    public void DeckTestSpadeThree()
    {
        SetDeckPos(CardSuit.Spade, CardRank.Three);
    }
    public void DeckTestHeartThree()
    {
        SetDeckPos(CardSuit.Heart, CardRank.Three);
    }
    public void DeckTestClubKing()
    {
        SetDeckPos(CardSuit.Club, CardRank.King);
    }
    public void DeckTestSpadeTen()
    {
        SetDeckPos(CardSuit.Spade, CardRank.Ten);
    }
    public void DeckTestClubseven()
    {
        SetDeckPos(CardSuit.Club, CardRank.Seven);
    }
}
