using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Metroidvania/Attacks/Config/Projectile", order = 1)]
    public class ProjectileConfig : ScriptableObject
    {
        public GameObject projectile; //the prefab
        public float velocity;
        public int damage;
        public int fireRate;
        public int activeTime;

        public void Fire(float directionFacing, Vector3 position)
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
