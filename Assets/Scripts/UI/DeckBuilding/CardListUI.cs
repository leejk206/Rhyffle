using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class CardListUI : CardMechUI
{


    private void Start()
    {
        base.Start();
        cardPerWidth = boardArray[0].GetComponent<CardListBoard>().cardsPerLine;
        cardPerHeight = 2;
        SetCards();
    }

    public override void CreateBoards(int pos)
    {
        GameObject tempBoard = Instantiate(boardPrefab,gameObject.transform);
        tempBoard.GetComponent<RectTransform>().localScale = Vector3.one;
        tempBoard.GetComponent<CardListBoard>().boardPos = pos - 1;
        tempBoard.GetComponent<CardListBoard>().SetBoard();
        boardArray[pos] = tempBoard;
    }

    public override void NextCards()
    {
        cardPage++;
        SetCards();
    }

    public override void PrevCards()
    {
        cardPage--;
        SetCards();
    }

    public override void SetCards()
    {
        // Online version
        // Send Filters, Page, CardPerBoard
        // server will calculate which data to send
        // recive the json list as string
        // serialize the string and save in cardInfo list cards

        // HardCode version
        // Pick cards in range from filtered list saved in searchListPath
        // save certain cardInfos from lists

        // searchCardList 전체 확인
        List<DeckCardInfo> filteredCards = JObject.Parse(File.ReadAllText(searchListPath))["card_index"].ToObject<List<DeckCardInfo>>();
        int cardFirst;
        if(cardPage * cardPerHeight * cardPerWidth > filteredCards.Count)
        {
            cardPage = 0;
            cardFirst = 0;
        }
        if(cardPage < 0)
        {
            cardPage = Mathf.FloorToInt(filteredCards.Count / (cardPerHeight * cardPerWidth));
        }
        cardFirst = cardPage * cardPerHeight * cardPerWidth;
        cards.Clear();
        Debug.Log(cardFirst + "_" + (cardFirst + cardPerHeight * cardPerWidth) + "_" + cardPerHeight + "_" + cardPerWidth);
        for(int i = cardFirst; i < cardFirst + cardPerHeight * cardPerWidth && i< filteredCards.Count; i++)
        {
            cards.Add(filteredCards[i]);
        }


        // 가운데 보드에 ApplyCards 적용
        boardArray[1].GetComponent<CardListBoard>().ApplyCard(cards);
    }

}
