using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class HorizonalMovementNoGravity : Abilities, IMovement
    {
        [Header("Horizontal Movement")]
        [SerializeField]
        protected float moveSpeed = 10f;
        [SerializeField]
        protected float maxSpeed = 8f;
        [SerializeField]
        protected float linearDrag = 4f;
        [SerializeField]
        protected float afterJumpLinearDragMultiplier = .5f;

        protected Vector2 direction;

        protected override void Initilization()
        {
            base.Initilization();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            MovementPressed();
        }

        protected virtual void FixedUpdate()
        {
            MoveCharacter(direction.x);
            RemoveFromGrapple();
            //ModifyPhysics();    // pull into a physics manager
        }
        public virtual bool MovementPressed()
        {
            direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Input.GetAxisRaw("Horizontal") != 0)
            {                
                return true;
            }
            else
                return false;
        }

        protected virtual void MoveCharacter(float horizontal)
        {
            if (MovementPressed())
            {
                CheckDirection();
                rb.AddForce(Vector2.right * horizontal * moveSpeed);                
            }
            anim.SetFloat("Velocity", Mathf.Abs(rb.velocity.x));
        }

        protected virtual void RemoveFromGrapple()
        {
            if (grapplingHook.removed)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, Time.deltaTime * 500);
                if (transform.rotation == Quaternion.identity)
                {
                    grapplingHook.removed = false;
                    rb.freezeRotation = true;
                }
            }
        }

        //protected virtual void ModifyPhysics()
        //{
        //    bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        //    if (character.isGrounded)
        //    {
        //        if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
        //        {
        //            rb.drag = linearDrag;
        //        }
        //        else
        //        {
        //            rb.drag = 0f;
        //        }
        //        rb.gravityScale = 0;
        //        return;
        //    }
        //    if (!character.isGrounded && !character.isWallSliding)
        //    {
        //        rb.drag = linearDrag * afterJumpLinearDragMultiplier;
        //        rb.gravityScale = jump.gravity;
        //        if (rb.velocity.y < 0)
        //        {
        //            rb.gravityScale = jump.gravity * jump.fallMultipler;
        //        }
        //        else if (rb.velocity.y > 0 && !input.JumpHeld())
        //        {
        //            rb.gravityScale = jump.gravity * (jump.fallMultipler / 2);
        //        }
        //    }
        //    if (!character.isGrounded && character.isWallSliding)
        //    {
        //        rb.gravityScale = jump.gravity * 0.1f;
        //    }
        //    if (grapplingHook.connected)
        //    {
        //        //rb.drag = linearDrag;
        //        if (CollisionCheck(Vector2.right, .1f, jump.collisionLayer) ||
        //            CollisionCheck(Vector2.left, .1f, jump.collisionLayer) ||
        //            CollisionCheck(Vector2.down, .1f, jump.collisionLayer) ||
        //            CollisionCheck(Vector2.up, .1f, jump.collisionLayer) ||
        //            character.isGrounded)
        //            {
        //                return;
        //            }
                
        //        if (grapplingHook.hookTrail.transform.position.y > grapplingHook.objectConnectedTo.transform.position.y)
        //        {
        //            //possibly slow down here
        //        }
        //        //rb.rotation -= rb.velocity.x;
        //    }

        //}

        protected virtual void CheckDirection()
        {

            if (direction.x > 0)
            {
                if (character.isFacingLeft)
                {
                    character.isFacingLeft = false;
                    Flip();
                }
                if (direction.x > maxSpeed)
                {
                    direction.x = maxSpeed;
                }
            }
            if (direction.x < 0)
            {
                if (!character.isFacingLeft)
                {
                    character.isFacingLeft = true;
                    Flip();
                }
                if (direction.x < -maxSpeed)
                {
                    direction.x = -maxSpeed;

                }
            }
        }
    }
}