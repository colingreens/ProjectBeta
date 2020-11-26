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
        
        protected IMovement movement;        
        protected IJump jump;
        protected IPhysics physics;
        protected ICollisionController collisionController;
        
        protected GameManager gameManager;       
        protected AimManager aimManager;
        protected Collider2D col;
        protected Rigidbody2D rb;
        protected Animator anim;
        protected InputManager input;
        protected GameObject currentPlatform;        
        protected GameObject player;
        protected ObjectPooler objectPooler;
        protected GrapplingHook grapplingHook;
        protected Weapon weapon;

        protected bool canMove;
        protected bool canJump;
        protected bool isWalking;
        protected bool isTouchingWall;
        protected float horizontalInputDirection;
        protected int facingDirection = 1;

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

        protected virtual void Flip()
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
