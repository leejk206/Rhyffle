using UnityEngine;
using System.Collections.Generic;

public class SongListGenerator : MonoBehaviour
{
    public GameObject songPrefab;
    public Transform content;
    
    [System.Serializable]
    public class SongData
    {
        public string title;
        public string artist;
        public string difficulty;
        public string record;
    }
    
    private List<GameObject> songInstances = new List<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            AddSong();
        }
    }
    
    void AddSong()
    {
        GameObject song = Instantiate(songPrefab, content, false);
        songInstances.Add(song);
    }

    private void Init()
    {
        GameObject index = Instantiate(songPrefab);
        index.transform.SetParent(GameObject.Find("Content").transform, false);

    }
    
    
}
