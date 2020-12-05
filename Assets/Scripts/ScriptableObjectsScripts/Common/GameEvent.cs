using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "CustomSO/Event/GameEvent", order = 1)]
    public class GameEvent : ScriptableObject
    {
        private List<GameEventListener> listeners =
            new List<GameEventListener>();

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; --i)
            {
                listeners[i].RaiseEvent();
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
