using UnityEngine;

namespace MetroidVaniaTools
{
    public class AIBrain : MonoBehaviour
    {
        private AIState currentState;

        private void Start()
        {
            SetState(new ReturnHomeState(this));
        }

        private void Update()
        {
            currentState.Tick();
        }

        public void SetState(AIState state)
        {
            if (currentState != null)
            {
                currentState.OnStateExit();
            }

            currentState = state;
            gameObject.name = "Cube - " + state.GetType().Name;

            if (currentState != null)
                currentState.OnStateEnter();
        }
    }
}
