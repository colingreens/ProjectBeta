using UnityEngine;

namespace MetroidVaniaTools
{
    public abstract class Character : MonoBehaviour
    {
        
        public Vector2 Velocity;
        
        public bool IsGrounded;
        [HideInInspector]
        public float MoveInput;
        [HideInInspector]
        public float MovementDirection;
        [HideInInspector]
        public bool IsFacingRight;

        [Header("SpeedSettings")]
        [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
        public float Speed = 9;

        [SerializeField, Tooltip("Acceleration while grounded.")]
        public float WalkAcceleration = 75;

        [SerializeField, Tooltip("Acceleration while in the air.")]
        public float AirAcceleration = 30;

        [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
        public float GroundDeceleration = 70;

        [SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
        public float JumpHeight = 4;

        [SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
        public float Gravity = -25;

        public LayerMask GroundLayer;
    }
}