using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SongListGenerator : MonoBehaviour
{
    public GameObject songPrefab;
    public Transform content;
    
    [System.Serializable]
    public class SongData
    {
        public string title;
        public string difficulty;
        public int level;
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
        GameObject song = Instantiate(songPrefab, content);
        
        // 🔧 위치·회전·스케일 초기화 (정렬 깨지는 문제 해결!)
        RectTransform rt = song.GetComponent<RectTransform>();
        rt.localPosition = Vector3.zero;
        rt.localRotation = Quaternion.identity;
        rt.localScale = Vector3.one;
        
        // 임의 텍스트 
        var texts = song.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var text in texts)
        {
            if (text.name.Contains("Title")) text.text = "Music";
            if (text.name.Contains("Level")) text.text = "(Level 10)";
            if (text.name.Contains("Diff")) text.text = "EXCEED";
        }

        songInstances.Add(song);
    }

    private void Init()
    {
        int yValue = 0;
        //ContentOl| Instantiate
        var index = Instantiate(songPrefab, new Vector3(0, yValue, 0), Quaternion.identity); 
        index. transform.SetParent(GameObject.Find("Content"). transform);
        yValue -= 200;
    }
    
    
}
