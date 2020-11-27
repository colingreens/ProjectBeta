using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class PlayerJump : MonoBehaviour, IJump
    {
        [SerializeField]
        private PlayerCharacter character;

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
            if (character.input.JumpPressed())
            {
                if (character.canJump && !character.isWallSliding)
                {
                    character.rb.velocity = new Vector2(character.rb.velocity.x, jumpForce);
                    amountOfJumpsLeft--;
                }
                else if (character.isWallSliding && character.horizontalInputDirection == 0 && character.canJump) //Wall hop
                {
                    character.isWallSliding = false;
                    amountOfJumpsLeft--;
                    Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -character.facingDirection, wallHopForce * wallHopDirection.y);
                    character.rb.AddForce(forceToAdd, ForceMode2D.Impulse);
                }
                else if ((character.isWallSliding || character.isTouchingWall) && character.horizontalInputDirection != 0 && character.canJump)
                {
                    character.isWallSliding = false;
                    amountOfJumpsLeft--;
                    Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * character.horizontalInputDirection, wallJumpForce * wallJumpDirection.y);
                    character.rb.AddForce(forceToAdd, ForceMode2D.Impulse);
                }
            }
            
        }

        private void ExtraJump()
        {
            character.rb.velocity = new Vector2(character.rb.velocity.x, character.rb.velocity.y * variableJumpHeightMultiplier);
        }



        private void CheckIfCanJump()
        {
            if ((character.collisionController.GroundCheck() && character.rb.velocity.y <= .4) || character.isWallSliding)
            {
                amountOfJumpsLeft = amountOfJumps;
            }
            if (amountOfJumpsLeft <= 0)
            {
                character.canJump = false;
            }
            else
            {
                character.canJump = true;
            }

        }
    }
}
