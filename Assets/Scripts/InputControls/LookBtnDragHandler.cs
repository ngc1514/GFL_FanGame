using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

public class LookBtnDragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    //public void OnDrag(PointerEventData eventData)
    //{
    //    Debug.Log("On drag");
    //}

    // set true only when dragging the LookBtn
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        UIController.Instance.UpdateDebug("Begin drag");
        InputManager.Instance.SetDraggingLookBtnVal(true);
    }

    // set false only when stopp dragging LookBtn
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");
        UIController.Instance.UpdateDebug("End drag");
        InputManager.Instance.SetDraggingLookBtnVal(false);
    }

}
