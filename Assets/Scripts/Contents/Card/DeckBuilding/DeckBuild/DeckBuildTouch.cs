using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class DeckBuildTouch : MonoBehaviour
{
    public DeckBuildUI deckUI;
    public DeckBuildDeck deck;
    public Canvas canvas;


    public bool DeckDragMode = false;
    public bool CardMode = false;
    public float clickTimer = 0;
    public GameObject target;
    public Vector2 savedPos;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (CardMode)
            {
                if(clickTimer > 1)
                {
                    Vector2 mouseEndPos = Input.mousePosition;
                    PointerEventData endData = new PointerEventData(EventSystem.current);
                    endData.position = mouseEndPos;

                    List<RaycastResult> endResults = new List<RaycastResult>();
                    EventSystem.current.RaycastAll(endData, endResults);
                    if (endResults.Count <= 0 )
                    {
                        Debug.Log("entered");
                        DeckBuildCard tempCard = target.GetComponent<DeckBuildCard>();
                        int x = (int)tempCard.cardBaseInfo.card_suit * 14 + (int)tempCard.cardBaseInfo.card_rank;
                        deck.cardInfos[x] = tempCard.cardInfo;
                        deck.cardInDeck[x].GetComponent<DeckBuildSlot>().cardSlotCard.GetComponent<DeckBuildCard>().cardInfo = tempCard.cardInfo;
                        deck.cardInDeck[x].GetComponent<DeckBuildSlot>().cardSlotCard.GetComponent<DeckBuildCard>().cardBaseInfo = tempCard.cardBaseInfo;
                        Debug.Log("Added the card");
                    }
                    else if (target == endResults[0].gameObject)
                    {
                        target.GetComponent<DeckBuildCard>().OpenCardInfo();
                    }
                    else
                    {
                        if (!target.GetComponent<DeckBuildCard>().isInDeck)
                        {
                            // µ¶ø° ≥÷¿Ω
                            DeckBuildCard tempCard = target.GetComponent<DeckBuildCard>();
                            int x = (int)tempCard.cardBaseInfo.card_suit * 14 + (int)tempCard.cardBaseInfo.card_rank;
                            deck.cardInfos[x] = tempCard.cardInfo;
                            deck.cardInDeck[x].GetComponent<DeckBuildSlot>().cardSlotCard.GetComponent<DeckBuildCard>().cardInfo = tempCard.cardInfo;
                            deck.cardInDeck[x].GetComponent<DeckBuildSlot>().cardSlotCard.GetComponent<DeckBuildCard>().cardBaseInfo = tempCard.cardBaseInfo;
                            Debug.Log("Added the card");
                        }
                    }
                }
            }


            DeckDragMode = false;
            CardMode = false;
            target = null;
            clickTimer = 0;
        }

        if (Input.GetMouseButton(0))
        {

            if (CardMode)
            {
                clickTimer += Time.deltaTime;
            }else if (DeckDragMode)
            {
                deck.ChangePosition((Input.mousePosition.x - savedPos.x)/canvas.scaleFactor);
                savedPos.x = Input.mousePosition.x;
                Debug.Log("Moving " + (Input.mousePosition.x - savedPos.x) / canvas.scaleFactor);
            }
        }
        
        if (!Input.GetMouseButtonDown(0)) return;

        savedPos.x = Input.mousePosition.x;
        Vector2 mousePos = Input.mousePosition;
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.position = mousePos;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);
        if (results.Count < 0) return;
        if(results[0].gameObject.tag == "card")
        {
            target = results[0].gameObject;
            CardMode = true;
        }else if (results[0].gameObject.tag == "Deck")
        {
            DeckDragMode = true;
        }
    }
}
