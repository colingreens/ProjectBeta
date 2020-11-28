using UnityEngine;

namespace MetroidVaniaTools 
{
    [CreateAssetMenu(fileName = "JumpConfig", menuName = "Metroidvania/Jump/JumpConfig", order = 1)]
    public class JumpConfig : ScriptableObject
    {
        public float gravity = -25f;
        public float jumpHeight = 3f;
        public float VariableJumpHeightMultiplier = 0.5f;        
    }
}
