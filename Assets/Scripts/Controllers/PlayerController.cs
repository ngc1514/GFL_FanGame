using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

/*
 * This script controls the player movement and other physics such as speed and gravity
 */
[RequireComponent(typeof(CharacterController), typeof(Player))]
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
    [SerializeField] private float playerRotationSpeed = 7f;
    [SerializeField] private float gunRotationSpeed = 10f;

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

        cameraMain = Camera.main.transform;
        playerModelObj = currentPlayer.transform;
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

    private void OnEnable()
    {
        if (InputManager.Instance == null)
        {
            Debug.LogError("InputManager.Instance is null. Check InputManager before proceeding.");
        }
        else
        {
            // subscribe to fire/reload action trigger so I don't have to use disgusting if statements
            InputManager.Instance.playerInput.PlayerAction.HoldFire.started += callBackContext => SingleFireTrigger(); // callBackContext);
            InputManager.Instance.playerInput.PlayerAction.HoldFire.performed += callBackContext => LongFireTrigger(); // callBackContext);
            InputManager.Instance.playerInput.PlayerAction.HoldFire.canceled += callBackContext => HoldFinished(); // callBackContext);

            InputManager.Instance.playerInput.PlayerAction.Reload.started += callBackContext => ReloadTrigger(callBackContext);
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

        // Rotate toward new direction after moving 
        //if (movementInput != Vector2.zero){
        //    Quaternion newRotation = Quaternion.Euler(new Vector3(gunChildObj.localEulerAngles.x, 
        //                                                            cameraMain.localEulerAngles.y,
        //                                                            gunChildObj.localEulerAngles.z));
        //    playerModelObj.rotation = Quaternion.Lerp(playerModelObj.rotation, newRotation, Time.deltaTime * rotationSpeed);
        //    gunChildObj.rotation = Quaternion.Lerp(gunChildObj.rotation, newRotation, Time.deltaTime * rotationSpeed); // FIXME: can be a bit faster
        //}

        // Rotate toward cam direction
        Quaternion newRotation = Quaternion.Euler(new Vector3(gunChildObj.localEulerAngles.x,
                                                                    cameraMain.localEulerAngles.y,
                                                                    gunChildObj.localEulerAngles.z));
        playerModelObj.rotation = Quaternion.Lerp(playerModelObj.rotation, newRotation, Time.deltaTime * playerRotationSpeed);
        // FIXME: gun rotate can be a bit faster?
        // FIXME: gun needs to be parallel with aim cam at all time
        gunChildObj.rotation = Quaternion.Lerp(gunChildObj.rotation, newRotation, Time.deltaTime * gunRotationSpeed);
        #endregion

        // fire rate regulator
        if (currentPlayer.isHoldingFire && currentPlayer.canShootNext)
        {
            StartCoroutine(LimitFireRate());
        }


    }


    #region Weapon control
    void SingleFireTrigger()// InputAction.CallbackContext context)
    {
        currentPlayer.GetCurrentWeapon().FireOne();
        //AudioManager.Instance.PlayGunSound(currentPlayer.GetCurrentWeapon()); 
        UIController.Instance.UpdateAmmoText();
    }

    void LongFireTrigger() //InputAction.CallbackContext context)
    {
        //Debug.Log("Holding");
        currentPlayer.isHoldingFire = true;
        currentPlayer.canShootNext = true;
    }

    void HoldFinished() //InputAction.CallbackContext context)
    {
        //Debug.Log("Hold done");
        currentPlayer.canShootNext = false;
        if (currentPlayer.isHoldingFire)
        {
            currentPlayer.isHoldingFire = false;
        }
    }

    // FIXLater: button hit same place wont fire again! 
    void ReloadTrigger(InputAction.CallbackContext context)
    {
        currentPlayer.GetCurrentWeapon().Reload();
        UIController.Instance.UpdateAmmoText();
    }

    // fire rate controll
    IEnumerator LimitFireRate()
    {
        //Debug.Log("fire rate limit");
        SingleFireTrigger();
        currentPlayer.canShootNext = false;
        yield return new WaitForSeconds(currentPlayer.GetCurrentWeapon().Rpm);
        currentPlayer.canShootNext = true;
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