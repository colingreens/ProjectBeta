using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class BaseProjectile : MonoBehaviour
    {
        [SerializeField]
        private Projectile projectile;

        private void Start()
        {
            Destroy(gameObject, projectile.activeTime);
        }
    }
}
