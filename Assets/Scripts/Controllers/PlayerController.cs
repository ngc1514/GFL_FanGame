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
    [SerializeField] private float playerSpeed = 20f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private Transform gunChildObj; //for rotating player when rotate camera


    // Managers and controllers
    [SerializeField] private PlayerManager playerManager;
    private CharacterController characterController;


    private Player currentPlayer;
    private Vector3 playerVelocity;
    private Transform cameraMain; // move in direction of cam
    private bool groundedPlayer;


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
            Debug.LogError("CharacterController is nulL");
        }

        if(InputManager.Instance == null)
        {
            Debug.LogError("InputManager Instance is null");
        }
        else
        {
            // subscribe to fire/reload action trigger so I don't have to use disgusting if statements
            InputManager.Instance.playerInput.PlayerAction.SingleFire.started += callBackContext => FireTrigger(callBackContext);
            InputManager.Instance.playerInput.PlayerAction.Reload.started += callBackContext => ReloadTrigger(callBackContext);
        }

        cameraMain = Camera.main.transform;
        //gunChildObj = transform.GetChild(0).transform; 
        if (gunChildObj == null)
        {
            Debug.LogError("Gun child obj null!");
        }
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

        Vector2 movementInput = InputManager.Instance.playerInput.PlayerAction.Move.ReadValue<Vector2>(); //playerInput.PlayerAction.Move.ReadValue<Vector2>();
        
        // move in direction of cam
        Vector3 move = (cameraMain.forward * movementInput.y + cameraMain.right * movementInput.x); 
        move.y = 0;
        // FIXME: FIX WHY PLAYER WONT ROTATE!!!!
        characterController.Move(move * Time.deltaTime * playerSpeed); 

        // Gravity physics
        if (InputManager.Instance.playerInput.PlayerAction.Jump.triggered && groundedPlayer) // playerInput.PlayerAction.Jump.triggered && groundedPlayer) 
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        // Rotate cam when rotating player
        if (movementInput != Vector2.zero){
            Quaternion newRotation = Quaternion.Euler(new Vector3(gunChildObj.localEulerAngles.x, 
                                                                    cameraMain.localEulerAngles.y,
                                                                    gunChildObj.localEulerAngles.z));
            gunChildObj.rotation = Quaternion.Lerp(gunChildObj.rotation, newRotation, Time.deltaTime * rotationSpeed);
        }
        #endregion
    }


    #region Weapon fire and reload
    // FIXME: button hit same place wont fire again! 
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