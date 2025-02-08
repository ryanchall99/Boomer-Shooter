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
    Weapon _currentWeapon;

    /* --- INPUT ACTIONS --- */
    InputActions_Player _inputActions;
    InputAction _moveAction;
    InputAction _lookAction;
    InputAction _fireAction;

    /* --- PLAYER MOVEMENT --- */
    CharacterController _characterController;  
    private float xRotation = 0f;

    private void OnEnable() 
    {
        if (_inputActions == null)
        {
            _inputActions = new InputActions_Player();
        }

        _inputActions.Player.Enable();
    }

    private void OnDisable() 
    {
        _inputActions.Player.Disable();    
    }

    private void Start() 
    {
        _characterController = GetComponent<CharacterController>();
        _currentWeapon = GetComponentInChildren<Weapon>();

        _moveAction = _inputActions.Player.Move;    
        _lookAction = _inputActions.Player.Look;
        _fireAction = _inputActions.Player.Fire;

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
        Vector2 move = _moveAction.ReadValue<Vector2>();
     
        Vector3 moveDirection = transform.right * move.x + transform.forward * move.y;

        _characterController.Move(moveDirection * MoveSpeed * Time.deltaTime);
    }

    private void HandleLook()
    {
        Vector2 look = _lookAction.ReadValue<Vector2>();

        float mouseX = look.x * LookSensitivity * Time.deltaTime;
        float mouseY = look.y * LookSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleShoot()
    {
        if (_fireAction.WasPressedThisFrame())
        {
            _currentWeapon.Shoot(ActiveWeapon);
        }
    }

}
