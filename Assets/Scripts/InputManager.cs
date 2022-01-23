using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

/*
 * Test code for touchscreen input action control but newer working code are moved to PlayerController
 * NOTE: contain some touch screen control concept. Might be helpful in the future. 
 */

//[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public PlayerInputActions playerInput;

    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;

    public Vector2 dragTouch;


    private void Awake()
    {
        playerInput = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Start()
    {
        playerInput.PlayerAction.TouchDragLook.started += callBackContext => StartTouchPrimary(callBackContext);
        //playerInput.PlayerAction.TouchDragLook.performed += callBackContext => EndTouchPrimary(callBackContext);
    }

    private void Update()
    {
        //Debug.Log(dragTouch);
    }



    void StartTouchPrimary(InputAction.CallbackContext context)
    {
        //InputActionPhase tPhase = playerInput.PlayerAction.TouchDragLook.phase;
        //Debug.Log(tPhase.ToString());

        //dragTouch = context.ReadValue<TouchState>().delta;
        //Debug.Log("Input Manager subscribe drag:" + dragTouch);
    }

    //void EndTouchPrimary(InputAction.CallbackContext context)
    //{
    //}





    //void SingleFire(InputAction.CallbackContext context)
    //{
    //    Debug.Log("Single Fire: " + context.action);
    //}


    // NOTE: not using for now fixed other way. Kept for reference
    //void Drag(InputAction.CallbackContext context)
    //{
    //    Debug.Log("Drag Look: " + playerInputActions.Touch.TouchDragLook.ReadValue<Vector2>());
    //}

    // an example to return screen cordinate after touching
    // void StartTouch(InputAction.CallbackContext context)
    // {
    //     Debug.Log("Touch Started: " + playerInputActions.Touch.TouchSingleFire.ReadValue<Vector2>());
    // }

}
