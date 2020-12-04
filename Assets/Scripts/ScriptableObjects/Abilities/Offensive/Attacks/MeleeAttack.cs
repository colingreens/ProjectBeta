using UnityEngine;

namespace MetroidVaniaTools
{
    public class MeleeAttack : Attack
    {
        [SerializeField]
        private MeleeConfig meleeConfig;
        public override void Execute(CombatController combat)
        {
           //add melee logic
        }
    }
}
