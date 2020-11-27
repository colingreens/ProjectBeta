using UnityEngine;

namespace MetroidVaniaTools
{
    public class Character : MonoBehaviour
    {
        [HideInInspector]
        public bool isFacingLeft;
        [HideInInspector]
        public bool isGrounded;
        [HideInInspector]
        public bool isCrouching;
        [HideInInspector]
        public bool isDashing;
        [HideInInspector]
        public bool isWallSliding;
        [HideInInspector]
        public bool isJumping;
        [HideInInspector]
        public bool isJumpingThroughPlatform;
        
        internal IMovement movement;        
        internal IJump jump;
        internal IPhysics physics;
        internal ICollisionController collisionController;

        internal GameManager gameManager;
        internal AimManager aimManager;
        internal Collider2D col;
        internal Rigidbody2D rb;
        internal Animator anim;
        internal InputManager input;
        internal GameObject currentPlatform;
        internal GameObject player;
        internal ObjectPooler objectPooler;
        internal GrapplingHook grapplingHook;
        internal Weapon weapon;

        internal bool canMove;
        internal bool canJump;
        internal bool isWalking;
        internal bool isTouchingWall;
        internal int facingDirection = 1;
        internal float horizontalInputDirection;

        private Vector2 facingLeft;


        private void Awake()
        {
            Initilization();
        }

        protected virtual void Initilization()
        {
            col = GetComponent<Collider2D>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            input = GetComponent<InputManager>();

            movement = GetComponent<IMovement>();
            jump = GetComponent<IJump>();
            physics = GetComponent<IPhysics>();
            collisionController = GetComponent<ICollisionController>();

            facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

        internal virtual void Flip()
        {
            if (isFacingLeft || (!isFacingLeft && isWallSliding))
            {
                transform.localScale = facingLeft;
                facingDirection = 1;
                isFacingLeft = false;
            }
            if (!isFacingLeft || (isFacingLeft && isWallSliding))
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                facingDirection = -1;
                isFacingLeft = true;
            }           
        }
      

        protected virtual bool Falling(float velocity)
        {
            if (!isGrounded && rb.velocity.y < velocity)
            {
                return true;
            }
            else
                return false;
        }

        protected virtual void FallSpeed(float speed)
        {
            rb.velocity = new Vector2(rb.velocity.x, (rb.velocity.y * speed)); 
        }

        public void InitializePlayer()
        {
            player = FindObjectOfType<Character>().gameObject;
            player.GetComponent<Character>().isFacingLeft = PlayerPrefs.GetInt("FacingLeft") == 1 ? true : false;
            if (player.GetComponent<Character>().isFacingLeft)
            {
                player.transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
        }
    }
}
