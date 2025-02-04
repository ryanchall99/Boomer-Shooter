using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Multipliers")]
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float lookSensitivity = 1f;

    /* --- INPUT ACTIONS --- */
    InputActions_Player m_InputActions;
    InputAction m_MoveAction;
    InputAction m_LookAction;

    /* --- PLAYER MOVEMENT --- */
    CharacterController m_CharacterController;  
    private float xRotation, yRotation = 0f;

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

        m_MoveAction = m_InputActions.Player.Move;    
        m_LookAction = m_InputActions.Player.Look;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() 
    {
        HandleMovement();
        HandleLook();
    }

    private void HandleMovement()
    {
        Vector2 move = m_MoveAction.ReadValue<Vector2>();

        Vector3 moveDirection = new Vector3(move.x, 0, move.y);
        
        if (move != Vector2.zero)
        {
            moveDirection = transform.right * move.x + transform.forward * move.y;
        }

        m_CharacterController.Move(moveDirection * (moveSpeed * Time.deltaTime));
    }

    private void HandleLook()
    {
        Vector2 look = m_LookAction.ReadValue<Vector2>();

        float mouseX = look.x * lookSensitivity * Time.deltaTime;
        float mouseY = look.y * lookSensitivity * Time.deltaTime;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

}
