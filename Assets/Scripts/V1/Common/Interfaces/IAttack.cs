using UnityEngine;

namespace MetroidVaniaTools
{
    public interface IAttack
    {
        void Execute(CombatController combat);
    }
}
