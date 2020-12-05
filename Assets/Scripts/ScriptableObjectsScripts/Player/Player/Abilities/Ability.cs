using UnityEngine;

namespace MetroidVaniaTools
{
    public abstract class Ability : ScriptableObject, IAbility
    {
        public abstract void Execute(PlayerManager playerManager);
    }
}
