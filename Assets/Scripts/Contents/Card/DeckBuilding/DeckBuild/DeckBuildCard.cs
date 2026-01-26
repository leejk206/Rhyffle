using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine;

public class DeckBuildCard : MonoBehaviour
{
    string cardBaseJsonPath = "Data/Json/CardData";

    public bool isInDeck = false;

    public DeckCardInfo cardInfo;
    public CardBaseInfo cardBaseInfo;
    DeckBuildUI deckBuildUI;
    float saveX, saveY;
    RectTransform rect;

    public void SetCard(DeckCardInfo cardInfo)
    {
        rect = gameObject.GetComponent<RectTransform>();
        deckBuildUI = GameObject.Find("DeckBuildingUI").GetComponent<DeckBuildUI>();
        this.cardInfo = cardInfo;
        LoadCardInfo();
        ApplyCard();
    }

    public void LoadCardInfo()
    {
        var tempText = Resources.Load<TextAsset>(cardBaseJsonPath).text;
        var tempCardIdx = (JArray)JObject.Parse(tempText)["card_index"];
        cardBaseInfo = tempCardIdx.FirstOrDefault(baseId => (int)baseId["card_baseid"] == cardInfo.card_baseid).ToObject<CardBaseInfo>();
    }

    // card Illustitraion application
    public void ApplyCard()
    {

    }

    public void OpenCardInfo()
    {
        deckBuildUI.OpenCardInfo(cardInfo, cardBaseInfo);
    }

    public DeckCardInfo SendCardInfo()
    {
        return cardInfo;
    }

    public CardBaseInfo SendCardBaseInfo()
    {
        return cardBaseInfo;
    }

}
