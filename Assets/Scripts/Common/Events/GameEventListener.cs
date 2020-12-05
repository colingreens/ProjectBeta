using UnityEngine;
using UnityEngine.Events;

namespace MetroidVaniaTools
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent Event;
        public UnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void RaiseEvent()
        {
            Response.Invoke();
        }
    }
}
