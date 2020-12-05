using UnityEngine;

namespace MetroidVaniaTools
{
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
