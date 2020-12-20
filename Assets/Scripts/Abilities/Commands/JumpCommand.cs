using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "JumpCommand", menuName = "Commands/Jump", order = 1)]
    public class JumpCommand : Command
    {
        private Vector2 velocityDelta;
        public override void Execute(Character character)
        {
            if (character.IsGrounded)
            {
                character.Velocity.y = 0f;
                if (Input.GetButtonDown("Jump"))
                {
                    character.Velocity.y =  Mathf.Sqrt(2 * character.JumpHeight * Mathf.Abs(character.Gravity));
                    velocityDelta = character.Velocity;
                }
            }
        }

        public override void RollBack(Character character)
        {
            character.Velocity.x = -velocityDelta.x;
            character.Velocity.y = -velocityDelta.y;
        }
    }
}
