using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "FireCommand", menuName = "Strategy/FireCommand", order = 1)]
    public class FireWeapon : Strategy
    {
        public GameObject Projectile;
        public float Velocity;
        
        public override void Execute(Character character)
        {
            FireTheWeapon(character.MovementDirection, character.transform.position);
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

}
