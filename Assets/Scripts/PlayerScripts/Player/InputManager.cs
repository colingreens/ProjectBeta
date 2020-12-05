using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    class InputManager : MonoBehaviour
    {
        [SerializeField]
        private List<InputEvent> inputCollection;

        private void Update()
        {
            foreach (var input in inputCollection)
            {
                input.CheckForKeyPress();
            }
        }
    }
}
