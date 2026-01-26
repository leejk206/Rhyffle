using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class DeckBuildUI : CardMechUI
{
    // 덱보드들 필요한 GameObject
    public GameObject deckBoardPrefab;

    public RectTransform back;
    public RectTransform starDust;
    public RectTransform starPiece;
    public RectTransform filter;
    public RectTransform next;
    public RectTransform prev;



    // Start부분에서 각각의 UI 위치들 화면 비율에 맞게 조정
    private void Start()
    {
        base.Start();
        CreateBoards(0);
        cardPerWidth = boardArray[0].GetComponent<DeckBuildBoard>().cardsPerLine;
        cardPerHeight = 1;
        SetCards();
        SetUI();
    }

    public void SetUI()
    {
        back.sizeDelta = new Vector2(rect.rect.width/15, rect.rect.height/30);

    }

    public override void CreateBoards(int pos)
    {
        GameObject tempBoard = Instantiate(boardPrefab,gameObject.transform);
        tempBoard.GetComponent<DeckBuildBoard>().SetBoard();
        boardArray[0] = tempBoard;
        boardArray[1] = tempBoard;
        boardArray[2] = tempBoard;
    }

    public override void NextCards()
    {
        cardPage++;
        SetCards();
    }

    public override void PrevCards() { 
        cardPage--; 
        SetCards();
    }

    public override void SetCards()
    {
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
        for (int i = cardFirst; i < cardFirst + cardPerHeight * cardPerWidth && i < filteredCards.Count; i++) { 
            cards.Add(filteredCards[i]);
        }

        boardArray[1].GetComponent<DeckBuildBoard>().ApplyCard(cards);
    }

    // 카드 정보 갱신 되었을 때 화면에 나온 카드들 정보 재입력

    //터치 리딩하는 부분
}
