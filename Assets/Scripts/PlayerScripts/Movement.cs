using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class Movement : Abilities, IMovement
    {
        [SerializeField]
        private float _movementSpeed;
        [SerializeField]
        private float _movementForceInAir;
        [SerializeField]
        private float _wallSlideSpeed;

        // Update is called once per frame
        protected virtual void Update()
        {
            CheckInput();
            CheckMovementDirection();
        }

        private void FixedUpdate()
        {
            ApplyMovement();
        }

        private void CheckInput()
        {
            horizontalInputDirection = Input.GetAxisRaw("Horizontal");
        }

        private void CheckMovementDirection()
        {
            if (isFacingLeft && horizontalInputDirection > 0)
            {
                Flip();
            }
            else if (!isFacingLeft && horizontalInputDirection < 0)
            {
                Flip();
            }

            if (rb.velocity.x != 0)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }
        }

        private void ApplyMovement()
        {

            if (isGrounded)
            {
                rb.velocity = new Vector2(_movementSpeed * horizontalInputDirection, rb.velocity.y);
            }
            else if (!isGrounded && !isWallSliding && horizontalInputDirection != 0)
            {
                Vector2 forceToAdd = new Vector2(_movementForceInAir * horizontalInputDirection, 0);
                rb.AddForce(forceToAdd);

                if (Mathf.Abs(rb.velocity.x) > _movementSpeed)
                {
                    rb.velocity = new Vector2(_movementSpeed * horizontalInputDirection, rb.velocity.y);
                }
            }            

            if (isWallSliding)
            {
                if (rb.velocity.y < -_wallSlideSpeed)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -_wallSlideSpeed);
                }
            }
        }
    }
}
