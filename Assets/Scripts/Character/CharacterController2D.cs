using System;
using UnityEngine;

namespace MetroidVaniaTools
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class CharacterController2D : Character
    {
        [Header("Commands")]
        [SerializeField]
        private Command JumpCommand;
        [SerializeField]
        private Command DashCommand;

        [Header("Strategies")]
        [SerializeField]
        private Strategy MainAttackStrategy;

        private BoxCollider2D boxCollider;
        private SpriteRenderer spriteRenderer;

        private float acceleration;
        private float deceleration;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            FacingDirection = 1;
        }

        private void Update()
        {
            HandleCoolDowns();
            CheckInput();
            ApplyPhysics();
            SetHorizontalVelocity();
            MoveCharacter();
            CheckOrientation();
            CheckCollisions();
        }

        private void HandleCoolDowns()
        {
            DashCommand.CoolDownTimeLeft -= Time.deltaTime;
        }

        private void CheckInput()
        {
            MoveInput = Input.GetAxisRaw("Horizontal");
            if (Input.GetButtonDown("Jump"))
            {
                JumpCommand.Execute(this);
            }
            if (Input.GetButtonDown("Fire3")) //Fire3 == left shift. Fire2 == Left Alt
            {
                DashCommand.Execute(this);
            }
            if (Input.GetButtonDown("Fire1"))
            {
                MainAttackStrategy.Execute(this);
            }
        }
        private void ApplyPhysics()
        {
            Gravity = Velocity.y > Mathf.Epsilon ? Gravity : Gravity * 2f;
            Velocity.y += IsGrounded ? Gravity * Time.deltaTime : 0f;
            acceleration = IsGrounded ? WalkAcceleration : AirAcceleration;
            deceleration = IsGrounded ? GroundDeceleration : float.Epsilon;
        }

        private void SetHorizontalVelocity()
        {
            Velocity.x = Math.Abs(MoveInput) > float.Epsilon ?
                Mathf.MoveTowards(Velocity.x, Speed * MoveInput, acceleration * Time.deltaTime) :
                Mathf.MoveTowards(Velocity.x, 0f, deceleration * Time.deltaTime);
        }

        private void MoveCharacter()
        {
            transform.Translate(Velocity * Time.deltaTime);
        }

        private void CheckOrientation()
        {
            FacingDirection = Math.Abs(MoveInput) > float.Epsilon ? (int)MoveInput : FacingDirection;
            if (Velocity.x > Mathf.Epsilon && FacingDirection < Mathf.Epsilon)
            {
                spriteRenderer.flipX = true;
            }
            else if (Velocity.x < Mathf.Epsilon && FacingDirection > Mathf.Epsilon)
            {
                spriteRenderer.flipX = false;
            }
        }

        //private void WallSlide()
        //{
        //    if (isWallSliding.Value && Input.GetButton("Horizontal") && Input.GetButtonDown("Jump"))
        //    {
        //        velocity.Value.y = Mathf.Sqrt(jumpForce.Value * -gravity.Value);
        //        velocity.Value.x = -1 * facingDirection.Value * wallHorizontalForce.Value;
        //    }
        //    if (isWallSliding.Value)
        //    {
        //        velocity.Value.y += jumpForce.Value * Time.deltaTime;
        //    }
        //}

        private void CheckCollisions()
        {
            IsGrounded = false;
            var hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);
            foreach (var hit in hits)
            {
                if (hit == boxCollider)
                    continue;
                var colliderDistance = hit.Distance(boxCollider);
                if (colliderDistance.isOverlapped)
                {
                    transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
                }
                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && Velocity.y < float.Epsilon)
                {
                    IsGrounded = true;
                }
            }
        }

        
    }
}

