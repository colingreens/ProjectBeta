using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "DashAbility", menuName = "Metroidvania/Player/Ability/Dash", order = 2)]
    public class DashAbility : Ability
    {
        [SerializeField]
        private float dashDistance;
        public override float Execute()
        {   
          return dashDistance;            
        }
    }
}
