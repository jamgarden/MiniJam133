using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField] private AnimationCurve groundedAccelerationCurve;
    [SerializeField] private AnimationCurve airAccelerationCurve;
    [SerializeField] private float coyoteTime;
    [SerializeField] private int extraJumps;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private AudioClip jumpSound;

    [Space]
    [SerializeField] private LayerMask groundRayLayerMask;

    private float groundRayLength;
    private float groundRayRadius;
    private bool isGrounded;
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
        groundRayRadius = colliderSize.x * 0.6f;
    }

    private void Update()
    {
        Move();
        Jump();
        SetGrounded();
        SetGravity();

        CountDownCoyoteTime();
    }

    private void CountDownCoyoteTime()
    {
        if (isGrounded == false)
            coyoteTimer -= Time.deltaTime;
    }

    private void SetGravity()
    {
        if (isGrounded)
            gravity = -groundRayHit.normal;
        else
            gravity = Vector2.down;

        gravity *= gravityMultiplier;
    }

    private void Move()
    {
        if (isGrounded)
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

        if (isGrounded)
            moveDirection.x *= groundedAccelerationCurve.Evaluate(accelerationTimer);
        else
            moveDirection.x *= airAccelerationCurve.Evaluate(accelerationTimer);
    }

    private void Jump()
    {
        if (input.Jump)
        {
            if (isGrounded || coyoteTimer > 0)
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
        AudioManager.Instance.PlaySound(jumpSound);
    }

    private void SetGrounded()
    {
        groundRayHit = Physics2D.CircleCast(transform.position, groundRayRadius, Vector2.down, groundRayLength, groundRayLayerMask);
        if (isGrounded == false && groundRayHit)
            OnGrounded();

        isGrounded = groundRayHit;
    }

    private void OnGrounded()
    {
        remainingExtraJumps = extraJumps;
        coyoteTimer = coyoteTime;
    }

    private void FixedUpdate()
    {
        playerRigidbody.AddForce(moveDirection + gravity);
    }
}