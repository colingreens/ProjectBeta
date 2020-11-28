using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class CombatController : MonoBehaviour
    {
        [SerializeField]
        private Transform projectileSpawnPoint;
        [SerializeField]
        private Projectile projectile;
        [SerializeField]
        private MovementConfig playerPosition;
        
        private bool canFire;
        private bool isShooting;

        private void Update()
        {
            CheckInput();
        }

        private void FixedUpdate()
        {
            Fire();
        }

        private void CheckInput()
        {
            if (isShooting)
            {
                return;
            }
            if (Input.GetButton("Fire1"))            
                canFire = true;            
            else
                canFire = false;
        }

        private void Fire()
        {
            if (canFire)
            {
                isShooting = true;

                //instaniate and shoot bullet
                GameObject bulletActive = Instantiate(projectile.projectile);
                bulletActive.transform.position = projectileSpawnPoint.position;
                bulletActive.GetComponent<Rigidbody2D>().velocity = new Vector2(playerPosition.facingPosition * projectile.velocity, 0f);
                canFire = false;

                Invoke("ResetShot", projectile.fireRate);
            }
        }



        private void ResetShot()
        {
            isShooting = false;
        }
    }
}
