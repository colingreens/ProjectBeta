using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class PlayerJump : Abilities, IJump
    {
        [SerializeField]
        private int amountOfJumps = 1;
        [SerializeField]
        private float jumpForce = 16.0f;
        [SerializeField]
        private float variableJumpHeightMultiplier = 0.5f;
        [SerializeField]
        private float wallHopForce;
        [SerializeField]
        private float wallJumpForce;

        private int amountOfJumpsLeft;

        public Vector2 wallHopDirection;
        public Vector2 wallJumpDirection;

        // Start is called before the first frame update
        private void Start()
        {
            amountOfJumpsLeft = amountOfJumps;
            wallHopDirection.Normalize();
            wallJumpDirection.Normalize();
        }

        // Update is called once per frame
        private void Update()
        {
            CheckInput();
            CheckIfCanJump();
        }

        private void FixedUpdate()
        {
            Jump();
        }

        private void CheckInput()
        {
            if (Input.GetButtonUp("Jump"))
            {
                ExtraJump();
            }
        }

        private void Jump()
        {
            if (input.JumpPressed())
            {
                if (canJump && !isWallSliding)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    amountOfJumpsLeft--;
                }
                else if (isWallSliding && horizontalInputDirection == 0 && canJump) //Wall hop
                {
                    isWallSliding = false;
                    amountOfJumpsLeft--;
                    Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
                    rb.AddForce(forceToAdd, ForceMode2D.Impulse);
                }
                else if ((isWallSliding || isTouchingWall) && horizontalInputDirection != 0 && canJump)
                {
                    isWallSliding = false;
                    amountOfJumpsLeft--;
                    Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * horizontalInputDirection, wallJumpForce * wallJumpDirection.y);
                    rb.AddForce(forceToAdd, ForceMode2D.Impulse);
                }
            }
            
        }

        private void ExtraJump()
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }



        private void CheckIfCanJump()
        {
            if ((collisionController.GroundCheck() && rb.velocity.y <= .4) || isWallSliding)
            {
                amountOfJumpsLeft = amountOfJumps;
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
    }
}
