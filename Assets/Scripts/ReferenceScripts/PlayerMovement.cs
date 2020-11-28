﻿using UnityEngine;

namespace MetroidVaniaTools
{
	public class PlayerMovement : MonoBehaviour
	{
		[SerializeField]
		private MovementConfig movementInfo;
		[SerializeField]
		private JumpConfig jump;
		[SerializeField]
		private bool CanDoubleJump;
		[SerializeField]
		private ExtraJumpConfig extraJump;
		[SerializeField]
		private bool CanWallJump;
		[SerializeField]
		private WallSlideConfig wallSlide;
		[SerializeField]
		private DashConfig dashConfig;

		private const int FacingRight = 1;
		private const int FacingLeft = -1;
		private int directionFacing;

		private float runSpeed;
		private float groundDamping;
		private float inAirDamping;
		private float dashTimeLeft;
		private float normalizedHorizontalSpeed = 0;

		private bool isWallSliding;		

		private CharacterController2D _controller;
		private readonly Animator _animator;

		

		private Vector3 _velocity;


		private void Start()
		{
			//_animator = GetComponent<Animator>();
			_controller = GetComponent<CharacterController2D>();

			// listen to some events for illustration purposes
			_controller.onControllerCollidedEvent += onControllerCollider;
			_controller.onTriggerEnterEvent += OnTriggerEnterEvent;
			_controller.onTriggerExitEvent += OnTriggerExitEvent;

			runSpeed = movementInfo.runSpeed;
			groundDamping = movementInfo.groundDamping;
			inAirDamping = movementInfo.inAirDamping;
			dashTimeLeft = dashConfig.dashCooldown;
		}


		#region Event Listeners

		void onControllerCollider(RaycastHit2D hit)
		{
			// bail out on plain old ground hits cause they arent very interesting
			if (hit.normal.y == 1f)
				return;

			// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
			//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
		}


		void OnTriggerEnterEvent(Collider2D col)
		{
			Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
		}

		void OnTriggerExitEvent(Collider2D col)
		{
			Debug.Log("onTriggerExitEvent: " + col.gameObject.name);
		}

		#endregion


		// the Update loop contains a very simple example of moving the character around and controlling the animation
		void Update()
		{
			GetInput();
			GetOrientation();
			GetVertical();
			GetDash();
			ApplyMovement();
		}

		private void GetInput()
        {
			normalizedHorizontalSpeed = Input.GetAxisRaw("Horizontal");

		}

		private void GetOrientation()
        {
			if (_controller.isGrounded)
			{
				_velocity.y = 0;
			}
            if (!_controller.isGrounded && (_controller.isOnLeftWall || _controller.isOnRightWall))
            {
				_velocity.y = 0;
				isWallSliding = true;
            }
            else
            {
				isWallSliding = false;
			}
			DirectionCheck();
		}

		private void GetVertical()
        {
            if (_controller.isGrounded)
            {
				if (Input.GetButtonDown("Jump"))
				{
					_velocity.y = Mathf.Sqrt(2f * jump.jumpHeight * -jump.gravity);
					//_animator.Play(Animator.StringToHash("Jump"));
				}

				if (Input.GetButton("Jump"))
				{
					_velocity.y += Mathf.Sqrt(2f * jump.additionalJumpHeight * -jump.gravity);
					_controller.ignoreOneWayPlatformsThisFrame = true;
				}
				
			}			
			if (isWallSliding && Input.GetButton("Horizontal") && Input.GetButtonDown("Jump"))
            {
				_velocity.y = Mathf.Sqrt(2f * wallSlide.WallJumpForce * -jump.gravity);
				_velocity.x = -1* directionFacing * wallSlide.HorizontalForce;
				
            }
            if (isWallSliding)
            {
                
				_velocity.y += wallSlide.wallTouchGravity * Time.deltaTime;
				
				
			}
            else
            {
				_velocity.y += jump.gravity * Time.deltaTime;
			}
			

		}

		private void GetDash()
        {
			dashTimeLeft -= Time.deltaTime;
            if (dashConfig.canDash && Input.GetKeyDown(dashConfig.dashKey) && dashTimeLeft < 0)
            {
				_velocity.x += directionFacing * 2f * dashConfig.dashDistance;
				dashTimeLeft = dashConfig.dashCooldown;
			}
        }
		
		private void ApplyMovement()
		{
			// apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
			var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
			_velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor);

			_controller.move(_velocity * Time.deltaTime);

			// grab our current _velocity to use as a base for all calculations
			_velocity = _controller.velocity;
		}

		private void DirectionCheck()
        {
			if (normalizedHorizontalSpeed > 0)
			{
				if (transform.localScale.x < 0f)
					transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
				directionFacing = FacingRight;
			}

			if (normalizedHorizontalSpeed < 0)
			{
				if (transform.localScale.x > 0f)
					transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
				directionFacing = FacingLeft;
			}
		}
	}
}
