using System;
using UnityEngine;

public class DeckBuildCard : MonoBehaviour
{
    bool isNull = false;
    bool cardInDeck;   
    public SpriteRenderer ownCardSprite;

    CardInfo cardInfo;
    CardBaseInfo cardBaseInfo;

    // Position before movement of card
    Vector2 cardPosBeforeMove;

    #region Card
    public CardBaseInfo CardBaseInfo()
    {
        return cardBaseInfo;
    }
    public CardInfo CardInfo()
    {
        return cardInfo;
    }
    #endregion

    #region PosMove
    // Setting beforePosition to comeback
    public void SetBeforePos()
    {
        cardPosBeforeMove = gameObject.transform.position;
    }

    public void FollowPos(Vector2 pos)
    {
        gameObject.transform.position = pos;
    }

    public void ResetPos()
    {
        gameObject.transform.position = cardPosBeforeMove;
    }
    #endregion

    #region CardInDeckSprite
    // Reseting cardInDeck
    public void ResetCardInDeck()
    {
        cardInDeck = false;
    }

    // searching whether this card is in deck
    // must be called several times by each card in deck
    public void IsCardInDeck(CardInfo deckCard)
    {
        if(deckCard.card_baseid == cardInfo.card_baseid)
        {
            cardInDeck = true;
        }
    }

    // Showing sprite of inDeck sprite 
    public void ShowIsCardInDeck()
    {
        if(cardInDeck)
        {
            ownCardSprite.enabled = true;
            // 여기서 카드 있다는 것 표시하는 이펙트
        }
        else
        {
            ownCardSprite.enabled = false;
        }
    }
    #endregion


    // Used only on CardBoard
    public void ChangeNull(bool isNull)
    {
        if (isNull) { gameObject.SetActive(false); this.isNull = true; }
        else {  gameObject.SetActive(true); this.isNull = false; }
    }
    public bool IsNull
    {
        get { return this.isNull; }
    }

}
