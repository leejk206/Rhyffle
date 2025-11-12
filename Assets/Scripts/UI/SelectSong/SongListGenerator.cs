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
}
