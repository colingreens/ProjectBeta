using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "Melee", menuName = "Metroidvania/Player/Abilities/Offensive/Melee", order = 2)]
    public class MeleeConfig : ScriptableObject
    {
        public int Damage;
    }
}
