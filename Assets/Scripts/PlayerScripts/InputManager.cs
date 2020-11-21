using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        protected KeyCode crouchHeld;
        [SerializeField]
        protected KeyCode dashPressed;
        [SerializeField]
        protected KeyCode sprintingHeld;
        [SerializeField]
        protected KeyCode jumpKey;

        // Update is called once per frame
        void Update()
        {
            CrouchHeld();
            DashPressed();
            SprintingHeld();
            JumpPressed();
            JumpHeld();
            FireOnePressed();
        }

        public virtual bool CrouchHeld()
        {
            if (Input.GetKey(crouchHeld))
            {
                return true;
            }
            return false;
        }

        public virtual bool DashPressed()
        {
            if (Input.GetKeyDown(dashPressed))
            {
               return true;
            }
            else
                return false;
        }

        public virtual bool SprintingHeld()
        {
            if (Input.GetKey(sprintingHeld))
            {
                return true;
            }
            else
                return false;

        }

        public virtual bool JumpPressed()
        {
            if (Input.GetKeyDown(jumpKey))
            {
                return true;
            }
            else
                return false;
        }

        public virtual bool JumpHeld()
        {
            if (Input.GetKey(jumpKey))
            {
                return true;
            }
            else
                return false;
        }

        public virtual bool FireOnePressed()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                return true;
            }
            else
                return false;
        }
    }
}