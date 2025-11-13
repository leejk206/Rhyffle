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

    void Start()
    {
        Init();
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
