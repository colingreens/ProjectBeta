using UnityEngine;

namespace MetroidVaniaTools
{
    public class PhysicsManager : Abilities, IPhysics
    {
        [SerializeField]
        private float airDragMultiplier = 0.95f;


        private void FixedUpdate()
        {
            ModifyPhysics();
        }

        private void ModifyPhysics()
        {
            if (!isGrounded && !isWallSliding && horizontalInputDirection == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
            }
        }
    }
}

