using UnityEngine;

namespace MetroidVaniaTools
{
    public class SimpleMovement : MonoBehaviour, IMovement
    {
        public PlayerBase playerBase;
        public MovementInfo movementInfo;

        private bool isFacingRight = true;
        private bool isWalking;
        private bool isGrounded;
        private bool isTouchingWall;
        private bool isWallSliding;
        private bool canJump;

        private float movementInputDirection;

        private int facingDirection;
        private int amountOfJumpsLeft;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            CheckInput();
            CheckMovementDirection();
        }

        void FixedUpdate()
        {
            ApplyMovement();
            CheckSurroundings();
        }

        private void CheckSurroundings()
        {
            isGrounded = Physics2D.OverlapCircle(movementInfo.groundCheck.position, movementInfo.groundCheckRadius, movementInfo.whatIsGround);

            isTouchingWall = Physics2D.Raycast(movementInfo.wallCheck.position, transform.right, movementInfo.wallCheckDistance, movementInfo.whatIsGround);
        }

        private void CheckIfCanJump()
        {
            if ((isGrounded && playerBase.rigidBody.velocity.y <= 0) || isWallSliding)
            {
                amountOfJumpsLeft = movementInfo.amountOfJumps;
            }

            if (amountOfJumpsLeft <= 0)
            {
                canJump = false;
            }
            else
            {
                canJump = true;
            }

        }

        private void CheckMovementDirection()
        {
            if (isFacingRight && movementInputDirection < 0)
            {
                Flip();
            }
            else if (!isFacingRight && movementInputDirection > 0)
            {
                Flip();
            }

            if (playerBase.rigidBody.velocity.x != 0)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }
        }

        private void UpdateAnimations()
        {
            //anim.SetBool("isWalking", isWalking);
            //anim.SetBool("isGrounded", isGrounded);
            //anim.SetFloat("yVelocity", playerBase.rigidBody.velocity.y);
            //anim.SetBool("isWallSliding", isWallSliding);
        }

        private void CheckInput()
        {
            movementInputDirection = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            if (Input.GetButtonUp("Jump"))
            {
                playerBase.rigidBody.velocity = new Vector2(playerBase.rigidBody.velocity.x, playerBase.rigidBody.velocity.y * movementInfo.variableJumpHeightMultiplier);
            }

        }

        private void Jump()
        {
            if (canJump && !isWallSliding)
            {
                playerBase.rigidBody.velocity = new Vector2(playerBase.rigidBody.velocity.x, movementInfo.jumpForce);
                amountOfJumpsLeft--;
            }
            else if (isWallSliding && movementInputDirection == 0 && canJump) //Wall hop
            {
                isWallSliding = false;
                amountOfJumpsLeft--;
                Vector2 forceToAdd = new Vector2(movementInfo.wallHopForce * movementInfo.wallHopDirection.x * -facingDirection, movementInfo.wallHopForce * movementInfo.wallHopDirection.y);
                playerBase.rigidBody.AddForce(forceToAdd, ForceMode2D.Impulse);
            }
            else if ((isWallSliding || isTouchingWall) && movementInputDirection != 0 && canJump)
            {
                isWallSliding = false;
                amountOfJumpsLeft--;
                Vector2 forceToAdd = new Vector2(movementInfo.wallJumpForce * movementInfo.wallJumpDirection.x * movementInputDirection, movementInfo.wallJumpForce * movementInfo.wallJumpDirection.y);
                playerBase.rigidBody.AddForce(forceToAdd, ForceMode2D.Impulse);
            }
        }

        private void ApplyMovement()
        {

            if (isGrounded)
            {
                playerBase.rigidBody.velocity = new Vector2(movementInfo.movementSpeed * movementInputDirection, playerBase.rigidBody.velocity.y);
            }
            else if (!isGrounded && !isWallSliding && movementInputDirection != 0)
            {
                Vector2 forceToAdd = new Vector2(movementInfo.movementForceInAir * movementInputDirection, 0);
                playerBase.rigidBody.AddForce(forceToAdd);

                if (Mathf.Abs(playerBase.rigidBody.velocity.x) > movementInfo.movementSpeed)
                {
                    playerBase.rigidBody.velocity = new Vector2(movementInfo.movementSpeed * movementInputDirection, playerBase.rigidBody.velocity.y);
                }
            }
            else if (!isGrounded && !isWallSliding && movementInputDirection == 0)
            {
                playerBase.rigidBody.velocity = new Vector2(playerBase.rigidBody.velocity.x * movementInfo.airDragMultiplier, playerBase.rigidBody.velocity.y);
            }

            if (isWallSliding)
            {
                if (playerBase.rigidBody.velocity.y < -movementInfo.wallSlideSpeed)
                {
                    playerBase.rigidBody.velocity = new Vector2(playerBase.rigidBody.velocity.x, -movementInfo.wallSlideSpeed);
                }
            }
        }

        private void Flip()
        {
            if (!isWallSliding)
            {
                facingDirection *= -1;
                isFacingRight = !isFacingRight;
                transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(movementInfo.groundCheck.position, movementInfo.groundCheckRadius);

            Gizmos.DrawLine(movementInfo.wallCheck.position, new Vector3(movementInfo.wallCheck.position.x + movementInfo.wallCheckDistance, movementInfo.wallCheck.position.y, movementInfo.wallCheck.position.z));
        }
    }
}
