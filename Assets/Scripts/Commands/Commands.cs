using System;
using Cinemachine;
using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu]
    public class DashCommand : Command
    {
        [SerializeField]
        private float dashForce;
        public override void Execute(Character character)
        {            
            if (CoolDownTimeLeft < float.Epsilon)
            {
                character.Velocity.x += character.FacingDirection * dashForce;
                CoolDownTimeLeft = CoolDownTime;
            }
        }

        public override void RollBack(Character character)
        {
            throw new NotImplementedException();
        }
    }

    [CreateAssetMenu]
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
