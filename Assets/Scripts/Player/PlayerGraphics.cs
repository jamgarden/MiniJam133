using UnityEngine;

public class PlayerGraphics : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer playerSprite;
    private PlayerMovement movementController;

    private void Awake()
    {
        movementController = GetComponent<PlayerMovement>();
        movementController.OnJump.AddListener(Jump);
    }

    private void Update()
    {
        animator.SetBool("IsWalking", movementController.IsWalking);
        animator.SetBool("IsGrounded", movementController.IsGrounded);
        playerSprite.flipX = movementController.FacingDirection.x > 0;
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
    }
}