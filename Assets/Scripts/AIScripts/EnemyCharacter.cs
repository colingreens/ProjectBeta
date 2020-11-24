using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class EnemyCharacter : MonoBehaviour
    {
        [HideInInspector]
        public bool isFacingLeft;
        [HideInInspector]
        public bool isGrounded;

        protected Rigidbody2D rb;
        protected Collider2D col;

        private Vector2 facingLeft;
        
        // Start is called before the first frame update
        void Start()
        {
            Initilization();
        }

        protected virtual void Initilization()
        {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

        protected virtual void Flip()
        {
            if (isFacingLeft)
            {
                transform.localScale = facingLeft; 
            }
            if (!isFacingLeft)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
        }

        protected virtual bool CollisionCheck(Vector2 direction, float distance, LayerMask collision)
        {
            RaycastHit2D[] hits = new RaycastHit2D[10];
            int numHits = col.Cast(direction, hits, distance);
            for (int i = 0; i < numHits; i++)
            {
                if ((1 << hits[i].collider.gameObject.layer & collision) != 0)
                {
                    //currentPlatform = hits[i].collider.gameObject;
                    return true;
                }
            }
            return false;
        }
    }
}
