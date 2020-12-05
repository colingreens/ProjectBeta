using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "NewFloatVar", menuName = "CustomSO/Types/FloatVariable")]
    public class FloatVariable : ScriptableObject 
    {
        public float Value;
    }
}
