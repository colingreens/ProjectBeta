using UnityEngine;

namespace MetroidVaniaTools
{
    public class PhysicsManager : MonoBehaviour, IPhysics
    {
        [SerializeField]
        private PlayerCharacter character;
        [SerializeField]
        private float airDragMultiplier = 0.95f;


        private void FixedUpdate()
        {
            ModifyPhysics();
        }

        private void ModifyPhysics()
        {
            //if (!character.isGrounded && !character.isWallSliding &&  == 0)
            //{
            //    character.rb.velocity = new Vector2(character.rb.velocity.x * airDragMultiplier, character.rb.velocity.y);
            //}
        }
    }
}

