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
    // UI�� Order�� �����ִ� �ڵ�. Popup�� ��� 10���� ����, SceneUI�� ��� 0.
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay; // �����Ϸ� ���� ���� �ϱ��� ��.
        canvas.overrideSorting = true; // ĵ���� ��ø �� �θ��� sortingOrder�� ������� �ʰ� �������� ���� ����.

        if (sort) // PopupUI�� ���
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else // SceneUI�� ���
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
        // SceneUI�� Instantiate�ϴ� �ڵ�. 
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        // name�� ������� ��� name�� type�� ���� ����

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        // UI Prefab�� Instantiate��.
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        go.transform.SetParent(Root.transform);
        // �˾��� UI�� �����ϱ� ���� �� ������ ���� �ִ� �ڵ�


        return sceneUI;
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
        // PopupUI�� Instantiate�ϴ� �ڵ�
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
    // PopupUI�� �˻��� ���Ŀ� �ݴ� �ڵ�
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        } // ���� �ݰ� �ִ� UI�� ������ ���� ���� UI���� �˻�

        ClosePopupUI();
    }

    public void ClosePopupUI()
    // PopupUI�� �ݴ� �ڵ�
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
