using UnityEngine.UI;
using UnityEngine;

public class SuitFilter : MonoBehaviour
{
    public int suit;
    public FilterUI filterUI;

    public void FilterSwitch()
    {
        filterUI.suit[suit] = !filterUI.suit[suit];
        if (filterUI.suit[suit])
        {
            gameObject.GetComponent<Image>().color = Color.red;
        }
        else
        {
            gameObject.GetComponent<Image>().color =Color.white;
        }
    }
}
