using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * First version test code for movement and physics (Using PC mouse and keyboard) but newer working code are moved to PlayerController
 * NOTE: This is not compatible with the new touchscreen input action as below code uses the old input system
 */

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;                                   // to move with the angle/direction of camera

    // physics and gravity var
    private float speed = 6f;
    private float gravity = -9.81f;
    private float jumpHeight = 1.5f;
    Vector3 velocity;
    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // Smoothe the turning 
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    void Update()
    {
        // horizontal movement + camera
        float hori = 0;  //Input.GetAxisRaw("Horizontal");
        float verti = 0;  //Input.GetAxisRaw("Vertical");

        // jump control
        isGrounded = controller.isGrounded; // Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // -2f;
        }
        // FIXME: need to not allow changing direction after jump
        if ((Input.GetButtonDown("Jump") || Input.GetButton("Jump")) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -1.5f * gravity);
        }

        // gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        Vector3 direction = new Vector3(hori, 0f, verti).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // added cam angel so it will follow camera angle
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // rotate obj with smooth angle
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

    }
}
