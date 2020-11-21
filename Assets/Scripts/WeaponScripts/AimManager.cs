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

        public virtual void ChangeArms()
        {
            notAimingGun.enabled = !notAimingGun.enabled;
            notAimingOffHand.enabled = !notAimingOffHand.enabled;
            aimingGun.enabled = !aimingGun.enabled;
            aimingOffHand.enabled = !aimingOffHand.enabled;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(origin.position, bounds.size);
        }
    }
}
