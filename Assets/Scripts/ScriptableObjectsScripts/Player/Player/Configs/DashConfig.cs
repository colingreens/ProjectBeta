using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "DashConfig", menuName = "Metroidvania/Player/DashConfig", order = 5)]
    public class DashConfig : ScriptableObject
    {
        public float dashDistance;
        public float dashCooldown;
        public KeyCode dashKey;

    }
}
