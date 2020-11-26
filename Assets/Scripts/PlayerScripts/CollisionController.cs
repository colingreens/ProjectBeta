using UnityEngine;

namespace MetroidVaniaTools
{
    public class CollisionController : Abilities, ICollisionController
    {
        [SerializeField]
        private float _distanceToCollider = 0.08f;
        [SerializeField]
        private LayerMask _whatIsGround;
        [SerializeField]
        private LayerMask _whatIsWall;

        private void Start()
        {
          
        }

        private void FixedUpdate()
        {
            
        }

        public virtual bool CollisionCheck(Vector2 direction, float distance, LayerMask collision)
        {
            RaycastHit2D[] hits = new RaycastHit2D[10];
            int numHits = col.Cast(direction, hits, distance);
            for (int i = 0; i < numHits; i++)
            {
                if ((1 << hits[i].collider.gameObject.layer & collision) != 0)
                {
                    currentPlatform = hits[i].collider.gameObject;
                    return true;
                }
            }
            return false;
        }

       public bool GroundCheck()
        {
            if (CollisionCheck(Vector2.down, _distanceToCollider, _whatIsGround) && !isJumping)
            {
                //if (currentPlatform.GetComponent<MoveablePlatform>())
                //{
                //    transform.parent = currentPlatform.transform;
                //}                
                isGrounded = true;
                isJumping = false;
                return true;
            }
            else
            {
                transform.parent = null;
                isGrounded = false;
                return false;
            }            
        }

        private void WallCheck()
        {
            if (CollisionCheck(Vector2.left, _distanceToCollider, _whatIsWall) && !isJumping
                || CollisionCheck(Vector2.right, _distanceToCollider, _whatIsWall) && !isJumping)
            {
                isTouchingWall = true;
            }
            else
                isTouchingWall = false;
        }
    }
}
