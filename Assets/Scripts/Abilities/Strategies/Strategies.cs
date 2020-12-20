using UnityEngine;

namespace MetroidVaniaTools
{
    public abstract class Strategy : ScriptableObject, IStrategy
    {
        public abstract void Execute(Character character);
    }

    public interface IStrategy
    {
        void Execute(Character character);
    }

}
