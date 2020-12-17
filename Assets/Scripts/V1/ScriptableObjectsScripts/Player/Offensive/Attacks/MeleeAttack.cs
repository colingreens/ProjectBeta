using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "MeleeAttack", menuName = "Metroidvania/Attacks/Melee", order = 1)]
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
