using UnityEngine;

namespace MetroidVaniaTools
{
	public class CharacterManager : MonoBehaviour
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
		private FloatVariable Gravity;

		[SerializeField]
		private CharacterController2D _controller;

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
			ApplyMovement();
		}

		private void GetInput()
        {
			horizontalDirection.Value = Input.GetAxisRaw("Horizontal");	
		}
		
		private void ApplyMovement()
		{
			var smoothedMovementFactor = _controller.isGrounded ? groundDamping.Value : inAirDamping.Value;
			velocity.Value.x = Mathf.Lerp(velocity.Value.x, horizontalDirection.Value * runSpeed.Value, Time.deltaTime * smoothedMovementFactor);
			velocity.Value.y += Gravity.Value * Time.deltaTime;
			_controller.move(velocity.Value * Time.deltaTime);
			velocity.Value = _controller.velocity;
		}
	}
}
