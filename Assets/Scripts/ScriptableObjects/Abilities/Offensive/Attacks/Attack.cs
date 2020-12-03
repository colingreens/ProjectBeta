using UnityEngine;

namespace MetroidVaniaTools
{
    public abstract class Attack : ScriptableObject, IAttack
    {
        public abstract void Execute(CombatController combatController);
    }
}
