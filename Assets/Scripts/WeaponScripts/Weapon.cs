using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class Weapon : Abilities
    {
        [SerializeField]
        protected List<WeaponTypes> weaponTypes;
        [SerializeField]
        public Transform gunBarrel;
        [SerializeField]
        protected Transform gunRotation;

        [HideInInspector]
        public List<GameObject> currentPool = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> bulletsToReset = new List<GameObject>();
        public GameObject currentProjectile;
        public WeaponTypes currentWeapon;
        public float currentTimeTillChangeArms;

        private GameObject projectileParentFolder;
        private float currentTimeBetweenShots;

        protected override void Initilization()
        {
            base.Initilization();
            foreach (var weapon in weaponTypes)
            {
                projectileParentFolder = new GameObject();
                objectPooler.CreatePool(weapon, currentPool, projectileParentFolder);
            }
            currentWeapon = weaponTypes[0];
        }

        protected virtual void Update()
        {
            if (input.FireOnePressed())
            {
                FireWeapon();
            }
        }

        protected virtual void FixedUpdate()
        {
            NegateTimeTillChangeArms();
            FireWeaponHeld();
        }

        protected virtual void FireWeapon()
        {
            CheckAimAndArms();            
            currentProjectile = objectPooler.GetObject(currentPool, currentWeapon, this, projectileParentFolder);
            if (currentProjectile != null)
            {
                Invoke("PlaceProjectile", .1f);
            }
            currentTimeBetweenShots = currentWeapon.timeBetweenShots;
        }

        protected virtual void FireWeaponHeld()
        {
            if (input.FireOneHeld())
            {
                if (currentWeapon.automatic)
                {
                    CheckAimAndArms();
                    currentTimeBetweenShots -= Time.deltaTime;
                    if (currentTimeBetweenShots < 0)
                    {
                        currentProjectile = objectPooler.GetObject(currentPool, currentWeapon, this, projectileParentFolder);
                        if (currentProjectile != null)
                        {
                            Invoke("PlaceProjectile", .1f);
                        }
                        currentTimeBetweenShots = currentWeapon.timeBetweenShots;
                    }
                }
            }
        }

        protected virtual void NegateTimeTillChangeArms()
        {
            currentTimeTillChangeArms -= Time.deltaTime;
        }

        protected virtual void PlaceProjectile()
        {
            currentProjectile.transform.position = gunBarrel.position;
            currentProjectile.transform.rotation = gunRotation.rotation;
            currentProjectile.SetActive(true);
            if (!character.isFacingLeft)
            {
                currentProjectile.GetComponent<Projectile>().left = false;
            }
            else
            {
                currentProjectile.GetComponent<Projectile>().left = true;
            }
            currentProjectile.GetComponent<Projectile>().fired = true;
        }

        protected virtual void CheckAimAndArms()
        {
            aimManager.aimingGun.transform.GetChild(0).position = aimManager.whereToAim.transform.position;
            aimManager.aimingOffHand.transform.GetChild(0).position = aimManager.whereToPlaceHand.transform.position;
            currentTimeTillChangeArms = currentWeapon.lifeTime;
            aimManager.ChangeArms();
        }
    }
}
