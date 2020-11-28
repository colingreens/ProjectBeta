using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "WallJump", menuName = "Metroidvania/Jump/WallJumpConfig", order = 3)]
    public class WallJumpConfig : ScriptableObject
    {
        public bool CanWallHop;
        public bool CanWallJump;

        public float WallHopForce = 1f;
        public float WallJumpForce = 1f;

        public Vector2 WallHopDirection = new Vector2(1f,1f);
        public Vector2 WallJumpDirectio = new Vector2(1f,1f);


    }
}
