using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace MetroidVaniaTools
{
    public class GameManager : MonoBehaviour
    {
        [HideInInspector]
        public float xMin;
        [HideInInspector]
        public float xMax;
        [HideInInspector]
        public float yMin;
        [HideInInspector]
        public float yMax;

        protected GameObject player;
        protected Character character;
        protected LevelManager levelManager;

        // Start is called before the first frame update
        void Start()
        {
            Initialization();

        }
        protected virtual void Initialization()
        {
            player = FindObjectOfType<Character>().gameObject;
            character = player.GetComponent<Character>();
            levelManager = FindObjectOfType<LevelManager>();            
            xMin = levelManager.levelSize.min.x;
            xMax = levelManager.levelSize.max.x;
            yMin = levelManager.levelSize.min.y;
            yMax = levelManager.levelSize.max.y;
            levelManager.SetCamera();
        }

        protected virtual void CreatePlayer(GameObject initialPlayer, Vector3 spawnLocation)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length > 0)
            {
                foreach (var obj in players)
                {
                    Destroy(obj);
                }
            }
            Instantiate(initialPlayer, new Vector3(spawnLocation.x, spawnLocation.y), Quaternion.identity);
            initialPlayer.GetComponent<Character>().InitializePlayer();
        }
    }
}
