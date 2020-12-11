using UnityEngine;

namespace MetroidVaniaTools
{
    public class Jump : MonoBehaviour
    {

        [SerializeField]
        private FloatVariable gravity; //= -25f;
        [SerializeField]
        private FloatVariable jumpForce;//= 3f;
        [SerializeField]
        private FloatVariable additionalJumpHeight; //= 0.5f;
        [SerializeField]
        private bool CanDoubleJump;
        [SerializeField]
        private bool CanWallJump;
        [SerializeField]
        private FloatVariable wallJumpForce;//= 3f;
        [SerializeField]
        private FloatVariable wallHorizontalForce;//= 3f;
        [SerializeField]
        private FloatVariable wallHopForce;//= 3f;
        [SerializeField]
        private BoolVariable isGrounded;
        [SerializeField]
        private FloatVariable facingDirection;
        [SerializeField]
        private BoolVariable ignoreOneWayPlatformsThisFrame;

        private CharacterManager Player;

        private bool isWallSliding;
        // Start is called before the first frame update
        void Start()
        {
            Player = GetComponent<CharacterManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isGrounded.Value)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    Player.Velocity.y = Mathf.Sqrt(jumpForce.Value * -gravity.Value);
                }

                else if (Input.GetButton("Jump"))
                {
                    Player.Velocity.y += Mathf.Sqrt(additionalJumpHeight.Value * -gravity.Value);
                    ignoreOneWayPlatformsThisFrame.Value = true;
                }

            }
            WallSlide();
        }

        private void WallSlide()
        {
            if (isWallSliding && Input.GetButton("Horizontal") && Input.GetButtonDown("Jump"))
            {
                Player.Velocity.y = Mathf.Sqrt(wallJumpForce.Value * -gravity.Value);
                Player.Velocity.x = -1 * facingDirection.Value * wallHorizontalForce.Value;
            }
            if (isWallSliding)
            {
                Player.Velocity.y += wallHopForce.Value * Time.deltaTime;
            }
        }
    }
}
