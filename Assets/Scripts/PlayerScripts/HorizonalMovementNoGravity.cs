using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class HorizonalMovementNoGravity : Abilities
    {
        [Header("Horizontal Movement")]
        public float moveSpeed = 10f;
        public float maxSpeed = 8f;
        public float linearDrag = 4f;
        public Vector2 direction;

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
            ModifyPhysics();
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

        }

        protected virtual void ModifyPhysics()
        {
            bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);
            print(direction.x);
            
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
        }

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