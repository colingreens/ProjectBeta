using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu]
    public class FireWeapon : Strategy
    {
        public GameObject Projectile;
        public float Velocity;
        
        public override void Execute(Character character)
        {
            FireTheWeapon(character.FacingDirection, character.transform.position);
        }

        private void FireTheWeapon(float directionFacing, Vector3 position)
        {
            var bulletActive = Instantiate(Projectile);
            bulletActive.transform.position = position;
            if (directionFacing < 0)
            {
                bulletActive.transform.rotation = Quaternion.Euler(180, 0, 180);
            }
            bulletActive.GetComponent<Rigidbody2D>().velocity = new Vector2(directionFacing * Velocity , 0f);
        }
    }

    public abstract class Strategy : ScriptableObject, IStrategy
    {
        public abstract void Execute(Character character);
    }

    public interface IStrategy
    {
        void Execute(Character character);
    }

}
