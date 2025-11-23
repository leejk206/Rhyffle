using UnityEngine;

public class DeckBuildDeck : MonoBehaviour
{
    Rect rect;
    DeckBuildDeckCard[] cards;

    #region sizeInfo
    float scrWid;
    float scrHei;
    float boardH;
    float boardW;
    float boardY;
    float cardH;
    float cardW;
    float cardY;
    float leftEnd;
    float rightEnd;
    float cardX;
    #endregion

    // use path parameter to get deck json
    // then load deck from that json
    public void LoadDeck(string path)
    {

    }

    public void NewDeck()
    {
        cards = new DeckBuildDeckCard[56];
    }

    public void SetDeckBoard()
    {
        rect = gameObject.GetComponent<Rect>();
        
    }
}
