using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// This component handles Unity UI events and allows external handlers to be attached via delegates.
public class UI_EventHandler : MonoBehaviour,
    IPointerClickHandler,       // Handles click events
    IDragHandler,               // Handles drag events
    IPointerEnterHandler,       // Handles pointer enter (hover) events
    IPointerExitHandler,        // Handles pointer exit events
    IDropHandler                // Handles drop events
{
    // Delegates that external scripts can assign to in order to handle specific UI events
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnPointerEnterHandler = null;
    public Action<PointerEventData> OnPointerExitHandler = null;
    public Action<PointerEventData> OnDropHandler = null;

    // Called by Unity when the UI element is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData);
    }

    // Called by Unity when the UI element is dragged
    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData);
    }

    // Called when the pointer enters the UI element's area
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointerEnterHandler != null)
            OnPointerEnterHandler.Invoke(eventData);
    }

    // Called when the pointer exits the UI element's area
    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExitHandler != null)
            OnPointerExitHandler.Invoke(eventData);
    }

    // Called when something is dropped onto this UI element
    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropHandler != null)
            OnDropHandler.Invoke(eventData);
    }
}
