using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

/*
 * This script controls the player movement and other physics such as speed and gravity
 */

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Managers and controllers
    [SerializeField] private PlayerManager playerManager;
    private Player currentPlayer;


    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 20f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 4f;
    // move in direction of cam
    private Transform cameraMain;

    //for rotating player when rotate camera
    [SerializeField] private Transform gunChildObj;
    [SerializeField] private Transform playerModelObj;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (playerManager == null)
        {
            playerManager = Object.FindObjectOfType<PlayerManager>();
            if(playerManager == null)
            {
                Debug.LogError("playerManager still null");
            }
        }
    }

    private void Start()
    {
        currentPlayer = playerManager.GetCurrentPlayer();
        if (currentPlayer == null)
        {
            Debug.LogError("currentPlayer is null");
        }

        if (characterController == null)
        {
            Debug.LogError("characterController is nulL");
        }

        if(InputManager.Instance == null)
        {
            Debug.LogError("InputManager.Instance is null. Check InputManager before proceeding.");
        }
        else
        {
            // subscribe to fire/reload action trigger so I don't have to use disgusting if statements
            InputManager.Instance.playerInput.PlayerAction.SingleFire.started += callBackContext => FireTrigger(callBackContext);
            InputManager.Instance.playerInput.PlayerAction.HoldFire.started += callBackContext => HoldFireTrigger(callBackContext);
            InputManager.Instance.playerInput.PlayerAction.Reload.started += callBackContext => ReloadTrigger(callBackContext);
        }

        cameraMain = Camera.main.transform;
        playerModelObj = currentPlayer.transform;
        //Debug.Log($"playerObj name: {playerModelObj.gameObject.name}");

        // DOING: add get gunChildObj code without using drag drop

        if (gunChildObj == null)
        {
            Debug.LogError("gunChildObj is null!");
        }
        if(playerModelObj == null)
        {
            Debug.LogError("playerModelObj is null!");
        }

        // TODO: automatic assign follow/look object at for cinemachine
    }

    void Update()
    {
        #region Player movement and camera control
        // Modified from Unity Manual Move code
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput = InputManager.Instance.playerInput.PlayerAction.Move.ReadValue<Vector2>();
        // move in direction of cam
        Vector3 move = (cameraMain.forward * movementInput.y + cameraMain.right * movementInput.x); 
        move.y = 0f;
        characterController.Move(move * Time.deltaTime * playerSpeed); 

        // Gravity physics
        if (InputManager.Instance.playerInput.PlayerAction.Jump.triggered && groundedPlayer) 
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        // Rotate cam when rotating player
        // FIXME: when draggin look, move stick becomes hard to control?
        if (movementInput != Vector2.zero){
            Quaternion newRotation = Quaternion.Euler(new Vector3(gunChildObj.localEulerAngles.x, 
                                                                    cameraMain.localEulerAngles.y,
                                                                    gunChildObj.localEulerAngles.z));
            playerModelObj.rotation = Quaternion.Lerp(playerModelObj.rotation, newRotation, Time.deltaTime * rotationSpeed);
            gunChildObj.rotation = Quaternion.Lerp(gunChildObj.rotation, newRotation, Time.deltaTime * rotationSpeed);
        }
        #endregion
    }


    #region Weapon fire and reload
    // FIXLater: button hit same place wont fire again! 
    void FireTrigger(InputAction.CallbackContext context)
    {
        //Debug.Log($"Action: {context.action}");
        //Debug.Log($"Ammo: {currentPlayer.GetCurrentWeapon().CurrentAmmo}, {currentPlayer.GetCurrentWeapon().TotalAmmoRemain}");
        currentPlayer.GetCurrentWeapon().FireOne();
        //AudioManager.Instance.PlayGunSound(currentPlayer.GetCurrentWeapon()); 
        UIController.Instance.UpdateAmmoText();
    }

    void ReloadTrigger(InputAction.CallbackContext context)
    {
        //Debug.Log($"Action: {context.action}");
        //Debug.Log($"Ammo: {currentPlayer.GetCurrentWeapon().CurrentAmmo}, {currentPlayer.GetCurrentWeapon().TotalAmmoRemain}");
        currentPlayer.GetCurrentWeapon().Reload();
        //AudioManager.Instance.PlayGunSound(currentPlayer.GetCurrentWeapon()); 
        UIController.Instance.UpdateAmmoText();
    }

    // TODO: Hold to keep shooting holdFire
    void HoldFireTrigger(InputAction.CallbackContext context)
    {
    }
    #endregion



    #region Weapon switch
    // TODO: switch weapon feature
    void SwitchUpWeapon()
    {

    }

    void SwitchDownWeapon()
    {

    }
    #endregion



}