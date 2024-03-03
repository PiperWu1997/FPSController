using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void SetupInput(PlayerInput.OnFootActions onFoot)
    {
        onFoot.Movement.performed += OnMovementPerformed;
        onFoot.Movement.canceled += OnMovementCanceled;
    }

    void Update()
    {
        ProcessMove();
        isGrounded = controller.isGrounded;
    }

    public void ProcessMove()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    void OnMovementPerformed(InputAction.CallbackContext context)
    {
        ProcessMove();
    }

    void OnMovementCanceled(InputAction.CallbackContext context)
    {
        ProcessMove();
    }
}
