using UnityEngine;

namespace MetroidVaniaTools
{
    public class MagicAttack : IAttack
    {
        [SerializeField]
        private MagicConfig magicConfig;

        public void Execute(CombatController combat)
        {
            //Throw in Magic Logic
        }
    }
}
