using UnityEngine;

namespace MetroidVaniaTools
{
    public interface ICollisionController
    {
        bool CollisionCheck(Vector2 direction, float distance, LayerMask collision);
    }
}
