using System;
using Unity.Cinemachine;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Multipliers")]
    [SerializeField] float WalkSpeed = 1f;
    [SerializeField] float SprintSpeed = 5f;
    [SerializeField] float LookSensitivity = 1f;

    [Header("Camera")]
    [SerializeField] GameObject CinemachineCameraTarget;

    /* --- WEAPON --- */
    ActiveWeapon _activeWeapon;

    /* --- INPUT ACTIONS --- */
    InputActions_Player _inputActions;
    InputAction _moveAction;
    InputAction _lookAction;
    InputAction _sprintAction;

    /* --- PLAYER MOVEMENT --- */
    CharacterController _characterController;  
    private float xRotation = 0f;
    private float _movementSpeed;

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
        _activeWeapon = GetComponentInChildren<ActiveWeapon>();

        _moveAction = _inputActions.Player.Move;    
        _lookAction = _inputActions.Player.Look;
        _sprintAction = _inputActions.Player.Sprint;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() 
    {
        HandleMovement();
        HandleLook();
    }

    public InputActions_Player GetInput()
    {
        return _inputActions;
    }

    public float GetLookSensitivity()
    {
        return LookSensitivity;
    }

    public void ChangeLookSensitivity(float amount)
    {
        LookSensitivity = amount;
    }

    private void HandleMovement()
    {
        Vector2 move = _moveAction.ReadValue<Vector2>();
     
        Vector3 moveDirection = transform.right * move.x + transform.forward * move.y;

        _movementSpeed = _sprintAction.IsPressed() ? SprintSpeed : WalkSpeed;

        _characterController.Move(moveDirection * _movementSpeed * Time.deltaTime);
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
}
