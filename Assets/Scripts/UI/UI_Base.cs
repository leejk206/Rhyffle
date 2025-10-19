using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Base class for all UI components
public abstract class UI_Base : MonoBehaviour
{
    // 모든 UI의 베이스 코드
    // 사용 방법은 이 코드를 상속한 다른 코드를 참고할 것.


    // Dictionary to store UI elements by their type (e.g., Button, Text, etc.)
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    // Each subclass must implement its own Init() to set up bindings
    public abstract void Init();

    // Unity's Start() calls Init() when the object is first initialized
    void Start()
    {
        Init(); // Subclasses must implement Init(), and this gets called automatically at runtime
    }

    // Binds UI elements using enum names as keys
    // Example: if you have enum { Button1, Button2 }, it finds GameObjects with those names
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type); // Get all enum names as strings
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects); // Store the array in the dictionary using its type as the key

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                // Search for a GameObject with the specified name
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                // Search for a specific component (e.g., Button, Text) inside the GameObject
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to Bind ({names[i]})"); // Log error if the object was not found
        }
    }

    // Retrieves a previously bound object by index
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    // Shortcut methods for retrieving specific UI types
    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected TextMeshProUGUI GetTMP(int idx) { return Get<TextMeshProUGUI>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }

    // Binds an event handler (like Click or Drag) to a UI GameObject
    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        // Make sure the GameObject has a UI_EventHandler component
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        // Assign the correct delegate based on event type
        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action; // Prevent duplicates
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
            case Define.UIEvent.Drop:
                evt.OnDropHandler -= action;
                evt.OnDropHandler += action;
                break;
            case Define.UIEvent.PointerEnter:
                evt.OnPointerEnterHandler -= action;
                evt.OnPointerEnterHandler += action;
                break;
            case Define.UIEvent.PointerExit:
                evt.OnPointerExitHandler -= action;
                evt.OnPointerExitHandler += action;
                break;
        }
    }
}
