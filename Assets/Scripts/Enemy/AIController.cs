using UnityEngine;

namespace MetroidVaniaTools
{
    public class AIController : MonoBehaviour
    {
              
        [SerializeField]
        private Transform _aimPoint;
        [SerializeField]
        private LayerMask _targetLayer;
        [SerializeField]
        private bool _canPatrol;
        [SerializeField]
        private float _agroLoseRange;

        private Transform _target;
        private Rigidbody2D _rigidBody;
        private Vector2 _startPosition;

        private float _runSpeed;
        private float _horizontalDirection;
        private float _facingDirection;
        private enum AIState
        {
            Asleep, Patrol, ChasePlayer, ReturnToSpawn
        }
        private AIState currentState;

        // Start is called before the first frame update
        void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            _rigidBody = GetComponent<Rigidbody2D>();
            _startPosition = transform.position;  
        }

        private void FixedUpdate()
        {

            DetermineState();

            switch (currentState)
            {
                case AIState.Asleep:
                    break;
                case AIState.Patrol:
                    SendOnPatrol();
                    break;
                case AIState.ChasePlayer:
                    break;
                case AIState.ReturnToSpawn:
                    ReturnToSpawn();
                    break;
                default:
                    break;
            }  
        }

        private void DetermineState()
        {
            //distance to player
            float distToPlayer = Vector2.Distance(transform.position, _target.position);
            float distToSpawn = Vector2.Distance(transform.position, _startPosition);

            if (CanDetectTarget(_startPosition, distToSpawn, _targetLayer))
            {
                currentState = AIState.ChasePlayer;
                MoveToTarget(_target.position);
            }

            if (distToPlayer > _agroLoseRange)
            {
                if (_canPatrol)
                {
                    currentState = AIState.Patrol;
                }
                else
                {
                    ReturnToSpawn();
                }
            }
            if (_canPatrol || currentState == AIState.Patrol)
            {
                currentState = AIState.Patrol;
            }
        }

        private void MoveToTarget(Vector2 position)
        {
            if (transform.position.x < position.x)
            {
                //enemy to the left side of player so move right
                _rigidBody.velocity = new Vector2(_runSpeed, _rigidBody.velocity.y);
                _horizontalDirection = 1;
            }
            else if (transform.position.x > position.x)
            {
                //enemy to the left side of player so move right
                _rigidBody.velocity = new Vector2(-_runSpeed, _rigidBody.velocity.y);
                _horizontalDirection = -1;
            }            
        }

        private bool CanDetectTarget(Vector3 position, float distance, LayerMask collisionLayer)
        {
            var endPoint = _aimPoint.position + Vector3.right * distance * _facingDirection;
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

        private void ReturnToSpawn()
        {
            if (Vector2.Distance(transform.position, _startPosition) <= float.Epsilon)
            {
                _rigidBody.velocity = new Vector2(0f, 0f);
                _horizontalDirection = 0;
                currentState = AIState.Asleep;
            }
            else
            {
                MoveToTarget(_startPosition);
            }
        }

        private void SendOnPatrol()
        {

        }
    }
}
