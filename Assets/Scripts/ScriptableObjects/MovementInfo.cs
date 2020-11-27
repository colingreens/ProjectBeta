using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MetroidVaniaTools 
{
    [CreateAssetMenu(fileName = "MovementInfo", menuName = "Metroidvania/PlayerMovement")]
    public class MovementInfo : ScriptableObject
    {
        public float gravity = -25f;
        public float runSpeed = 8f;
        public float groundDamping = 20f; // how fast do we change direction? higher means faster
        public float inAirDamping = 5f;
        public float jumpHeight = 3f;
    }
}
