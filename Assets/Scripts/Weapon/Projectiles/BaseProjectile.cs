using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class BaseProjectile : MonoBehaviour
    {
        [SerializeField]
        private Projectile projectile;
        private BoxCollider2D collider;

        private void Start()
        {
            collider = GetComponent<BoxCollider2D>();
            Destroy(gameObject, projectile.activeTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Contains("Enemy"))
            {
                collision.gameObject.GetComponent<HealthController>().Damage(projectile.damage);                
            }
            Destroy(gameObject);
        }
    }
}
