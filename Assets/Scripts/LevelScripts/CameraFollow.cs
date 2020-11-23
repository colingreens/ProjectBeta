using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace MetroidVaniaTools
{
    public class CameraFollow : Managers
    {
        public CinemachineVirtualCamera vcam;

        protected override void Initialization()
        {
            base.Initialization();
            vcam = GetComponent<CinemachineVirtualCamera>();
        }

        public virtual void SetFollow(Transform transform)
        {
            vcam.LookAt = transform;
            vcam.Follow = transform;
        }
    }
}
