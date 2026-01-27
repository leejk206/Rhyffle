using UnityEngine;

public class SelectedSongManager : MonoBehaviour
{
    public Transform albumImageHolder;

    void Start()
    {
        string prefabPath = SelectedAlbumManager.Instance.albumPrefabPath;
        GameObject prefab = Resources.Load<GameObject>(prefabPath);

        if (prefab != null)
        {
            GameObject clone = Instantiate(prefab, albumImageHolder.position, Quaternion.identity, albumImageHolder);
            clone.transform.localPosition = Vector3.zero;
            clone.transform.localRotation = Quaternion.identity;
            clone.transform.localScale = Vector3.one;
        }
        else
        {
            Debug.LogWarning($"Resources에서 프리팹을 불러오지 못했습니다: {prefabPath}");
        }
    }
}