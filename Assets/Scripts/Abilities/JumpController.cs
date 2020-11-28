using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class JumpController : MonoBehaviour
    {
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

        private CharacterController2D _controller;

        private Vector3 _velocity;

        private void Start()
        {
            _controller = GetComponent<CharacterController2D>();
            _controller.onControllerCollidedEvent += OnControllerCollider;
        }

        private void Update()
        {
            ApplyJump();
        }

        private void ApplyJump()
        {
            if (_controller.isGrounded)
                _velocity.y = 0;

            if (_controller.isGrounded && Input.GetButtonDown("Jump"))
            {
                _velocity.y = Mathf.Sqrt(2f * jump.jumpHeight * -jump.gravity);
                //_animator.Play(Animator.StringToHash("Jump"));
            }
            // apply gravity before moving
            _velocity.y += jump.gravity * Time.deltaTime;

            // if holding down, bump up our movement amount and turn off one way platform detection for a frame.
            // this lets us jump down through one way platforms
            if (_controller.isGrounded && Input.GetButton("Jump"))
            {
                _velocity.y *= jump.VariableJumpHeightMultiplier;
                _controller.ignoreOneWayPlatformsThisFrame = true;
            }
            //_controller.move(_velocity * Time.deltaTime);

            // grab our current _velocity to use as a base for all calculations
            _velocity = _controller.velocity;
        }

        void OnControllerCollider(RaycastHit2D hit)
        {
            // bail out on plain old ground hits cause they arent very interesting
            if (hit.normal.y == 1f)
                return;

            // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
            Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
        }
    }
}
