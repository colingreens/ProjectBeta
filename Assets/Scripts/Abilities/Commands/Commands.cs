using Cinemachine;
using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "VariableJumpCommand", menuName = "Commands/VariableJump", order = 1)]
    public class VariableJump : Command{
        
        public override void Execute(Character character)
        {
            if (character.IsGrounded)
            {
                character.Velocity.y = 0f;
                if (Input.GetButtonDown("Jump"))
                {
                    character.Velocity.y = Mathf.Sqrt(2 * character.JumpHeight * Mathf.Abs(character.Gravity));
                }
            }
            else
            {
                if (Input.GetButtonUp("Jump"))
                {
                    character.Velocity.y = Mathf.Sqrt(character.JumpHeight * Mathf.Abs(character.Gravity / 2f));
                }
            }
        }

        public override void RollBack(Character character)
        {
            throw new System.NotImplementedException();
        }
    }

    public abstract class Command : ScriptableObject, ICommand
    {
        public float CoolDownTime;
        public float CoolDownTimeLeft;
        
        public abstract void Execute(Character character);
        public abstract void RollBack(Character character);

    }
   
    public interface ICommand
    {
        void Execute(Character character);
        void RollBack(Character character);
    }
}
