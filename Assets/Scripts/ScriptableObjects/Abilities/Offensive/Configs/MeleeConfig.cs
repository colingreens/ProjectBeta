using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "MeleeConfig", menuName = "Metroidvania/Attacks/Config/Melee", order = 2)]
    public class MeleeConfig : ScriptableObject
    {
        public int Damage;
    }
}
