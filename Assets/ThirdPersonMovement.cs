using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;                                   // to move with the direction of camera

    // physics and gravity var
    private float speed = 6f;
    private float gravity = -9.81f;
    private float jumpHeight = 1;
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
        // jump control
        // TODO: find a new way to detect if on ground (raycast?)
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // TODO: make a more natural gravity function
            velocity.y = Mathf.Sqrt(jumpHeight * -1.5f * gravity);
        }

        // gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // FIXME: when on air, dont move in X and Z dir
        // FIXME: isGrounded is never false
        if (isGrounded)
        {
            // horizontal movement + camera
            float hori = Input.GetAxisRaw("Horizontal");
            float verti = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(hori, 0f, verti).normalized;

            if (direction.magnitude >= 0.1f)
            {
                // added cam angel so it will follow camera angle
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

                // rotate obj with smooth angle
                transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                // controller.Move(direction * speed * Time.deltaTime);
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }

        Debug.Log("isGrounded: " + isGrounded);



    }
}
