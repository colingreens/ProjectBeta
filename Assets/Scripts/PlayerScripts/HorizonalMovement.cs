using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class HorizonalMovement : Abilities
    {
        [SerializeField]
        protected float timeTillMaxSpeed;
        [SerializeField]
        protected float maxSpeed;
        [SerializeField]
        protected float sprintMultiplier;
        [SerializeField]
        protected float crouchMultiplier;
        [SerializeField]
        protected float horizontalAcceleration;
        [SerializeField]
        [Range(0, 1)]
        protected float horizontalDampingBasic;
        [SerializeField]
        [Range(0, 1)]
        protected float horizontalDampingWhenStopping;
        [SerializeField]
        [Range(0, 1)]
        protected float horizontalDampingWhenTurning;
        private float horizontalVelocity;

        //private float acceleration;        
        //private float horizontalInput;
        //private float runTime;

        protected override void Initilization()
        {
            base.Initilization();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            MovementPressed();
            SprintingHeld();
        }
        protected virtual void FixedUpdate()
        {
            Movement();
        }

        public virtual bool MovementPressed()
        {
             if (Input.GetAxis("Horizontal") != 0)
            {
                //horizontalInput = Input.GetAxis("Horizontal");
                return true;
            }
            else
                return false;
            
        }
        protected virtual bool SprintingHeld()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                return true;
            }
            else
                return false;

        }      

        protected virtual void Movement()
        {
            horizontalVelocity = rb.velocity.x;
            horizontalVelocity += Input.GetAxisRaw("Horizontal");

            if (MovementPressed())
            {
                anim.SetBool("Moving", true);
                CheckDirection();
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
                {                    
                    horizontalVelocity *= Mathf.Pow(1f - horizontalDampingWhenStopping, Time.deltaTime * 10f);                    
                }
                else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(horizontalVelocity))
                {                    
                    horizontalVelocity *= Mathf.Pow(1f - horizontalDampingWhenTurning, Time.deltaTime * 10f);                   
                }
                else
                {
                    horizontalVelocity *= Mathf.Pow(1f - horizontalDampingBasic, Time.deltaTime * 10f);
                }

            }
            else
            {
                anim.SetBool("Moving", false);
                horizontalVelocity = 0;
            }
            SpeedMultiplier();
            anim.SetFloat("CurrentSpeed", horizontalVelocity);

            rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);


            //if (MovementPressed())
            //{
            //    anim.SetBool("Moving", true);
            //    acceleration = maxSpeed / timeTillMaxSpeed;
            //    runTime += Time.deltaTime;
            //    horizontalVelocity = horizontalInput * acceleration * runTime;
            //    CheckDirection();                
            //}
            //else
            //{
            //    anim.SetBool("Moving", false);
            //    acceleration = 0;
            //    runTime = 0;
            //    horizontalVelocity = 0;
            //}
            //SpeedMultiplier();
            //anim.SetFloat("CurrentSpeed", horizontalVelocity);

            //rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
        }

        protected virtual void CheckDirection()
        {

            if (horizontalVelocity > 0)
            {
                if (character.isFacingLeft)
                {
                    character.isFacingLeft = false;
                    Flip();
                }
                if (horizontalVelocity > maxSpeed)
                {
                    horizontalVelocity = maxSpeed;
                }
            }
            if (horizontalVelocity < 0)
            {
                if (!character.isFacingLeft)
                {
                    character.isFacingLeft = true;
                    Flip();
                }
                if (horizontalVelocity < -maxSpeed)
                {
                    horizontalVelocity = -maxSpeed;
                }
            }
            
        }

        protected virtual void SpeedMultiplier()
        {
            if (SprintingHeld())
            {
                horizontalVelocity *= sprintMultiplier;
            }
            if (character.isCrouching)
            {
                horizontalVelocity *= crouchMultiplier;
            }
            if (!character.isFacingLeft && CollisionCheck(Vector2.right, .02f, jump.collisionLayer) ||
                character.isFacingLeft && CollisionCheck(Vector2.left, .02f, jump.collisionLayer))
            {
                horizontalVelocity = .01f;
            }
        }
    }
}

