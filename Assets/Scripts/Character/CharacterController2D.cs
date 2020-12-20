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
        private float preJumpPeakGravity;
        private float postJumpPeakGravity;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            IsFacingRight = true;
            MovementDirection = 1;
            preJumpPeakGravity = Gravity * 2f;
            postJumpPeakGravity = Gravity;
        }

        private void Update()
        {
            HandleCoolDowns();
            CheckInput();            
            SetHorizontalVelocity();
            ApplyPhysics();
            MoveCharacter();
            CheckOrientation();
            CheckGround();
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
            //if (Input.GetButtonDown("Fire1"))
            //{
            //    MainAttackStrategy.Execute(this);
            //}
        }
        private void ApplyPhysics()
        {                       
            acceleration = IsGrounded ? WalkAcceleration : AirAcceleration;
            deceleration = IsGrounded ? GroundDeceleration : 0f;
            if (IsGrounded)
            {
                Gravity = 0f;
            }
            Gravity = Velocity.y < 0f ? postJumpPeakGravity : preJumpPeakGravity;
            
            Velocity.y += Gravity * Time.deltaTime;
        }

        private void SetHorizontalVelocity()
        {
            Velocity.x = Math.Abs(MoveInput) > 0f ?
                Mathf.MoveTowards(Velocity.x, Speed * MoveInput, acceleration * Time.deltaTime) :
                Mathf.MoveTowards(Velocity.x, 0f, deceleration * Time.deltaTime);
        }

        private void MoveCharacter()
        {
            transform.Translate(Velocity * Time.deltaTime);
            IsGrounded = false;            
        }

        private void CheckOrientation()
        {
            MovementDirection = MoveInput != 0f ? MoveInput: MovementDirection;
            if (IsFacingRight && MovementDirection < 0f)
            {
                spriteRenderer.flipX = true;
                IsFacingRight = false;
            }
            else if (!IsFacingRight && MovementDirection > 0f)
            {
                spriteRenderer.flipX = false;
                IsFacingRight = true;
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

        private void CheckGround()
        {
            var hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0, GroundLayer);
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
                    Velocity.y = 0;
                }
            }
        }

        
    }
}

