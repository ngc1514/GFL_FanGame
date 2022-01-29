using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


/*
 * This custom processor detects whether Touch input delta is activated via dragging the LookBtn.
 * If dragging LookBtn:
 *      send Vector2 values to CinemachineFreeLook
 *      
 * So that when dragging other places on screen (such as the MoveBtn) won't trigger LookBtn
 */

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class LookBtnProcessor : InputProcessor<Vector2>
{
    #if UNITY_EDITOR
    static LookBtnProcessor()
    {
        Initialize();
    }
    #endif

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        InputSystem.RegisterProcessor<LookBtnProcessor>();
    }


    public override Vector2 Process(Vector2 value, InputControl control)
    {
        if (InputManager.Instance.IsDraggingLookBtn)
        {
            //Debug.Log(value);
            return value;
        }
        else
        {
            return new Vector2(0, 0);
        }
    }
}
