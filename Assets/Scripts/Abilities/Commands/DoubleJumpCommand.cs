using System;
using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "DoubleJumpCommand", menuName = "Commands/DoubleJump", order = 1)]
    public class DoubleJumpCommand : Command
    {
        private bool canDoubleJump;
        private Vector2 velocityDelta;
        public override void Execute(Character character)
        {
            if (character.IsGrounded)
            {
                //character.velocity.y = 0  what was this doing here?
                canDoubleJump = true;
                Jump(character);
                
            }
            else if (canDoubleJump && Input.GetButtonDown("Jump"))
            {
                Jump(character);
                canDoubleJump = false;
            }           
        }

        private void Jump(Character character)
        {
            if (Input.GetButtonDown("Jump"))
            {
                character.Velocity.y = Mathf.Sqrt(2 * character.JumpHeight * Mathf.Abs(character.Gravity));
                velocityDelta = character.Velocity;
            }
        }

        public override void RollBack(Character character)
        {
            throw new NotImplementedException();
        }
    }
}
