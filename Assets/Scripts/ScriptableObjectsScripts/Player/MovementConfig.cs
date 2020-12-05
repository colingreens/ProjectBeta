using UnityEngine;

namespace MetroidVaniaTools 
{
    [CreateAssetMenu(fileName = "MovementConfig", menuName = "Metroidvania/Player/Movement", order = 2)]
    public class MovementConfig : ScriptableObject
    {
        public float runSpeed = 8f;
        public float groundDamping = 20f; // how fast do we change direction? higher means faster
        public float inAirDamping = 5f;
    }
}
