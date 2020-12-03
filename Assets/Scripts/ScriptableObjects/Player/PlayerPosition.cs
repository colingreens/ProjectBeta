using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu]
    public class PlayerPosition : ScriptableObject
    {
        public float horizontalDirection = 0;
        public int facingPosition;
    }
}
