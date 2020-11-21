using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.IK;

namespace MetroidVaniaTools
{
    public class AimManager : Abilities
    {
        public Solver2D aimingGun;
        public Solver2D aimingOffHand;
        public Solver2D notAimingGun;
        public Solver2D notAimingOffHand;
        public Transform whereToAim;
        public Transform whereToPlaceHand;
        public Transform origin;
        public Bounds bounds;

        protected override void Initilization()
        {
            base.Initilization();
            aimingGun.enabled = false;
            aimingOffHand.enabled = false;
            bounds.center = origin.position;
        }

        protected virtual void FixedUpdate()
        {
            ChangeArms();
            bounds.center = origin.position;
        }

        public virtual void ChangeArms()
        {
            if (weapon.currentTimeTillChangeArms > 0 )
            {
                aimingGun.enabled = true;
                aimingOffHand.enabled = true;
                notAimingGun.enabled = false;
                notAimingOffHand.enabled = false;
            }
            if (weapon.currentTimeTillChangeArms < 0)
            {
                aimingGun.enabled = false;
                aimingOffHand.enabled = false;
                notAimingGun.enabled = true;
                notAimingOffHand.enabled = true;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(origin.position, bounds.size);
        }
    }
}
