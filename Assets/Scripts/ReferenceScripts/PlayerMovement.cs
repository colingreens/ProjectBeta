using UnityEngine;

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
		private WallJumpConfig wallJump;
		[SerializeField]
		private DashConfig dashConfig;

		private const int FacingRight = 1;
		private const int FacingLeft = -1;

		// movement config
		//private float gravity;
		private float runSpeed;
		private float groundDamping;
		private float inAirDamping;
		private float dashTimeLeft;

		[HideInInspector]
		private float normalizedHorizontalSpeed = 0;

		private CharacterController2D _controller;
		private readonly Animator _animator;

		private int directionFacing; //Horizontal Facing Right = 1, Left = -1

		private bool canDash;


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
			canDash = dashConfig.canDash;
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
				_velocity.y = 0;

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

		private void GetVertical()
        {
			// we can only jump whilst grounded
			if (_controller.isGrounded && Input.GetButton("Jump"))
			{
				_velocity.y = Mathf.Sqrt(2f * jump.jumpHeight * -jump.gravity);
				//_animator.Play(Animator.StringToHash("Jump"));
			}
			_velocity.y += jump.gravity * Time.deltaTime;

			if (_controller.isGrounded && Input.GetButton("Jump"))
			{
				_velocity.y *= jump.VariableJumpHeightMultiplier;
				_controller.ignoreOneWayPlatformsThisFrame = true;
			}
		}

		private void GetDash()
        {
			dashTimeLeft -= Time.deltaTime;
            if (canDash && Input.GetKeyDown(dashConfig.dashKey) && dashTimeLeft < 0)
            {
				_velocity.x += directionFacing * 2 * dashConfig.dashDistance;
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
	}
}
