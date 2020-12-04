using System;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class InputEvent : ScriptableObject
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

