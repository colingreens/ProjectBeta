using UnityEngine;

namespace MetroidVaniaTools
{
	public class PlayerManager : MonoBehaviour
	{
		public Vector3 Velocity;
		public PlayerPosition PositionInfo;

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

		private float runSpeed;
		private float groundDamping;
		private float inAirDamping;
		private bool isWallSliding;		

		private CharacterController2D _controller;

		private void Start()
		{
			_controller = GetComponent<CharacterController2D>();

			// listen to some events for illustration purposes
			_controller.onControllerCollidedEvent += OnControllerCollider;
			_controller.onTriggerEnterEvent += OnTriggerEnterEvent;
			_controller.onTriggerExitEvent += OnTriggerExitEvent;

			runSpeed = movementInfo.runSpeed;
			groundDamping = movementInfo.groundDamping;
			inAirDamping = movementInfo.inAirDamping;
		}


		#region Event Listeners

		void OnControllerCollider(RaycastHit2D hit)
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
			ApplyMovement();
		}

		private void GetInput()
        {
			PositionInfo.horizontalDirection = Input.GetAxisRaw("Horizontal");	
		}

		private void GetOrientation()
        {
			if (_controller.isGrounded)
			{
				Velocity.y = 0;
			}
            if (!_controller.isGrounded && (_controller.isOnLeftWall || _controller.isOnRightWall))
            {
				Velocity.y = 0;
				isWallSliding = true;
            }
            else
            {
				isWallSliding = false;
			}			
		}

		private void GetVertical()
        {
            if (_controller.isGrounded)
            {
				if (Input.GetButtonDown("Jump"))
				{
					Velocity.y = Mathf.Sqrt(2f * jump.jumpHeight * -jump.gravity);
				}

				if (Input.GetButton("Jump"))
				{
					Velocity.y += Mathf.Sqrt(2f * jump.additionalJumpHeight * -jump.gravity);
					_controller.ignoreOneWayPlatformsThisFrame = true;
				}
				
			}			
			if (isWallSliding && Input.GetButton("Horizontal") && Input.GetButtonDown("Jump"))
            {
				Velocity.y = Mathf.Sqrt(2f * wallSlide.WallJumpForce * -jump.gravity);
				Velocity.x = -1* PositionInfo.facingPosition * wallSlide.HorizontalForce;
            }
            if (isWallSliding)
            {                
				Velocity.y += wallSlide.wallTouchGravity * Time.deltaTime;	
			}
            else
            {
				Velocity.y += jump.gravity * Time.deltaTime;
			}
			

		}
		
		private void ApplyMovement()
		{
			var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping;
			Velocity.x = Mathf.Lerp(Velocity.x, PositionInfo.horizontalDirection * runSpeed, Time.deltaTime * smoothedMovementFactor);

			_controller.move(Velocity * Time.deltaTime);

			// grab our current _velocity to use as a base for all calculations
			Velocity = _controller.velocity;
		}

		private void GeneralCoolDown(bool isCooledDown)
		{
			isCooledDown = true;
		}
	}
}
