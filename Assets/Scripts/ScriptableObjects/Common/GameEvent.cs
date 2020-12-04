using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject
    {
        private List<GameEventListener> listeners =
            new List<GameEventListener>();

        public void Raise()
        {
            foreach (var listener in listeners)
            {
                listener.OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
