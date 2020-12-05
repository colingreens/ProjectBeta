using System;

namespace MetroidVaniaTools
{
    public interface IInputEvent
    {
        event Action onKeyPress;

        void CheckForKeyPress();
    }
}