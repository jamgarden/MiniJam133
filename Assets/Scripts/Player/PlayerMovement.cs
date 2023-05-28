using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [field: SerializeField] public UnityEvent OnJump { get; private set; }

    public bool IsWalking { get; private set; }
    public Vector2 FacingDirection { get; private set; }
    public bool IsGrounded { get; private set; }

    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField] private AnimationCurve groundedAccelerationCurve;
    [SerializeField] private AnimationCurve airAccelerationCurve;
    [SerializeField] private float coyoteTime;
    [SerializeField] private int extraJumps;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource landingSounds;

    [Space]
    [SerializeField] private LayerMask groundRayLayerMask;

    private float groundRayLength;
    private float groundRayRadius;
    private Vector2 moveDirection;
    private Vector2 gravity;
    private float accelerationTimer;
    private float coyoteTimer;
    private float jumpTimer = 1;
    private int remainingExtraJumps;
    private Rigidbody2D playerRigidbody;
    private IInput input;
    private RaycastHit2D groundRayHit;
    private CapsuleCollider2D capsuleCollider;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        input = GetComponent<IInput>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        Vector3 colliderSize = capsuleCollider.bounds.size;
        groundRayLength = colliderSize.y * 0.4f;
        groundRayRadius = colliderSize.x * 0.5f;
    }

    private void Update()
    {
        Move();
        Jump();
        SetGrounded();
        SetGravity();

        CountDownCoyoteTime();

        IsWalking = IsGrounded && Mathf.Abs(moveDirection.x) > 0;
        if (input.Horizontal > 0)
            FacingDirection = Vector2.right;
        else if (input.Horizontal < 0)
            FacingDirection = Vector2.left;
    }

    private void CountDownCoyoteTime()
    {
        if (IsGrounded == false)
            coyoteTimer -= Time.deltaTime;
    }

    private void SetGravity()
    {
        if (IsGrounded)
            gravity = -groundRayHit.normal;
        else
            gravity = Vector2.down;

        gravity *= gravityMultiplier;
    }

    private void Move()
    {
        if (IsGrounded)
        {
            Vector3 cross = Vector3.Cross(transform.right * input.Horizontal, groundRayHit.normal);
            moveDirection = Vector3.Cross(groundRayHit.normal, cross);
        }
        else
        {
            moveDirection.x = input.Horizontal;
        }

        AddMoveAcceleration();

        moveDirection.x *= moveSpeed;
    }

    private void AddMoveAcceleration()
    {
        if (moveDirection.x == 0)
            accelerationTimer = 0;
        else if (accelerationTimer <= 1)
            accelerationTimer += Time.deltaTime;

        if (IsGrounded)
            moveDirection.x *= groundedAccelerationCurve.Evaluate(accelerationTimer);
        else
            moveDirection.x *= airAccelerationCurve.Evaluate(accelerationTimer);
    }

    private void Jump()
    {
        if (input.Jump)
        {
            if (IsGrounded || coyoteTimer > 0)
            {
                ExecuteJump();
            }
            else if (remainingExtraJumps > 0)
            {
                remainingExtraJumps--;
                ExecuteJump();
            }
        }

        if (jumpTimer <= 1)
        {
            moveDirection.y = jumpCurve.Evaluate(jumpTimer) * jumpForce;
            jumpTimer += Time.deltaTime;
        }
    }

    private void ExecuteJump()
    {
        jumpTimer = 0;
        jumpSound.Play();
        OnJump?.Invoke();
    }

    private void SetGrounded()
    {
        groundRayHit = Physics2D.CircleCast(transform.position, groundRayRadius, Vector2.down, groundRayLength, groundRayLayerMask);
        if (IsGrounded == false && groundRayHit)
            OnGrounded();

        IsGrounded = groundRayHit;
    }

    private void OnGrounded()
    {
        remainingExtraJumps = extraJumps;
        coyoteTimer = coyoteTime;
        landingSounds.Play();
    }

    private void FixedUpdate()
    {
        playerRigidbody.AddForce(moveDirection + gravity);
    }
}