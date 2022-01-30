using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script controls switching or canceling aiming
 */

public class AimCamController : MonoBehaviour
{
    // switch virtual camera when aiming
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] int camPriorityIncrease = 10;

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>(); // FindObjectOfType<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        if (vcam == null)
        {
            Debug.LogError("Virtual camera is nulL");
        }

        InputManager.Instance.playerInput.PlayerAction.Aim.started += _ => StartOrStopAim();
    }

    void StartOrStopAim() //InputAction.CallbackContext context)
    {
        if (InputManager.Instance.IsAimPressed)
        {
            Debug.Log("Stop aim");
            UIController.Instance.UpdateDebug("Stop aim");

            InputManager.Instance.SetAimPressedVal(!InputManager.Instance.IsAimPressed);
            vcam.Priority -= camPriorityIncrease;
        }
        else
        {
            Debug.Log("Start aim");
            UIController.Instance.UpdateDebug("Start aim");

            InputManager.Instance.SetAimPressedVal(!InputManager.Instance.IsAimPressed);
            vcam.Priority += camPriorityIncrease;
        }
    }

    //void CancelAim() //InputAction.CallbackContext context)
    //{
    //    Debug.Log("cancel aim");
    //    vcam.Priority -= camPriorityIncrease;
    //}

}
