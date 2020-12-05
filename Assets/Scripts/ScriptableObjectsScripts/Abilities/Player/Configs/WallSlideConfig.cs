using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "WallJump", menuName = "Metroidvania/Jump/WallJumpConfig", order = 3)]
    public class WallSlideConfig : ScriptableObject
    {
        public bool CanWallJump;

        public float WallJumpForce = 5f;
        public float HorizontalForce = 5f;
        public float wallTouchGravity = 1f;
        


    }
}
