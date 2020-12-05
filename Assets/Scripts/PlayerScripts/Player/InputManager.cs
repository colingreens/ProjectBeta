using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    class InputManager : MonoBehaviour
    {
        [SerializeField]
        private List<IInputEvent> inputCollection;

        private void Update()
        {
            foreach (var input in inputCollection)
            {
                input.CheckForKeyPress();
            }
        }
    }
}
