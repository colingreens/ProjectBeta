using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "NewVelocityVar", menuName = "CustomSO/Types/VelocityVariable")]
    public class VelocityVariable : ScriptableObject
    {
        public Vector3 Value;
    }
}
