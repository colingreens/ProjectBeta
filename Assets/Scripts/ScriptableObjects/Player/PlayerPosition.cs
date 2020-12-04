using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "PlayerPosition", menuName = "Metroidvania/Player/Position", order = 3)]
    public class PlayerPosition : ScriptableObject
    {
        public float horizontalDirection = 0;
        public int facingPosition;
        public Transform transform;
    }
}
