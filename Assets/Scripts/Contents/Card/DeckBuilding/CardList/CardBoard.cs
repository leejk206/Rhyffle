using UnityEngine;
using System.Collections;

public class CardBoard : MonoBehaviour
{
    Resolution[] resolutions;
    private void Start()
    {
        resolutions = Screen.resolutions;
        Debug.Log(resolutions);
    }

}
