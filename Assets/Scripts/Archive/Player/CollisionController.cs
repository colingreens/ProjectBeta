using UnityEngine;

namespace MetroidVaniaTools
{
    public class CollisionController : MonoBehaviour, ICollisionController
    {
        [SerializeField]
        private PlayerCharacter character;
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
            int numHits = character.col.Cast(direction, hits, distance);
            for (int i = 0; i < numHits; i++)
            {
                if ((1 << hits[i].collider.gameObject.layer & collision) != 0)
                {
                    character.currentPlatform = hits[i].collider.gameObject;
                    return true;
                }
            }
            return false;
        }

       public bool GroundCheck()
        {
            if (CollisionCheck(Vector2.down, _distanceToCollider, _whatIsGround) && !character.isJumping)
            {
                //if (currentPlatform.GetComponent<MoveablePlatform>())
                //{
                //    transform.parent = currentPlatform.transform;
                //}                
                character.isGrounded = true;
                character.isJumping = false;
                return true;
            }
            else
            {
                transform.parent = null;
                character.isGrounded = false;
                return false;
            }            
        }

        private void WallCheck()
        {
            if (CollisionCheck(Vector2.left, _distanceToCollider, _whatIsWall) && !character.isJumping
                || CollisionCheck(Vector2.right, _distanceToCollider, _whatIsWall) && !character.isJumping)
            {
                character.isTouchingWall = true;
            }
            else
                character.isTouchingWall = false;
        }
    }
}
