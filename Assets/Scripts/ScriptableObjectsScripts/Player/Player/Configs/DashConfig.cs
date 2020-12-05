using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "DashConfig", menuName = "Metroidvania/Player/DashConfig", order = 5)]
    public class DashConfig : ScriptableObject
    {
        public FloatReference dashDistance;
        public FloatVariable dashCooldown;
        public BoolVariable canDash;
        public KeyCode dashKey;

    }
}
