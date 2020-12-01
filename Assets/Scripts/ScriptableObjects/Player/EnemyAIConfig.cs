using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class EnemyAIConfig : ScriptableObject
    {
        public int agroWakeRange = 5;
        public int agroLoseRange = 8;
        public LayerMask targetLayer;
        public bool canPatrol;
        private List<Transform> pathPoints;
    }
}
