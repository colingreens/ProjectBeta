using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "ExtraJumpConfig", menuName = "Metroidvania/Jump/ExtraJumpConfig", order = 2)]
    public class ExtraJumpConfig : ScriptableObject
    {
        public bool CanDoubleJump;
        public int NumberOfJumps;
        public int AirJumpForce;
        
    }
}
