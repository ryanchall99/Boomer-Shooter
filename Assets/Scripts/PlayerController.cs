using System;
using Unity.Cinemachine;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Multipliers")]
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float lookSensitivity = 1f;

    [Header("Weapon")]
    [SerializeField] WeaponSO activeWeapon;

    [Header("Camera")]
    [SerializeField] GameObject CinemachineCameraTarget;

    /* --- WEAPON --- */
    Weapon m_CurrentWeapon;

    /* --- INPUT ACTIONS --- */
    InputActions_Player m_InputActions;
    InputAction m_MoveAction;
    InputAction m_LookAction;
    InputAction m_FireAction;

    /* --- PLAYER MOVEMENT --- */
    CharacterController m_CharacterController;  
    private float xRotation = 0f;

    private void OnEnable() 
    {
        if (m_InputActions == null)
        {
            m_InputActions = new InputActions_Player();
        }

        m_InputActions.Player.Enable();
    }

    private void OnDisable() 
    {
        m_InputActions.Player.Disable();    
    }

    private void Start() 
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_CurrentWeapon = GetComponentInChildren<Weapon>();

        m_MoveAction = m_InputActions.Player.Move;    
        m_LookAction = m_InputActions.Player.Look;
        m_FireAction = m_InputActions.Player.Fire;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() 
    {
        HandleMovement();
        HandleLook();
        HandleShoot();
    }

    private void HandleMovement()
    {
        Vector2 move = m_MoveAction.ReadValue<Vector2>();
     
        Vector3 moveDirection = transform.right * move.x + transform.forward * move.y;

        m_CharacterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void HandleLook()
    {
        Vector2 look = m_LookAction.ReadValue<Vector2>();

        float mouseX = look.x * lookSensitivity * Time.deltaTime;
        float mouseY = look.y * lookSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleShoot()
    {
        if (m_FireAction.WasPressedThisFrame())
        {
            m_CurrentWeapon.Shoot(activeWeapon);
        }
    }

}
