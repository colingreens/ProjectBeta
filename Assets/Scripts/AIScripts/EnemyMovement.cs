using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class EnemyMovement : AIManagers
    {
        [SerializeField]
        protected enum MovementType { Normal }
        [SerializeField]
        protected MovementType type;
        [SerializeField]
        protected bool spawnFacingLeft;
        [SerializeField]
        protected bool turnaroundOnCollision;
        [SerializeField]
        protected bool avoidFalling;
        [SerializeField]
        protected float moveSpeed = 10f;
        [SerializeField]
        protected float maxSpeed = 8f;
        [SerializeField]
        protected float linearDrag = 4f;
        [SerializeField]
        protected float afterJumpLinearDragMultiplier = .5f;
        [SerializeField]
        protected LayerMask collidersToTurnAroundOn;

        private Vector2 direction;
        private float horizontalDirection; //-1 = left 0 = still 1 = right
        private bool spawning;
        

        protected override void Initilization()
        {
            base.Initilization();
            if (spawnFacingLeft)
            {
                enemyCharacter.isFacingLeft = true;
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }
            Invoke("Spawning", .1f);

        }

        protected virtual void FixedUpdate()
        {
            Movement();
            MoveCharacter(horizontalDirection);
            CheckDirection();
        }

        protected virtual void Movement()
        {
            if (!enemyCharacter.isFacingLeft)
            {
                horizontalDirection = 1;
                if (CollisionCheck(Vector2.right, .5f, collidersToTurnAroundOn) && turnaroundOnCollision && !spawning)
                {
                    enemyCharacter.isFacingLeft = true;
                    transform.localScale = new Vector2(transform.localScale.x *-1, transform.localScale.y);
                    horizontalDirection = -1;
                }
            }
            else
            {
                horizontalDirection = -1;
                if (CollisionCheck(Vector2.left, .5f, collidersToTurnAroundOn) && turnaroundOnCollision && !spawning)
                {
                    enemyCharacter.isFacingLeft = false;
                    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                    horizontalDirection = 1;
                }
            }
        }

        protected virtual void MoveCharacter(float horizontal)  
        {
            CheckDirection();
            rb.AddForce(Vector2.right * horizontal * moveSpeed);
            
            //anim.SetFloat("Velocity", Mathf.Abs(rb.velocity.x));
        } 

        protected virtual void CheckDirection()
        {

            if (direction.x > 0)
            {
                if (enemyCharacter.isFacingLeft)
                {
                    enemyCharacter.isFacingLeft = false;
                    Flip();
                }
                if (direction.x > maxSpeed)
                {
                    direction.x = maxSpeed;
                }
            }
            if (direction.x < 0)
            {
                if (!enemyCharacter.isFacingLeft)
                {
                    enemyCharacter.isFacingLeft = true;
                    Flip();
                }
                if (direction.x < -maxSpeed)
                {
                    direction.x = -maxSpeed;

                }
            }
        }
        protected virtual void ModifyPhysics()
        {
            bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

            if (enemyCharacter.isGrounded)
            {
                if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
                {
                    rb.drag = linearDrag;
                }
                else
                {
                    rb.drag = 0f;
                }
                rb.gravityScale = 0;
                return;
            }
            //if (!enemyCharacter.isGrounded)
            //{
            //    rb.drag = linearDrag * afterJumpLinearDragMultiplier;
            //    rb.gravityScale = jump.gravity;
            //    if (rb.velocity.y < 0)
            //    {
            //        rb.gravityScale = jump.gravity * jump.fallMultipler;
            //    }
            //    else if (rb.velocity.y > 0 && !input.JumpHeld())
            //    {
            //        rb.gravityScale = jump.gravity * (jump.fallMultipler / 2);
            //    }
            //}
        }

        protected virtual void Spawning()
        {
            spawning = false;
        }
    }
}
