using UnityEngine;

public class SelectedAlbumManager : MonoBehaviour
{
    public static SelectedAlbumManager Instance;

    public string albumPrefabPath;
    public string jsonPath;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}