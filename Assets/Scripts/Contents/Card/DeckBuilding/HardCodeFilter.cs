using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.VisualScripting;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Linq;

public class HardCodeFilter : MonoBehaviour
{
    private string cardDataPath;
    private string searchListPath;
    private string playerCardPath;

    public List<CardInfo> totalCards;
    public List<CardInfo> filteredCards;
    private void Start()
    {
        cardDataPath = "Data/Json/CardData";
        searchListPath = "/SearchList.json";
        playerCardPath = "Data/Json/PlayerCard";
        filteredCards = new List<CardInfo>();
        totalCards = new List<CardInfo>();
        var playerCardJson = Resources.Load<TextAsset>(playerCardPath).text;
        var playerPage = JObject.Parse(playerCardJson);
        totalCards = playerPage["card_index"].ToObject<List<CardInfo>>();
        filteredCards = playerPage["card_index"].ToObject<List<CardInfo>>();
        NewSets();
    }


    // 필터 적용
    // Apply Parameter then execute the function --> Changes filteredCards List
    public void ApplyFilter(List<Define.CardRank> rank, List<Define.CardRarity> rarity, List<Define.CardSuit> suit, bool owned, bool bookmark)
    {
        filteredCards.Clear();

        bool filterRank = false;
        bool filterSuit = false;
        bool filterRarity = false;
        bool filterBookmark = false;
        
        var tempBaseJson = Resources.Load<TextAsset>(cardDataPath).text;
        var cardDataPage = (JArray)JObject.Parse(tempBaseJson)["card_index"];
        
        foreach (CardInfo card in totalCards)
        {
            CardBaseInfo cardData = cardDataPage.FirstOrDefault(baseid => (int)baseid["card_baseid"] == card.card_baseid).ToObject<CardBaseInfo>();
            if(rank.Count != 0)
            {
                foreach (Define.CardRank r in rank)
                {
                    if (r == cardData.card_rank)
                    {
                        filterRank = true;
                    }
                }
            }
            else{
                filterRank = true;
            }
            if (rarity.Count != 0)
            {
                foreach (Define.CardRarity r in rarity)
                {
                    if (r == cardData.card_rarity)
                    {
                        filterRarity = true;
                    }
                }
            }
            else
            {
                filterRarity = true;
            }
            if (suit.Count != 0)
            {
                foreach (Define.CardSuit s in suit)
                {
                    if (s == cardData.card_suit)
                    {
                        filterSuit = true;
                    }
                }
            }
            else
            {
                filterSuit = true;
            }
            if (bookmark)
            {
                if (card.bookmarked)
                {
                    filterBookmark = true;
                }
            }
            else
            {
                filterBookmark = true;
            }
            if (filterRank && filterRarity && filterSuit && filterBookmark)
            {
                filteredCards.Add(card);
            }
            filterRank = false;
            filterRarity = false;
            filterSuit = false;
            filterBookmark = false;
        }
        // own 적용 안 함 아직
        NewSets();
    }

    // 여기서 Resources의 Json으로 바꿈
    public void NewSets()
    {
        CardPage tempPage = new CardPage();
        tempPage.card_page = 0;
        tempPage.card_index = filteredCards.ToArray();
        string searchListTxt = JsonConvert.SerializeObject(tempPage);
        File.WriteAllText(Application.persistentDataPath + searchListPath, searchListTxt);
    }
}
