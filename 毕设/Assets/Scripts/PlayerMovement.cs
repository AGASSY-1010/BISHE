using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("移动参数")]
    public float moveSpeed = 5f;
    public float acceleration = 15f;
    public float friction = 8f;

    [Header("跳跃参数")]
    public float jumpForce = 8f;
    public float gravityMultiplier = 2f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer = 1;

    [Header("组件引用")]
    private Rigidbody rb; // 使用3D Rigidbody
    private Vector3 movementInput;
    private Vector3 currentVelocity;

    private bool isGrounded;
    private bool jumpRequested;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.freezeRotation = true;
        }

    }

    void Update()
    {
        HandleInput();
        CheckGrounded();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        ApplyMovementWithFriction();
        HandleJump();
        ApplyGravity();
    }

    void HandleInput()
    {
        float horizontal = 0f;
        float vertical = 0f;


        if (Input.GetKey(KeyCode.W)) vertical += 1f;
        
        if (Input.GetKey(KeyCode.S)) vertical -= 1f;
        
        if (Input.GetKey(KeyCode.A)) horizontal -= 1f;

        if (Input.GetKey(KeyCode.D)) horizontal += 1f; 


        movementInput = new Vector3(horizontal, 0f, vertical);

        if (movementInput.magnitude > 1f)
        {
            movementInput.Normalize();
        }
    }

    void CheckGrounded()
    {
        Vector3 rayStart = transform.position;
        float rayLength = groundCheckDistance + 0.2f;

        RaycastHit hit;
        isGrounded = Physics.Raycast(rayStart, Vector3.down, out hit, rayLength, groundLayer);

        Debug.Log($"Player Y: {transform.position.y}, Ground Y: 0, Distance: {transform.position.y}, Grounded: {isGrounded}");
        Debug.DrawRay(rayStart, Vector3.down * rayLength, isGrounded ? Color.green : Color.red);
    }


    void ApplyMovementWithFriction()
    {
        Vector3 targetVelocity = movementInput * moveSpeed;

        if (movementInput.magnitude > 0.1f)
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, friction * Time.fixedDeltaTime);
        }

        Vector3 newVelocity = new Vector3(currentVelocity.x, rb.velocity.y, currentVelocity.z);
        rb.velocity = newVelocity;
    }

    void HandleJump()
    {
        if (jumpRequested && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);

            jumpRequested = false;

            isGrounded = false;
        }
    }

    void ApplyGravity()
    {
        if (!isGrounded)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (gravityMultiplier - 1) * Time.fixedDeltaTime;
        }
    }
}
