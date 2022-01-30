using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

/*
 * This script is activated when player is dragging the look btn
 *      Setting InputManager.Instance.IsDraggingLookBtn to true
 */

public class LookBtnDragProcessor : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    //public void OnDrag(PointerEventData eventData)
    //{
    //    Debug.Log("On drag");
    //}

    // set true when dragging the LookBtn
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        UIController.Instance.UpdateDebug("Begin drag");
        InputManager.Instance.SetDraggingLookBtnVal(true);
    }

    // set false when stopp dragging LookBtn
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");
        UIController.Instance.UpdateDebug("End drag");
        InputManager.Instance.SetDraggingLookBtnVal(false);
    }

}
