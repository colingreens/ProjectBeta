using UnityEngine;

namespace MetroidVaniaTools
{
    public class DashAbility : Ability
    {
        public override float Execute(PlayerManager playerManager)
        {           
               return  playerManager.positionInfo.facingPosition * 2f * playerManager.dashConfig.dashDistance;
                
        }
    }
}
