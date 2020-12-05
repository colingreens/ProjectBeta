using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "EnemyAIConfig", menuName = "Metroidvania/Enemy/AIConfig", order = 1)]
    public class EnemyAIConfig : ScriptableObject
    {
        public int agroWakeRange = 5;
        public int agroLoseRange = 8;
        public LayerMask targetLayer;
        public bool canPatrol;
        private List<Transform> pathPoints;
    }
}
