using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "JumpAbility", menuName = "Metroidvania/Player/Ability/Jump", order = 1)]
    public class JumpAbility : Ability
    {
        public override float Execute(PlayerManager playerManager)
        {
            return 0f;
        }
    }
}
