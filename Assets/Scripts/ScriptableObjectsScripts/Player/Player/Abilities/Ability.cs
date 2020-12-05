using UnityEngine;

namespace MetroidVaniaTools
{
    public abstract class Ability : ScriptableObject, IAbility
    {
        public abstract float Execute(PlayerManager playerManager);
    }
}
