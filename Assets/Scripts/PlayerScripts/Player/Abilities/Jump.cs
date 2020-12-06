using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class Jump : MonoBehaviour
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
        private WallSlideConfig wallSlide;

        private bool isWallSliding;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
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
                Velocity.x = -1 * facingDirection.Value * wallSlide.HorizontalForce;
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

    }
}
