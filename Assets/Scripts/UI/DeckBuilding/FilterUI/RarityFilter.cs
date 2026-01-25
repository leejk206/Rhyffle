using UnityEngine;
using UnityEngine.UI;

public class RarityFilter : MonoBehaviour
{
    public int rarity;
    public FilterUI filterUI;

    public void FilterSwitch()
    {
        filterUI.rarity[rarity] = !filterUI.rarity[rarity];
        if (filterUI.rarity[rarity])
        {
            gameObject.GetComponent<Image>().color = Color.red;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }
}
