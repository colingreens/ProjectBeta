using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "Arrow", menuName = "Metroidvania/Player/Abilities/Offensive/Arrow", order = 1)]
    public class Projectile : ScriptableObject
    {
        public GameObject projectile;
        public float velocity;
        public int damage;
        public int fireRate;
        public int activeTime;

        public void Fire(int directionFacing, Vector3 position)
        {
            var bulletActive = Instantiate(projectile);
            bulletActive.transform.position = position;
            if (directionFacing < 0)
            {
                bulletActive.transform.rotation = Quaternion.Euler(180, 0, 180);
            }
            bulletActive.GetComponent<Rigidbody2D>().velocity = new Vector2(directionFacing * velocity, 0f);
        }

        public void Destroy()
        {
            Destroy(projectile);
        }
    }
}
