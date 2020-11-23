using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class JumpAlt : Abilities
    {
        [SerializeField]
        protected float jumpForce;
        [SerializeField]
        protected float distanceToCollider;
        [SerializeField]
        protected float jumpPressedBufferTime;
        [SerializeField]
        protected float groundedBufferTime;
       
        
        public LayerMask collisionLayer;
        public float gravity; //used in modifyphysics in horizontal movement script
        public float fallMultipler; //used in modifyphysics in horizontal movement script

        protected float jumpPressedRemember;
        protected float groundedRemember;

        // Update is called once per frame
        void Update()
        {
            CheckForJump();
        }

        protected virtual void FixedUpdate()
        {
            GroundCheck();
            WallCheck();
        }

        protected virtual void CheckForJump()
        {
            groundedRemember -= Time.deltaTime;
            if (character.isGrounded)
            {
                groundedRemember = groundedBufferTime;
            }         

            jumpPressedRemember -= Time.deltaTime;
            if (input.JumpPressed())
            {
                jumpPressedRemember = jumpPressedBufferTime;
            }

            if (jumpPressedRemember > 0 && groundedRemember > 0)
            {
                if (currentPlatform != null && currentPlatform.GetComponent<OneWayPlatform>() && input.DownHeld())
                {
                    character.isJumpingThroughPlatform = true;
                    JumpDown();
                    Invoke("NotJumpingThroughPlatform", .1f);
                    return;
                }
                Jump();
                jumpPressedRemember = 0;
                groundedRemember = 0;
            }
            if (jumpPressedRemember > 0 && character.isWallSliding)
            {
                WallJump();
                jumpPressedRemember = 0;
            }
                
        }

        protected virtual void Jump()
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            character.isJumping = true;
        }

        protected virtual void WallJump()
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            if (character.isFacingLeft)
            {
                rb.AddForce(Vector2.right * jumpForce, ForceMode2D.Impulse);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            if (!character.isFacingLeft)
            {
                rb.AddForce(Vector2.left * jumpForce, ForceMode2D.Impulse);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

        }

        protected virtual void JumpDown()
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
            character.isJumping = true;
        }


        protected virtual void GroundCheck()
        {
            if (CollisionCheck(Vector2.down, distanceToCollider, collisionLayer) && !isJumping)
            {
                if (currentPlatform.GetComponent<MoveablePlatform>())
                {
                    transform.parent = currentPlatform.transform;
                }
                anim.SetBool("Grounded", true);
                character.isGrounded = true;
                character.isJumping = false;
            }
            else
            {
                transform.parent = null;
                anim.SetBool("Grounded", false);
                character.isGrounded = false;
            }
            anim.SetFloat("VerticalSpeed", rb.velocity.y);
        }

        protected virtual void WallCheck()
        {
            if (CollisionCheck(Vector2.left, distanceToCollider, collisionLayer) ||
                CollisionCheck(Vector2.right, distanceToCollider, collisionLayer))
            {
                character.isWallSliding = true;
            }
            else
            {
                character.isWallSliding = false;
            }
        }

        protected virtual void NotJumpingThroughPlatform()
        {
            character.isJumpingThroughPlatform = false;
        }
    }
}
