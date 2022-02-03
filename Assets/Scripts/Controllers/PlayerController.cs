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


    // FIXME: FOR TESTING ONLY for now. get weapon and projectile prefab from ResourceManager
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] public Transform muzzleTransform; // FIXME: get barrel from the spawned rifle
    [SerializeField] public Transform projectileParent;



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

        if(projectileParent == null)
        {
            projectileParent = GameObject.Find("ProjectileParent").transform;
        }
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
        if (playerModelObj == null)
        {
            Debug.LogError("playerModelObj is null!");
        }

        // cache currentWeapon
        currentWeapon = currentPlayer.GetCurrentWeapon();
        if(currentWeapon != null && !currentWeapon.GetType().Equals(typeof(NullWeapon)))
        {
            muzzleTransform = currentWeapon.muzzleTransform;
            projectilePrefab = currentWeapon.projectilePrefab;
        }

        // TODO: automatic assign follow/look object at for cinemachine when spawned player
    }


    void Update()
    {
        #region Player movement and camera control
        // Modified from Unity Manual Move code
        groundedPlayer = characterController.isGrounded; // FIXME: jump not actiavted randomly
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput = InputManager.Instance.playerInput.PlayerAction.Move.ReadValue<Vector2>();
        // move in direction of cam
        Vector3 move = (cameraMain.forward * movementInput.y + cameraMain.right * movementInput.x); 
        move.y = 0f;
        characterController.Move(move * Time.deltaTime * playerSpeed); 

        // Gravity physicsz
        if (InputManager.Instance.playerInput.PlayerAction.Jump.triggered && groundedPlayer) 
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        // Two modes of rotation for Aim and 3rd
        // Aim: Rotate toward cam direction
        if (InputManager.Instance.IsAimPressed)
        {
            Quaternion newRotation = Quaternion.Euler(new Vector3(gunChildObj.localEulerAngles.x,
                                                                        cameraMain.localEulerAngles.y,
                                                                        gunChildObj.localEulerAngles.z));
            playerModelObj.rotation = Quaternion.Lerp(playerModelObj.rotation, newRotation, Time.deltaTime * playerRotationSpeed);
            gunChildObj.rotation = Quaternion.Lerp(gunChildObj.rotation, newRotation, Time.deltaTime * gunRotationSpeed);
        }
        // 3rd person: rotate toward new direction only after moving 
        else
        {
            if (movementInput != Vector2.zero)
            {
                Quaternion newRotation = Quaternion.Euler(new Vector3(gunChildObj.localEulerAngles.x,
                                                                        cameraMain.localEulerAngles.y,
                                                                        gunChildObj.localEulerAngles.z));
                playerModelObj.rotation = Quaternion.Lerp(playerModelObj.rotation, newRotation, Time.deltaTime * playerRotationSpeed);
                gunChildObj.rotation = Quaternion.Lerp(gunChildObj.rotation, newRotation, Time.deltaTime * gunRotationSpeed); 
            }
        }
        #endregion

        // fire rate regulator
        if (currentPlayer.IsHoldingFire && currentPlayer.CanShootNext)
        {
            StartCoroutine(LimitFireRate());
        }

    }


    #region Weapon control
    void SingleFireTrigger()// InputAction.CallbackContext context)
    {
        Weapon curWeapon = currentWeapon; //currentPlayer.GetCurrentWeapon();
        if (curWeapon.Attack())
        {
            AudioManager.Instance.PlayGunSound(curWeapon);
            // TODO: use object pool, organize code structure for GetPrefab() for GetCurrentWeapon()
            RaycastHit hit;

            // FIXME: need to get barrel from weapon
            GameObject bullet = Instantiate(projectilePrefab, 
                                            muzzleTransform.position,
                                            Quaternion.identity,  // FIXME: bullet facing side way
                                            projectileParent);
            ProjectileController projectileController = bullet.GetComponent<ProjectileController>(); 

            // TODO: change infinity to a finite distance for bullet drop
            if (Physics.Raycast(cameraMain.position, cameraMain.forward, out hit, Mathf.Infinity))
            {
                //Debug.Log("Hit");
                projectileController.Target = hit.point;
                projectileController.Hit = true;
            }
            else
            {
                //Debug.Log("Not hit");
                // FIXME: make a var for 25 bulletHitMissDistance for Projectile class
                projectileController.Target = cameraMain.position + cameraMain.forward * 25; // starting from cam pos, and go forward // not hit? shoot it to a striaght random point pointed by camera
                projectileController.Hit = false;
            }
        }
        
        //AudioManager.Instance.PlayGunSound(currentPlayer.GetCurrentWeapon()); 
        UIController.Instance.UpdateAmmoText();
    }

    void LongFireTrigger() //InputAction.CallbackContext context)
    {
        //Debug.Log("Holding");
        currentPlayer.IsHoldingFire = true;
        currentPlayer.CanShootNext = true;
    }

    void HoldFinished() //InputAction.CallbackContext context)
    {
        //Debug.Log("Hold done");
        currentPlayer.CanShootNext = false;
        if (currentPlayer.IsHoldingFire)
        {
            currentPlayer.IsHoldingFire = false;
        }
    }

    // FIXLater: button hit same place wont fire again!
    void ReloadTrigger(InputAction.CallbackContext context)
    {
        if (currentWeapon.IsShootable) //currentPlayer.GetCurrentWeapon().IsShootable)
        {
            currentWeapon.Reload(); // currentPlayer.GetCurrentWeapon().Reload();
            UIController.Instance.UpdateAmmoText();
        }
    }

    // fire rate controll
    IEnumerator LimitFireRate()
    {
        //Debug.Log("fire rate limit");
        SingleFireTrigger();
        currentPlayer.CanShootNext = false;
        yield return new WaitForSeconds(currentWeapon.Rpm); //currentPlayer.GetCurrentWeapon().Rpm);
        currentPlayer.CanShootNext = true;
    }
    #endregion



    #region Weapon switch
    void SwitchUpWeapon()
    {
        // TODO: change currentWeapon variable!!!!!
        // update UI and stuffs
    }

    void SwitchDownWeapon()
    {

    }
    #endregion


    void ThrowWeapon()
    {

    }

    void PickUpWeapon()
    {

    }
}