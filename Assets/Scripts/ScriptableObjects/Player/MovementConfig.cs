using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MetroidVaniaTools 
{
    [CreateAssetMenu(fileName = "MovementInfo", menuName = "Metroidvania/PlayerMovement")]
    public class MovementConfig : ScriptableObject
    {
        public float runSpeed = 8f;
        public float groundDamping = 20f; // how fast do we change direction? higher means faster
        public float inAirDamping = 5f;        
    }
}
