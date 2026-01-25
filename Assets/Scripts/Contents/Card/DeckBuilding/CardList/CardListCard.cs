using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using UnityEngine.EventSystems;

public class CardListCard : MonoBehaviour
{
    string cardBaseJsonPath = "Data/Json/CardData";
    public DeckCardInfo cardInfo;
    CardBaseInfo cardBaseInfo;
    CardListUI cardListUI;

    public void SetCard(DeckCardInfo cardInfo)
    {
        cardListUI = GameObject.Find("CardListUI").GetComponent<CardListUI>();
        this.cardInfo = cardInfo;
        LoadCardInfo();
        ApplyCard();
    }

    // Loading card info based on CardBaseInfo and CardInfo
    public void LoadCardInfo()
    {
        var tempText = Resources.Load<TextAsset>(cardBaseJsonPath).text;
        var tempCardIdx = (JArray)JObject.Parse(tempText)["card_index"];
        cardBaseInfo = tempCardIdx.FirstOrDefault(baseid => (int)baseid["card_baseid"] == cardInfo.card_baseid).ToObject<CardBaseInfo>();
    }

    // Card Illustration or card UI based on cardBaseInfo
    public void ApplyCard() { }

    // Opens CardInfoUI through cardListUI
    public void OpenCardInfo()
    {
        cardListUI.OpenCardInfo(cardInfo, cardBaseInfo);
    }

    public DeckCardInfo SendCardInfo()
    {
        return cardInfo;
    }

    public CardBaseInfo SendCardBaseInfo()
    {
        return cardBaseInfo;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OpenCardInfo();
    }
}
