using UnityEngine;

public class DeckBuildDeck : MonoBehaviour
{
    public int cardsPerLine;
    public RectTransform rect;
    public RectTransform canvas;
    public GameObject deckBuildSlot;

    #region sizeInfo
    float scrWid;
    float scrHei;
    float boardHei;
    float boardWid;
    float boardY;
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
        boardWid = scrWid;
        boardY = (scrHei / 16 * 3);
        rect.sizeDelta = new Vector2(boardWid, boardHei);
        rect.localPosition = new Vector3(0, boardY, 0);

        cardHei = boardHei / 5 * 4;
        cardWid = (cardHei * 5) / 8;

        cardY = boardY;

        for(int i = 0; i < 52; i++)
        {
            cardX = (boardHei / 20) + cardWid / 2;
        }
    }
}
