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
                projectile.Fire(playerPosition.facingPosition, projectileSpawnPoint.position);
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
