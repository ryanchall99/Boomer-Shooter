using System;
using Unity.Cinemachine;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Multipliers")]
    [SerializeField] float MoveSpeed = 1f;
    [SerializeField] float LookSensitivity = 1f;

    [Header("Weapon")]
    [SerializeField] WeaponSO ActiveWeapon;

    [Header("Camera")]
    [SerializeField] GameObject CinemachineCameraTarget;

    /* --- WEAPON --- */
    Weapon _CurrentWeapon;

    /* --- INPUT ACTIONS --- */
    InputActions_Player _InputActions;
    InputAction _MoveAction;
    InputAction _LookAction;
    InputAction _FireAction;

    /* --- PLAYER MOVEMENT --- */
    CharacterController _CharacterController;  
    private float xRotation = 0f;

    private void OnEnable() 
    {
        if (_InputActions == null)
        {
            _InputActions = new InputActions_Player();
        }

        _InputActions.Player.Enable();
    }

    private void OnDisable() 
    {
        _InputActions.Player.Disable();    
    }

    private void Start() 
    {
        _CharacterController = GetComponent<CharacterController>();
        _CurrentWeapon = GetComponentInChildren<Weapon>();

        _MoveAction = _InputActions.Player.Move;    
        _LookAction = _InputActions.Player.Look;
        _FireAction = _InputActions.Player.Fire;

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
        Vector2 move = _MoveAction.ReadValue<Vector2>();
     
        Vector3 moveDirection = transform.right * move.x + transform.forward * move.y;

        _CharacterController.Move(moveDirection * MoveSpeed * Time.deltaTime);
    }

    private void HandleLook()
    {
        Vector2 look = _LookAction.ReadValue<Vector2>();

        float mouseX = look.x * LookSensitivity * Time.deltaTime;
        float mouseY = look.y * LookSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleShoot()
    {
        if (_FireAction.WasPressedThisFrame())
        {
            _CurrentWeapon.Shoot(ActiveWeapon);
        }
    }

}
