using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


/*
 * This script controls camera speed manually via script. might be helpful for future setting page?\
 * FIXME: DO NO USE for now. Camera will stuck on top or bottom. Probably due to rounding? 
 */

[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraLook : MonoBehaviour
{
    [SerializeField] private float lookSpeed = 3;

    [SerializeField] private CinemachineFreeLook cinemachineFreeLook;
    private PlayerInputActions playerInput; // TODO: just instantiate this once. maybe read it from inputManager?

    private void Awake()
    {
        playerInput = new PlayerInputActions();
        cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }


    void Update()
    {
        Vector2 delta = playerInput.PlayerAction.TouchDragLook.ReadValue<Vector2>();

        // NOTE: Changing camera speed via script. might be helpful for future setting page?
        cinemachineFreeLook.m_XAxis.Value += delta.x * lookSpeed * Time.deltaTime; // NOTE: 200 is good for mobile
        cinemachineFreeLook.m_YAxis.Value += delta.y * lookSpeed * Time.deltaTime;

        Debug.Log("x speed: " + cinemachineFreeLook.m_XAxis.Value);
        Debug.Log("y speed: " + cinemachineFreeLook.m_YAxis.Value);

    }
}
