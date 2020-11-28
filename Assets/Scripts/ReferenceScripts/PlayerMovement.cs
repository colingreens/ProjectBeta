﻿using UnityEngine;

namespace MetroidVaniaTools
{
	public class PlayerMovement : MonoBehaviour
	{
		[SerializeField]
		private MovementInfo movementInfo;
		[SerializeField]
		private JumpConfig jump;
		[SerializeField]
		private bool CanDoubleJump;
		[SerializeField]
		private ExtraJumpConfig extraJump;
		[SerializeField]
		private bool CanWallJump;
		[SerializeField]
		private WallJumpConfig wallJump;

		// movement config
		//private float gravity;
		private float runSpeed;
		private float groundDamping;
		private float inAirDamping;

		[HideInInspector]
		private float normalizedHorizontalSpeed = 0;

		private CharacterController2D _controller;
		private readonly Animator _animator;
		//private RaycastHit2D _lastControllerColliderHit; what was this used for?
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
			ApplyMovement();
		}

		private void ApplyMovement()
		{
			if (_controller.isGrounded)
				_velocity.y = 0;

			if (Input.GetKey(KeyCode.RightArrow))
			{
				normalizedHorizontalSpeed = 1;
				if (transform.localScale.x < 0f)
					transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

				//if (_controller.isGrounded)
					//_animator.Play(Animator.StringToHash("Run"));
			}
			else if (Input.GetKey(KeyCode.LeftArrow))
			{
				normalizedHorizontalSpeed = -1;
				if (transform.localScale.x > 0f)
					transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

				//if (_controller.isGrounded)
					//_animator.Play(Animator.StringToHash("Run"));
			}
			else
			{
				normalizedHorizontalSpeed = 0;

				//if (_controller.isGrounded)
					//_animator.Play(Animator.StringToHash("Idle"));
			}


			// we can only jump whilst grounded
			if (_controller.isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
			{
				_velocity.y = Mathf.Sqrt(2f * jump.jumpHeight * -jump.gravity);
				//_animator.Play(Animator.StringToHash("Jump"));
			}


			// apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
			var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
			_velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor);

			// apply gravity before moving
			_velocity.y += jump.gravity * Time.deltaTime;

			// if holding down bump up our movement amount and turn off one way platform detection for a frame.
			// this lets us jump down through one way platforms
			if (_controller.isGrounded && Input.GetKey(KeyCode.DownArrow))
			{
				_velocity.y *= 3f;
				_controller.ignoreOneWayPlatformsThisFrame = true;
			}

			_controller.move(_velocity * Time.deltaTime);

			// grab our current _velocity to use as a base for all calculations
			_velocity = _controller.velocity;
		}
	}
}
