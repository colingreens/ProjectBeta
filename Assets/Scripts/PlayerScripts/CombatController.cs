using UnityEngine;

namespace MetroidVaniaTools
{
    public class CombatController : MonoBehaviour
    {
        public PlayerPosition playerPosition;
        public Transform projectilePosition;        

        [HideInInspector]
        public float WeaponCoolDown = .1f;

        [SerializeField]
        private Attack attack;

        private void Update()
        {
            CheckInput();
        }

        private void FixedUpdate()
        {
            Fire(attack);
        }

        private void CheckInput()
        {
            if (isShooting)
                return;
            if (Input.GetButton("Fire1"))
                canFire = true;
            else
                canFire = false;
        }

        private void Fire(IAttack attack)
        {
            if (canFire)
            {
                attack.Execute(this);
                Invoke(nameof(GlobalCoolDown), WeaponCoolDown);
            }
            canFire = false;
        }
        private void GlobalCoolDown()
        {
            isShooting = false;
            canFire = true;
        }

        private bool isShooting;
        private bool canFire;
    }
}
