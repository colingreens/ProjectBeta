using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class Movement : MonoBehaviour, IMovement
    {
        [SerializeField]
        private PlayerCharacter character;
        [SerializeField]
        private float _movementSpeed;
        [SerializeField]
        private float _movementForceInAir;
        [SerializeField]
        private float _wallSlideSpeed;

        private float inputDirection;

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
            inputDirection = Input.GetAxisRaw("Horizontal");
        }

        private void CheckMovementDirection()
        {
            if (character.isFacingLeft && inputDirection > 0)
            {
                character.Flip();
            }
            else if (!character.isFacingLeft && inputDirection < 0)
            {
                character.Flip();
            }

            if (character.rb.velocity.x != 0)
            {
                character.isWalking = true;
            }
            else
            {
                character.isWalking = false;
            }
        }

        private void ApplyMovement()
        {
            
            if (character.collisionController.GroundCheck())
            {
                character.rb.velocity = new Vector2(_movementSpeed * inputDirection, character.rb.velocity.y);
            }
            else if (!character.collisionController.GroundCheck() && !character.isWallSliding && inputDirection != 0)
            {
                Vector2 forceToAdd = new Vector2(_movementForceInAir * inputDirection, 0);
                character.rb.AddForce(forceToAdd);

                if (Mathf.Abs(character.rb.velocity.x) > _movementSpeed)
                {
                    character.rb.velocity = new Vector2(_movementSpeed * inputDirection, character.rb.velocity.y);
                }
            }

            if (character.isWallSliding)
            {
                if (character.rb.velocity.y < -_wallSlideSpeed)
                {
                    character.rb.velocity = new Vector2(character.rb.velocity.x, -_wallSlideSpeed);
                }
            }
        }
    }
}
