using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerClickHandler
{
    public Action<PointerEventData> onBeginDragHandler = null;
    public Action<PointerEventData> onDragHandler = null;
    public Action<PointerEventData> onClickHandler = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");

        if (onBeginDragHandler != null)
            onBeginDragHandler.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // transform.position = eventData.position;
        // Debug.Log("OnDrag");

        if (onDragHandler != null)
            onDragHandler.Invoke(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClickHandler != null)
            onClickHandler.Invoke(eventData);
    }
}
