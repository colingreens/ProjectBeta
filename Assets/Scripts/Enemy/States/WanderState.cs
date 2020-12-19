using UnityEngine;

namespace MetroidVaniaTools
{
    public class WanderState : AIState
    {
        public WanderState(AIBrain ai) : base(ai)
        {
        }

        private Vector3 nextDestination;
        private float wanderTime = 5f;
        private float timer;

        public override void OnStateEnter()
        {
            nextDestination = GetRandomDestination();
        }

        public override void Tick()
        {
            if (ReachedDestination())
            {
                nextDestination = GetRandomDestination();
            }

            //simpleAI.MoveToward(nextDestination);

            timer += Time.deltaTime;
            if (timer >= wanderTime)
            {
                simpleAI.SetState(new ReturnHomeState(simpleAI));
            }
        }
        private Vector3 GetRandomDestination()
        {
            return new Vector3(
                Random.Range(-40, 40),
                0f,
                Random.Range(-40, 40)
                );
        }

        private bool ReachedDestination()
        {
            return Vector3.Distance(simpleAI.transform.position, nextDestination) < 0.5f;
        }

    }
}
