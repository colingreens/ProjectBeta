using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

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
            if (playerSpawnPoints.Count <= PlayerPrefs.GetInt("SpawnReference"))
            {
                startingLocation = playerSpawnPoints[0].position;
            }
            else
            {
                startingLocation = playerSpawnPoints[PlayerPrefs.GetInt("SpawnReference")].position;
                CreatePlayer(initialPlayer, startingLocation);
            }
                       
        }

        protected virtual void OnDisable()
        {
            PlayerPrefs.SetInt("FacingLeft", character.isFacingLeft ? 1 : 0);
        }
        public virtual void SetCamera()
        {
            virtualCamera.Follow = character.transform;
            virtualCamera.LookAt = character.transform;
        }

        public virtual void NextScene(SceneReference scene, int spawnReference)
        {
            PlayerPrefs.SetInt("FacingLeft", character.isFacingLeft ? 1 : 0);
            PlayerPrefs.SetInt("SpawnReference", spawnReference);
            SceneManager.LoadScene(scene);
        }

        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(levelSize.center, levelSize.size);
        }

        
    
    }
}

