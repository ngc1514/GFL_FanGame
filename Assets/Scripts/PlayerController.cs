using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool groundedPlayer;
    private Vector3 playerVelocity;
    private Transform cameraMain; // move in direction of cam
    private Transform childPlayer; //for rotating player when rotate camera
    private Transform childGun; //for rotating gun when rotate camera

    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerInputActions playerInput;
    [SerializeField] private PlayerManager playerManager;


    private void Awake()
    {
        playerInput = new PlayerInputActions();
        controller = GetComponent<CharacterController>();

        if (playerManager == null)
        {
            playerManager = new PlayerManager();
        }
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
        cameraMain = Camera.main.transform;
        childPlayer = transform.GetChild(1).transform;
    }

    void Update()
    {
        // Modified Unity Manual Move code
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // old input system
        Vector2 movementInput = playerInput.PlayerAction.Move.ReadValue<Vector2>();
        // Vector3 move = new Vector3(movementInput.x, 0f, movementInput.y);
        Vector3 move = (cameraMain.forward * movementInput.y + cameraMain.right * movementInput.x); // move in direction of cam
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Gravity physics
        if (playerInput.PlayerAction.Jump.triggered && groundedPlayer) //Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Rotate cam when rotating player
        if (movementInput != Vector2.zero){
            Quaternion newRotation = Quaternion.Euler(new Vector3(childPlayer.localEulerAngles.x, cameraMain.localEulerAngles.y, childPlayer.localEulerAngles.z));        
            childPlayer.rotation = Quaternion.Lerp(childPlayer.rotation, newRotation, Time.deltaTime * rotationSpeed);
        }


        // Fire button press
        if(playerInput.PlayerAction.SingleFire.triggered){
            playerManager.FireOrReload();
        }
        // TODO: Hold to keep shooting holdFire



        // Reload button
        if (playerInput.PlayerAction.Reload.triggered)
        {
            playerManager.Reload();
        }

    }

}
