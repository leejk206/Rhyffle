using UnityEngine;
using UnityEngine.UI;

public class BookmarkFilter : MonoBehaviour
{
    public FilterUI filterUI;

    public void FilterSwitch()
    {
        filterUI.bookmark = !filterUI.bookmark;
        if (filterUI.bookmark)
        {
            gameObject.GetComponent<Image>().color = Color.red;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }
}
