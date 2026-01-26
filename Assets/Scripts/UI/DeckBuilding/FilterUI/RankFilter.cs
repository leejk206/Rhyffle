using UnityEngine;
using UnityEngine.UI;

public class RankFilter : MonoBehaviour
{
    public int rank;
    public FilterUI filterUI;

    public void FilterSwitch()
    {
        filterUI.rank[rank] = !filterUI.rank[rank];
        if (filterUI.rank[rank])
        {
            // gameObject.GetComponent<Image>().color = Color.red;
        }
        else
        {
            // gameObject.GetComponent<Image>().color =Color.white;
        }
    }
}
