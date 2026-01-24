using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainController : MonoBehaviour
{
    [Header("탭 버튼 텍스트")]
    public TextMeshProUGUI cardTabText;
    public TextMeshProUGUI craftTabText;

    [Header("탭 벋튼 색상")]
    public Color selectedColor = Color.white;
    public Color deselectedColor = new Color32(193, 193, 193, 255);

    public void OnClickCardTab()
    {
        cardTabText.color = selectedColor;
        craftTabText.color = deselectedColor;
    }

    public void OnClickCraftTab()
    {
        cardTabText.color = deselectedColor;
        craftTabText.color = selectedColor;
    }

    private void Start()
    {
        OnClickCardTab();
    }
}