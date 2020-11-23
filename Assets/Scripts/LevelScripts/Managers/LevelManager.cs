using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace MetroidVaniaTools
{
    public class LevelManager : Managers
    {
        public Bounds levelSize;
        public GameObject initialPlayer;
        public CinemachineVirtualCamera virtualCamera;

        [SerializeField]
        protected List<Transform> playerSpawnPoints;

        private Vector3 startingLocation;
        protected virtual void Awake()
        {
            startingLocation = playerSpawnPoints[0].position;
            CreatePlayer(initialPlayer, startingLocation);            
        }

        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(levelSize.center, levelSize.size);
        }
    }
}

