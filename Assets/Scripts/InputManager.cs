using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/*
 * Test code for touchscreen input action control but newer working code are moved to PlayerController
 * NOTE: contain some touch screen control concept. Might be helpful in the future. 
 */

public class InputManager : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void Start()
    {
        // when TouchSingleFire action is started, subscribe to callBackContext variable and call function SingleFire(arg)
        playerInputActions.Touch.TouchSingleFire.started += callBackContext => SingleFire(callBackContext);


        //inputControl.Touch.TouchDragLook.started += callBackContext => Drag(callBackContext);

        // an example to return screen cordinate after touching
        // playerInputActions.Touch.TouchSingleFire.started += callBackContext => StartTouch(callBackContext);
        // an example of endinga touch
        // playerInputActions.Touch.TouchSingleFire.canceled += callBackContext => EndTouch(callBackContext);


    }


    void SingleFire(InputAction.CallbackContext context)
    {
        Debug.Log("Single Fire: " + context.action);
    }




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
