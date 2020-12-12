using UnityEngine;

namespace MetroidVaniaTools
{
    public class Jump : MonoBehaviour
    {        
        [SerializeField]
        private FloatVariable jumpForce;//= 3f; 
        [SerializeField]
        private BoolVariable CanWallJump;
        [SerializeField]
        private FloatVariable wallHorizontalForce;//= 3f;
        [SerializeField]
        private BoolVariable isGrounded;
        [SerializeField]
        private FloatVariable facingDirection;
        [SerializeField]
        private BoolVariable ignoreOneWayPlatformsThisFrame;
        [SerializeField]
        private FloatVariable gravity; //= -25f;
        [SerializeField]
        private VelocityVariable velocity;

        private bool isWallSliding;
       

        // Update is called once per frame
        void Update()
        {
            if (isGrounded.Value)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    velocity.Value.y = Mathf.Sqrt(jumpForce.Value * -gravity.Value);
                }
            }
            WallSlide();
        }

        private void WallSlide()
        {
            if (isWallSliding && Input.GetButton("Horizontal") && Input.GetButtonDown("Jump"))
            {
                velocity.Value.y = Mathf.Sqrt(jumpForce.Value * -gravity.Value);
                velocity.Value.x = -1 * facingDirection.Value * wallHorizontalForce.Value;
            }
            if (isWallSliding)
            {
                velocity.Value.y += jumpForce.Value * Time.deltaTime;
            }
        }
    }
}
