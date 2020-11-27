using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "PlayerBase", menuName = "Metroidvania/Player", order = 1)]    
    public class PlayerBase : ScriptableObject
    {
        public Rigidbody2D rigidBody;
        public CapsuleCollider2D collider;
    }
}
