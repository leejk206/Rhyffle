using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class AlbumListGenerator : MonoBehaviour
{
    public GameObject albumPrefab;
    public GameObject activeImage;

    public Transform leftAnchor;
    public Transform centerAnchor;
    public Transform rightAnchor;
    public Transform hiddenAnchor;

    private List<GameObject> albumInstances = new List<GameObject>();
    private int viewCenterIndex = 0;
    
    private Dictionary<GameObject, Vector3> targetPositions = new();
    private Dictionary<GameObject, Vector3> targetScales = new();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            AddAlbum();
            UpdateAlbumPositions();
        }
        
        // 매 프레임 부드럽게 이동
        foreach (var album in albumInstances)
        {
            if (!targetPositions.ContainsKey(album)) continue;

            album.transform.position = Vector3.Lerp(
                album.transform.position,
                targetPositions[album],
                Time.deltaTime * 10f // 보간 속도 조절
            );

            album.transform.localScale = Vector3.Lerp(
                album.transform.localScale,
                targetScales[album],
                Time.deltaTime * 10f
            );
        }
    }

    void AddAlbum()
    {
        GameObject album = Instantiate(albumPrefab, hiddenAnchor.transform.position, Quaternion.identity, this.transform);
        albumInstances.Add(album);
        
        targetPositions[album] = hiddenAnchor.transform.position;
        targetScales[album] = Vector3.one;
    }

    void UpdateAlbumPositions()
    {
        Vector3 left = leftAnchor.position;
        Vector3 center = centerAnchor.position;
        Vector3 right = rightAnchor.position;

        // 좌중간 우중간 위치 보정
        Vector3 midLeft = Vector3.Lerp(left, center, 0.45f);
        Vector3 midRight = Vector3.Lerp(center, right, 0.55f);

        Vector3[] positions = new Vector3[]
        {
            left,
            midLeft,
            center,
            midRight,
            right
        };

        float curveStrength = 0.1f; // 타원 궤도 높이

        for (int i = 0; i < albumInstances.Count; i++)
        {
            GameObject album = albumInstances[i];
            int offset = i - viewCenterIndex;

            if (offset >= -2 && offset <= 2)
            {
                int anchorIndex = offset + 2; // -2~2 → 0~4

                // 원래 위치 + 타원 곡선 보정
                Vector3 basePos = positions[anchorIndex];
                float yCurve = -Mathf.Pow(offset, 2) * curveStrength;
                Vector3 curvePos = new Vector3(basePos.x, basePos.y + yCurve, basePos.z);

                // 가운데 앨범은 크기 확대, 나머지는 원래 크기
                targetPositions[album] = curvePos;
                targetScales[album] = (offset == 0) ? Vector3.one * 1.15f : Vector3.one;
            }
            else
            {
                targetPositions[album] = hiddenAnchor.transform.position;
                targetScales[album] = Vector3.one;
            }
            
            Transform activeObj = album.transform.Find("Active");
            if (activeObj != null)
            {
                bool isCenter = (offset == 0);
                activeObj.gameObject.SetActive(isCenter);
            }
        }
    }
    
    // Active 활성화
    public void SetActiveVisual(bool isActive)
    {
        activeImage.SetActive(isActive);
    }


    public void LeftButtonClicked()
    {
        if (viewCenterIndex > 0)
        {
            viewCenterIndex--;
            UpdateAlbumPositions();
        }
    }

    public void RightButtonClicked()
    {
        if (viewCenterIndex < albumInstances.Count - 1)
        {
            viewCenterIndex++;
            UpdateAlbumPositions();
        }
    }
}
