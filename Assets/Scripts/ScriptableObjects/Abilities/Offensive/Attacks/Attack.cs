using UnityEngine;

namespace MetroidVaniaTools
{
    public abstract class Attack : ScriptableObject, IAttack
    {
        public GameEvent onAttack;        
        public abstract void Execute(CombatController combatController);
    }    
}
