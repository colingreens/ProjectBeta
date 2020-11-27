using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "PlayerBase", menuName = "Metroidvania/Player", order = 1)]    
    public class PlayerBase : ScriptableObject
    {
        public Transform transform;
        public Rigidbody2D rigidBody;
        public Collider2D collider;        
    }
}
