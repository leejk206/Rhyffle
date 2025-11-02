using UnityEngine;
using System.Collections;

public class CardListRatio : MonoBehaviour
{
    Resolution[] resolutions;
    public GameObject cardBoard;
    public GameObject UIBack;

    private void Start()
    {
        resolutions = Screen.resolutions;
    }
}
