using UnityEngine;

namespace MetroidVaniaTools
{
	[RequireComponent(typeof(CharacterEngine2D))]
	[RequireComponent(typeof(OrientationController))]
	[RequireComponent(typeof(CombatController))]
	[RequireComponent(typeof(HealthController))]
	[RequireComponent(typeof(InputController))]
	public class CharacterController : MonoBehaviour
	{
		[SerializeField]
		private FloatVariable runSpeed;
		[SerializeField]
		private FloatVariable groundDamping;
		[SerializeField]
		private FloatVariable inAirDamping;
		[SerializeField]
		private VelocityVariable velocity;
		[SerializeField]
		private FloatVariable horizontalDirection;
		[SerializeField]
		private FloatVariable gravity;
		[SerializeField]
		private BoolVariable isGrounded;
		[SerializeField]
		private BoolVariable isWallSliding;

		[SerializeField]
		private CharacterEngine2D _controller;

		private void Start()
		{
			// listen to some events for illustration purposes
			_controller.onControllerCollidedEvent += OnControllerCollider;
			_controller.onTriggerEnterEvent += OnTriggerEnterEvent;
			_controller.onTriggerExitEvent += OnTriggerExitEvent;
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
		}

        private void FixedUpdate()
        {
			ApplyMovement();
			SetCharacterState();
		}

        private void GetInput()
        {
			horizontalDirection.Value = Input.GetAxisRaw("Horizontal");	
		}
		
		private void ApplyMovement()
		{
			var smoothedMovementFactor = _controller.isGrounded ? groundDamping.Value : inAirDamping.Value;
			velocity.Value.x = Mathf.Lerp(velocity.Value.x, horizontalDirection.Value * runSpeed.Value, Time.deltaTime * smoothedMovementFactor);
			velocity.Value.y += gravity.Value * Time.deltaTime;
			_controller.move(velocity.Value * Time.deltaTime);
			velocity.Value = _controller.velocity;
		}

		private void SetCharacterState()
        {
			isGrounded.Value = _controller.isGrounded;
			if (_controller.isGrounded)
			{
				velocity.Value.y = 0;
			}
			if (!_controller.isGrounded && (_controller.isOnLeftWall || _controller.isOnRightWall))
			{
				velocity.Value.y = 0;
				isWallSliding.Value = true;
			}
			else
			{
				isWallSliding.Value = false;
			}
		}
	}
}
