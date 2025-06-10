using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float acceleration = 15f;
    public float deceleration = 20f;
    public float velocityPower = 0.9f;

    [Header("Jump Settings")]
    public float jumpForce = 15f;
    public float jumpCutMultiplier = 0.5f;
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.1f;
    [HideInInspector] public bool suppressJumpCut = false;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private float moveInput;
    private bool isJumpPressed;
    private bool isJumpHeld;

    private bool isGrounded;
    private float lastTimeGrounded;
    private float lastTimeJumpPressed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        // Initialize camera to follow player movement
        CameraController camFollow = Camera.main.GetComponent<CameraController>();
        if (camFollow != null)
        {
            camFollow.target = transform;
        }
    }

    void Update()
    {
        // --- Input ---
        moveInput = Input.GetAxisRaw("Horizontal");
        isJumpPressed = Input.GetButtonDown("Jump");
        isJumpHeld = Input.GetButton("Jump");

        // --- Timers ---
        if (isJumpPressed) lastTimeJumpPressed = Time.time;
        if (CheckGrounded()) lastTimeGrounded = Time.time;

        // --- Jump ---
        if (CanJump())
        {
            Jump();
        }

        // --- Variable Jump Height ---
        if (!isJumpHeld && rb.velocity.y > 0f && !suppressJumpCut)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutMultiplier);
        }

        // --- Walk Animation ---
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    void FixedUpdate()
    {
        // --- Horizontal Movement ---
        float targetSpeed = moveInput * moveSpeed;
        float speedDiff = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velocityPower) * Mathf.Sign(speedDiff);
        rb.AddForce(Vector2.right * movement);

        // --- Flip Player Sprite Based on Direction ---
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        lastTimeJumpPressed = 0;
        lastTimeGrounded = 0;
    }

    bool CanJump()
    {
        return Time.time - lastTimeGrounded <= coyoteTime &&
               Time.time - lastTimeJumpPressed <= jumpBufferTime;
    }

    bool CheckGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public bool IsGrounded()
    {
        return CheckGrounded();
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}