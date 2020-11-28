using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "DashConfig", menuName = "Metroidvania/Player/Abilities/DashConfig", order = 1)]
    public class DashConfig : ScriptableObject
    {
        public float dashDistance;
        public float dashCooldown;
        public bool canDash;
        public KeyCode dashKey;

    }
}
