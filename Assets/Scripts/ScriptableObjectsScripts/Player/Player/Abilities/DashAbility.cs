using UnityEngine;

namespace MetroidVaniaTools
{
    public class DashAbility : Ability
    {
        public DashConfig dashConfig;
        public override float Execute(PlayerManager playerManager)
        {
            if (dashConfig.canDash.value)
            {
                dashConfig.canDash.value = false;
                return playerManager.positionInfo.facingPosition * dashConfig.dashDistance;
            }
            else
                return 0f;
        }
    }
}
