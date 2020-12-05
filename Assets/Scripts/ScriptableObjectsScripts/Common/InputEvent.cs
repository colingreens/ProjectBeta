using System;
using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "InputEvent", menuName = "Metroidvania/Event/InputEvent", order = 1)]
    public class InputEvent : ScriptableObject, IInputEvent
    {
        public KeyCode KeyCode;

        public event Action onKeyPress;
        public void CheckForKeyPress()
        {
            if (Input.GetKeyDown(KeyCode))
                onKeyPress();
        }
    }
}

