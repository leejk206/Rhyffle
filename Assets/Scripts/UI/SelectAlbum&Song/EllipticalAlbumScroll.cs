using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class EllipticalAlbumScroll : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // 회전 플래그
    public List<RectTransform> albumItems = new List<RectTransform>();
    public float radiusX = 400f;
    public float radiusY = 150f;
    public float centerAngle;
    public float spacingAngle = 20f;
    public float scaleMultiplier = 1.2f;
    public float scaleLerpSpeed = 5f;
    
    // 드래그 혹은 전환 플래그
    private float _dragDelta;
    private bool _isDragging;
    private Coroutine _snapRoutine;
    public float cutoffLineY; // 이 아래로는 안 보이게
    
    void Start()
    {
        centerAngle = CalculateCenteredAngle();
        UpdateAlbumPositions();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) centerAngle += spacingAngle;
        else if (Input.GetKeyDown(KeyCode.RightArrow)) centerAngle -= spacingAngle;

        UpdateAlbumPositions();
        
        for (int i = 0; i < albumItems.Count; i++)
        {
            RectTransform album = albumItems[i];
    
            // 기준보다 아래에 있으면 비활성화
            bool isAboveLine = album.anchoredPosition.y >= cutoffLineY;

            album.gameObject.SetActive(isAboveLine);
        }
    }
    
    float CalculateCenteredAngle()
    {
        float count = albumItems.Count;
        float offset = (count % 2 == 0) ? (count / 2f - 0.5f) : Mathf.Floor(count / 2f);
        return offset * -spacingAngle;
    }
    
    void UpdateAlbumPositions()
    {
        float count = albumItems.Count;
        float offset = (count % 2 == 0) ? (count / 2f - 0.5f) : Mathf.Floor(count / 2f);
        
        for (int i = 0; i < albumItems.Count; i++)
        {
            float angle = centerAngle + (i - offset) * spacingAngle;
            float rad = angle * Mathf.Deg2Rad;

            Vector2 pos = new Vector2(Mathf.Sin(rad) * radiusX, Mathf.Cos(rad) * radiusY);
            albumItems[i].anchoredPosition = pos;

            float distanceToCenter = Mathf.Abs(angle % 360);
            float scale = Mathf.Lerp(1f, scaleMultiplier, 1f - Mathf.Min(distanceToCenter / spacingAngle, 1f));
            albumItems[i].localScale = Vector3.Lerp(albumItems[i].localScale, Vector3.one * scale, Time.deltaTime * scaleLerpSpeed);
        }
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
        _dragDelta = 0f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _dragDelta += eventData.delta.x;
        centerAngle += eventData.delta.x * 0.1f;  // 조절값
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
        SnapToNearestAlbum();
    }
    
    void SnapToNearestAlbum(int direction = 0)
    {
        float count = albumItems.Count;
        float offset = (count % 2 == 0) ? (count / 2f - 0.5f) : Mathf.Floor(count / 2f);

        float relativeIndex = (centerAngle / spacingAngle) + offset;
        int nearestIndex = Mathf.RoundToInt(relativeIndex + direction); // 방향 반영
        float targetAngle = (nearestIndex - offset) * spacingAngle;
        
        StartCoroutine(SmoothSnap(targetAngle));

    }

    IEnumerator SmoothSnap(float targetAngle)
    {
        float duration = 0.3f;
        float time = 0f;
        float start = centerAngle;

        while (time < duration)
        {
            centerAngle = Mathf.Lerp(start, targetAngle, time / duration);
            UpdateAlbumPositions();
            time += Time.deltaTime;
            yield return null;
        }

        centerAngle = targetAngle;
        UpdateAlbumPositions();
    }
    
    public void ScrollLeft()
    {
        SnapToNearestAlbum(1);
    }

    public void ScrollRight()
    {
        SnapToNearestAlbum(-1);
    }
}
