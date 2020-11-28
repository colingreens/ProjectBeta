using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "Arrow", menuName = "Metroidvania/Player/Abilities/Offensive/Arrow", order = 1)]
    public class Projectile : ScriptableObject
    {
        public GameObject projectile;
        public float velocity;
        public int damage;
        public int fireRate;
    }
}
