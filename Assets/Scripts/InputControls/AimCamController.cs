using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This script controls switching or canceling aiming
 */

public class AimCamController : MonoBehaviour
{
    // switch virtual camera when aiming
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private int camPriorityIncrease = 10;

    [SerializeField] Image thirdCrosshair;
    [SerializeField] Image aimCrosshair;


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

        if(thirdCrosshair == null)
        {
            Debug.LogError("thirdCrosshair is nulL");
        }
        if (aimCrosshair == null)
        {
            Debug.LogError("aimCrosshair is nulL");
        }
        // make sure if aim crosshair is now shown at beginning 
        else if (aimCrosshair.IsActive())
        {
            aimCrosshair.gameObject.SetActive(false);
        }

        InputManager.Instance.playerInput.PlayerAction.Aim.started += _ => StartOrStopAim();
    }

    void StartOrStopAim() //InputAction.CallbackContext context)
    {
        if (InputManager.Instance.IsAimPressed)
        {
            //Debug.Log("Stop aim");
            UIController.Instance.UpdateDebug("Stop aim");
            InputManager.Instance.SetAimPressedVal(!InputManager.Instance.IsAimPressed);

            aimCrosshair.gameObject.SetActive(false);
            thirdCrosshair.gameObject.SetActive(true);

            vcam.Priority -= camPriorityIncrease;
        }
        else
        {
            //Debug.Log("Start aim");
            UIController.Instance.UpdateDebug("Start aim");
            InputManager.Instance.SetAimPressedVal(!InputManager.Instance.IsAimPressed);

            aimCrosshair.gameObject.SetActive(true);
            thirdCrosshair.gameObject.SetActive(false);

            vcam.Priority += camPriorityIncrease;
        }
    }

}
