using System;
using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "DashCommand", menuName = "Commands/Dash", order = 1)]
    public class DashCommand : Command
    {
        [SerializeField]
        private float dashForce;
        public override void Execute(Character character)
        {            
            if (CoolDownTimeLeft < float.Epsilon)
            {
                character.Velocity.x += character.MovementDirection * dashForce;
                CoolDownTimeLeft = CoolDownTime;
            }
        }

        public override void RollBack(Character character)
        {
            throw new NotImplementedException();
        }
    }
}
