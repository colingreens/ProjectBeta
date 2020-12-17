using UnityEngine;

namespace MetroidVaniaTools
{
    public class Jump : MonoBehaviour
    {        
        [SerializeField]
        private FloatVariable jumpForce;
        [SerializeField]
        private BoolVariable CanWallJump;
        [SerializeField]
        private BoolVariable CanDoubleJump;
        [SerializeField]
        private FloatVariable wallHorizontalForce;
        [SerializeField]
        private BoolVariable isGrounded;
        [SerializeField]
        private BoolVariable isWallSliding;
        [SerializeField]
        private FloatVariable facingDirection;
        [SerializeField]
        private BoolVariable ignoreOneWayPlatformsThisFrame;
        [SerializeField]
        private FloatVariable gravity;
        [SerializeField]
        private VelocityVariable velocity;

        private float shortJump;
        private bool canDoubleJump;

        private void Start()
        {
            shortJump = jumpForce.Value / 2f;
            canDoubleJump = CanDoubleJump.Value;
        }


        // Update is called once per frame
        void Update()
        {
            if (isGrounded.Value)
            {
                if (CanDoubleJump.Value)
                {
                    canDoubleJump = true;
                }
                if (Input.GetButtonDown("Jump"))
                {
                    velocity.Value.y = Mathf.Sqrt(shortJump * -gravity.Value);
                }                
            }
            else if (canDoubleJump && Input.GetButtonDown("Jump"))
                {
                    velocity.Value.y = Mathf.Sqrt(jumpForce.Value * -gravity.Value);
                    canDoubleJump = false;
                }
            WallSlide();
        }        

        private void WallSlide()
        {
            if (isWallSliding.Value && Input.GetButton("Horizontal") && Input.GetButtonDown("Jump"))
            {
                velocity.Value.y = Mathf.Sqrt(jumpForce.Value * -gravity.Value);
                velocity.Value.x = -1 * facingDirection.Value * wallHorizontalForce.Value;
            }
            if (isWallSliding.Value)
            {
                velocity.Value.y += jumpForce.Value * Time.deltaTime;
            }
        }
    }
}
