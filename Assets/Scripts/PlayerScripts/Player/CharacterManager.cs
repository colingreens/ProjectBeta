using UnityEngine;

namespace MetroidVaniaTools
{
	public class CharacterManager : MonoBehaviour
	{
		[HideInInspector]
		public Vector3 Velocity;

		[SerializeField]
		private FloatVariable horizontalDirection;
		[SerializeField]
		private FloatVariable facingDirection;
		[SerializeField]
		private FloatVariable runSpeed; //= 8f;
		[SerializeField]
		private FloatVariable groundDamping; //= 20f; // how fast do we change direction? higher means faster
		[SerializeField]
		private FloatVariable inAirDamping;
		[SerializeField]
		private BoolVariable isWallSliding;
		[SerializeField]
		private BoolVariable isGrounded;

		[SerializeField]
		private CharacterController2D _controller;

		private void Start()
		{
			_controller = GetComponent<CharacterController2D>();

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
			GetOrientation();
			ApplyMovement();
		}

		private void GetInput()
        {
			horizontalDirection.Value = Input.GetAxisRaw("Horizontal");	
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
				isWallSliding.Value = true;
            }
            else
            {
				isWallSliding.Value = false;
			}			
		}
		
		private void ApplyMovement()
		{
			var smoothedMovementFactor = _controller.isGrounded ? groundDamping.Value : inAirDamping.Value;
			Velocity.x = Mathf.Lerp(Velocity.x, horizontalDirection.Value * runSpeed.Value, Time.deltaTime * smoothedMovementFactor);

			_controller.move(Velocity * Time.deltaTime);
			Velocity = _controller.velocity;
		}
	}
}
