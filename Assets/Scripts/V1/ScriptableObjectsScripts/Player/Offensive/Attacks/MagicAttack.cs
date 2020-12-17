using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "MagicAttack", menuName = "Metroidvania/Attacks/Magic", order = 3)]
    public class MagicAttack : Attack
    {
        [SerializeField]
        private MagicConfig magicConfig;

        public override void Execute(CombatController combat)
        {
            //Throw in Magic Logic
        }
    }
}
