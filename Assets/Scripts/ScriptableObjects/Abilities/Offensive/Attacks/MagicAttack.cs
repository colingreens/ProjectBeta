using UnityEngine;

namespace MetroidVaniaTools
{
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
