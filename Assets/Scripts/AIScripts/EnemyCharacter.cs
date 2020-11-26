using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class EnemyCharacter : Character
    {
        [HideInInspector]
        public bool followPlayer;
        [HideInInspector]
        public bool playerIsClose;

        protected GameObject playerCharacter;
        protected Collider2D playerCollider;
        protected EnemyMovement enemyMovement;

        private Vector2 facingLeft;
        protected int rayHitNumber;
        public float originalTimeTillDoAction;
        protected float timeTillDoAction;

        // Start is called before the first frame update
        void Start()
        {
            Initialization();
        }

        protected virtual void Initialization()
        {
            player = FindObjectOfType<PlayerCharacter>().gameObject;
            playerCollider = player.GetComponent<Collider2D>();
        }        
    }
}
