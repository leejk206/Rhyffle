using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager
{

    int _order = 10;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
            {
                root = Managers.Resource.Instantiate("UI/Scene/@UI_Root");
            }
            return root;
        }
    }

    public void MakeUIRoot()
    {
        Root.transform.rotation = Quaternion.identity;
    }

    public void SetCanvas(GameObject go, bool sort = true)
    // UI의 Order를 정해주는 코드. Popup의 경우 10부터 시작, SceneUI의 경우 0.
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay; // 이해하려 하지 말고 암기할 것.
        canvas.overrideSorting = true; // 캔버스 중첩 시 부모의 sortingOrder에 영향받지 않고 독립적인 값을 지님.

        if (sort) // PopupUI의 경우
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else // SceneUI의 경우
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
        // SceneUI를 Instantiate하는 코드. 
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        // name이 비어있을 경우 name을 type과 같게 설정

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        // UI Prefab을 Instantiate함.
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        go.transform.SetParent(Root.transform);
        // 팝업된 UI를 정리하기 쉽게 한 곳으로 몰아 넣는 코드


        return sceneUI;
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
        // PopupUI를 Instantiate하는 코드
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform);

        return popup;
    }

    public void ShowExtraLoadingUI()
    {
        ShowPopupUI<LoadingExtraUI>();
    }

    public void ShowHardLoadingUI()
    {
        ShowPopupUI<LoadingHardUI>();
    }

    public void CloseLoadingUI()
    {
        ClosePopupUI();
    }

    public void ClosePopupUI(UI_Popup popup)
    // PopupUI를 검사한 이후에 닫는 코드
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        } // 지금 닫고 있는 UI가 실제로 가장 위의 UI인지 검사

        ClosePopupUI();
    }

    public void ClosePopupUI()
    // PopupUI를 닫는 코드
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        _sceneUI = null;
    }
}
