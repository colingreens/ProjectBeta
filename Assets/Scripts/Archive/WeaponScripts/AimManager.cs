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
        public float yDirectionAimAdjust;

        private Vector3 mouse;
        private Vector3 camOffSetDistance;
        private Camera cam;
        


        protected override void Initilization()
        {
            base.Initilization();
            aimingGun.enabled = false;
            aimingOffHand.enabled = false;
            bounds.center = origin.position;
            cam = Camera.main;
        }

        protected virtual void FixedUpdate()
        {
            DirectionalAim();
            bounds.center = origin.position;
        }

        public virtual void DirectionalAim()
        {
            camOffSetDistance = cam.WorldToScreenPoint(character.transform.localPosition);
            mouse = Input.mousePosition;
            whereToAim.transform.position = new Vector2(mouse.x - camOffSetDistance.x, mouse.y - camOffSetDistance.y - yDirectionAimAdjust);            
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
            //Gizmos.DrawWireCube(origin.position, bounds.size);
            //Gizmos.DrawLine(bounds.center, whereToAim.transform.position);
        }
    }
}
