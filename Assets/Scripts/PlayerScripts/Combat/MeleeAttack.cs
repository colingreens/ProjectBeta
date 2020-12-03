using UnityEngine;

namespace MetroidVaniaTools
{
    public class MeleeAttack : IAttack
    {
        [SerializeField]
        private MeleeConfig meleeConfig;
        public void Execute(CombatController combat)
        {
            //Melee Logic
        }
    }
}
