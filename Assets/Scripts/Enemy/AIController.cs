using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        private MovementConfig _movementConfig;
        [SerializeField]
        private JumpConfig jump;

        [SerializeField]
        private int agroWakeRange = 5;
        [SerializeField]
        private int agroLoseRange = 8;
        [SerializeField]
        private Transform aimPoint;
        [SerializeField]
        private LayerMask playerLayer;

        private Transform target;
        private Rigidbody2D rigidBody;
        private Vector2 startPosition;

        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            rigidBody = GetComponent<Rigidbody2D>();
            startPosition = transform.position;
            

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void FixedUpdate()
        {
            //distance to player
            float distToPlayer = Vector2.Distance(transform.position, target.position);
            float distToSpawn = Vector2.Distance(transform.position, startPosition);

            if (CanDetectTarget(startPosition, distToSpawn, playerLayer))
            {
                MoveToTarget(target.position);
            }

            if (distToPlayer > agroLoseRange)
            {
                if (Vector2.Distance(transform.position, startPosition) <= float.Epsilon)
                {
                    rigidBody.velocity = new Vector2(0f, 0f);
                    _movementConfig.horizontalDirection = 0;
                }
                else
                {
                    MoveToTarget(startPosition);
                }
                
            }
        }

        private void MoveToTarget(Vector2 position)
        {
            if (transform.position.x < position.x)
            {
                //enemy to the left side of player so move right
                rigidBody.velocity = new Vector2(_movementConfig.runSpeed, rigidBody.velocity.y);
                _movementConfig.horizontalDirection = 1;
            }
            else if (transform.position.x > position.x)
            {
                //enemy to the left side of player so move right
                rigidBody.velocity = new Vector2(-_movementConfig.runSpeed, rigidBody.velocity.y);
                _movementConfig.horizontalDirection = -1;
            }            
        }

        private bool CanDetectTarget(Vector3 position, float distance, LayerMask collisionLayer)
        {
            var endPoint = aimPoint.position + Vector3.right * distance * _movementConfig.facingPosition;
            var hit = Physics2D.Linecast(position, endPoint, collisionLayer);
            Debug.DrawLine(position, endPoint, Color.blue);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
            
                
        }
    }
}
