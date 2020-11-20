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
        [SerializeField]
        protected float gravity;
        [SerializeField]
        protected float fallMultipler;
        public LayerMask collisionLayer;

        public bool isJumping;

        public float jumpPressedRemember;
        public float groundedRemember;

        // Update is called once per frame
        void Update()
        {
            CheckForJump();
        }

        protected virtual void FixedUpdate()
        {

            GroundCheck();            
            ModifyPhysics();
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
                Jump();
                jumpPressedRemember = 0;
                groundedRemember = 0;
            }
                
        }

        protected virtual void Jump()
        {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        protected virtual void ModifyPhysics()  //TODO: Refactor into character script. 
        {
            if (!character.isGrounded)
            {
                rb.gravityScale = gravity;
                if (rb.velocity.y < 0 )
                {
                    rb.gravityScale = gravity * fallMultipler;
                }
                else if (rb.velocity.y > 0 && !input.JumpHeld())
                {
                    rb.gravityScale = gravity * (fallMultipler / 2);
                }
            }
            
        }


        protected virtual void GroundCheck()
        {
            if (CollisionCheck(Vector2.down, distanceToCollider, collisionLayer) && !isJumping)
            {
                anim.SetBool("Grounded", true);
                character.isGrounded = true;
            }
            else
            {
                anim.SetBool("Grounded", false);
                character.isGrounded = false;
            }
            anim.SetFloat("VerticalSpeed", rb.velocity.y);
        }
    }
}
