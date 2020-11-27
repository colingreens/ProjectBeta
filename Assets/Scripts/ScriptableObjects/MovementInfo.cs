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
        public float movementSpeed = 10.0f;
        public float jumpForce = 16.0f;
        public float groundCheckRadius;
        public float wallCheckDistance;
        public float wallSlideSpeed;
        public float movementForceInAir;
        public float airDragMultiplier = 0.95f;
        public float variableJumpHeightMultiplier = 0.5f;
        public float wallHopForce;
        public float wallJumpForce;

        public int amountOfJumps;

        public Vector2 wallHopDirection;
        public Vector2 wallJumpDirection;

        public Transform groundCheck;
        public Transform wallCheck;

        public LayerMask whatIsGround;
    }
}
