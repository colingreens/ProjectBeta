using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class EnemyMovement : AIManagers
    {
        [SerializeField]
        protected enum MovementType { Normal, HugWalls, Flying }
        [SerializeField]
        protected MovementType type;
        [SerializeField]
        protected bool spawnFacingLeft;
        [SerializeField]
        protected bool turnAroundOnCollision;
        [SerializeField]
        protected bool avoidFalling;
        public bool standStill;
        [SerializeField]
        protected float timeTillMaxSpeed;
        [SerializeField]
        protected float maxSpeed;
        [SerializeField]
        protected float jumpVerticalForce;
        [SerializeField]
        protected float jumpHorizontalForce;
        [SerializeField]
        protected float minDistance;
        [SerializeField]
        protected LayerMask collidersToTurnAroundOn;
        [HideInInspector]
        public bool turn;

        private bool spawning = true;
        private float acceleration;
        private float direction;
        private float runTime;

        protected bool wait;
        protected bool wasJumping;
        protected float originalWaitTime = .1f;
        protected float currentWaitTime;
        protected float currentSpeed;

        protected override void Initialization()
        {
            base.Initialization();
            if (spawnFacingLeft)
            {
                enemyCharacter.isFacingLeft = true;
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }
            currentWaitTime = originalWaitTime;
            timeTillDoAction = originalTimeTillDoAction;
            Invoke("Spawning", .01f);
        }

        protected virtual void FixedUpdate()
        {


        }
    }
}
