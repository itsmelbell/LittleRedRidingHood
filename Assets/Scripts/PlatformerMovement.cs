using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;
    bool isFacingRight = true;
    private float moveSpeed = 5f;
    float horizontalMovement;

    public float jumpPower = 10f;
    public int maxJumps = 2;
    int jumpsRemaining;

    //ground checks
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(.05f, .05f);
    public LayerMask groundLayer;
    bool isGrounded = false;

    //gravity settings
    public float baseGravity = 3;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;

    //wall checks
    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(.05f, .05f);
    public LayerMask wallLayer;

    //wall movement
    public float wallSlideSpeed= 2;
    bool isWallSliding;
    bool isWallJumping;
    float wallJumpDir;
    float wallJumpTime = .05f;
    float wallJumpTimer;
    public Vector2 wallJumpPower = new Vector2(5f, 13f);

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update(){
        
        GroundCheck();
        Gravity();
        WallSlide();
        WallJump();

        if(!isWallJumping){
            rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
            flip();
        }

        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetFloat("magnitude", rb.linearVelocity.magnitude);
        animator.SetBool("isWallSliding", isWallSliding);

    }

    private void Gravity(){
        if(rb.linearVelocity.y < 0){
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));

        } else {
            rb.gravityScale = baseGravity;
        }

    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context){
        if (context.performed){
        
            if (wallJumpTimer > 0f){
                isWallJumping = true;
                rb.linearVelocity = new Vector2(wallJumpDir * wallJumpPower.x, wallJumpPower.y);
                wallJumpTimer = 0;
                animator.SetTrigger("jump");

                if (transform.localScale.x != wallJumpDir){
                    isFacingRight = !isFacingRight;
                    Vector3 ls = transform.localScale;
                    ls.x *= -1f;
                    transform.localScale = ls;
                }

                Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f);
                return;
            }

            if (jumpsRemaining > 0){
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                jumpsRemaining--;
                animator.SetTrigger("jump");
            }
        } else if (context.canceled && rb.linearVelocity.y > 0f) {
        //short jump
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            animator.SetTrigger("jump");
        }
    }

    private void WallSlide(){
        if(!isGrounded && WallCheck() && horizontalMovement != 0){
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -wallSlideSpeed));
        } else {
            isWallSliding = false;
        }
    }

    private void WallJump(){
        if(isWallSliding){
            isWallJumping = false;
            wallJumpDir = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;

            CancelInvoke(nameof(CancelWallJump));
        } else if (wallJumpTimer > 0f){
            wallJumpTimer -= Time.deltaTime;
        }
    }

    private void CancelWallJump(){
        isWallJumping = false;
    }

    private void GroundCheck(){
        isGrounded = Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer);
        
        if(isGrounded && rb.linearVelocity.y <= 0.0f){
            jumpsRemaining = maxJumps;
        }
        
    }

    private bool WallCheck(){
        return Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer);
    }

    private void flip(){
        if(isFacingRight && horizontalMovement < 0 ||!isFacingRight && horizontalMovement > 0){
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    public void OnDrawGizmosSelected(){
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);
    }
}


