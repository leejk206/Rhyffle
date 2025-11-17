using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class CardListUI : MonoBehaviour
{
    RectTransform rect;
    int cardPerBoard = 0;
    GameObject[] cardBoards = new GameObject[3];

    private void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        float UIWid = gameObject.transform.parent.gameObject.GetComponent<RectTransform>().rect.width;
        float UIHei = gameObject.transform.parent.gameObject.GetComponent<RectTransform>().rect.height;
        rect.sizeDelta = new Vector2 (UIWid, UIHei);
    
        // Board Creation

        // 여기서 보드 크기 확인
    }

    //우측 이동 함수
    public void NextCards()
    {
        // 왼쪽 보드 cardBoards에서 제거하고 삭제
        // 가운데 보드를 왼쪽으로 이동하고 cardBoard 정보 이동
        // 오른쪽 보드를 가운데로 이동하고 cardBoard 정보 이동
        // 새로운 보드를 생성하고 cardBoard에 추가
        // 현재 보드에 카드 정보 입력
        // 카드 각각에 들어가서 카드 정보 띄우기
    }

    public void PreviousCards() { 
        // NextCards 반대로 작동
    }

    public void ApplyFilter()
    {

    }
    
}
