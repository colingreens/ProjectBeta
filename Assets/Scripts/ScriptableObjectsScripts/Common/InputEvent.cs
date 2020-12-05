using System;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "InputEvent", menuName = "CustomSO/Event/InputEvent", order = 1)]
    public class InputEvent : ScriptableObject, IInputEvent
    {
        public KeyCode KeyCode;

        private List<InputEvent> listeners =
            new List<InputEvent>();

        public event Action onKeyPress;
        public void CheckForKeyPress()
        {
            if (Input.GetKeyDown(KeyCode))
                onKeyPress();
        }

        public void RegisterListener(InputEvent listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(InputEvent listener)
        {
            listeners.Remove(listener);
        }
    }
}

