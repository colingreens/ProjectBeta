using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MetroidVaniaTools
{
    public abstract class MovingObject : MonoBehaviour
    {
        [SerializeField]
        private PlayerBase playerBase;
        [SerializeField]
        private float moveTime = 0.1f;
        [SerializeField]
        private LayerMask collisionLayer;

        private float inverseMoveTime;

        protected virtual void Start()
        {
            inverseMoveTime = 1f / moveTime;
        }

        protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
        {
            Vector2 start = transform.position;
            var end = start + new Vector2(xDir, yDir);
            playerBase.collider.enabled = false;
            hit = Physics2D.Linecast(start, end, collisionLayer);
            playerBase.collider.enabled = true;

            if (hit.transform == null)
            {
                StartCoroutine(SmoothMovement(end));
                return true;
            }
            return false;
        }

        protected IEnumerator SmoothMovement(Vector3 end)
        {
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            while (sqrRemainingDistance > float.Epsilon)
            {
                var newPosition = Vector3.MoveTowards(playerBase.rigidBody.position, end, inverseMoveTime * Time.deltaTime);
                playerBase.rigidBody.MovePosition(newPosition);
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;

                yield return null;
            }
        }

        protected virtual void AttemptMove <T> (int xDir, int yDir)
            where T : Component //Generic T to specify the type of component we expect to interact with if blocked (Player for enemies, wall for player)
        {
            RaycastHit2D hit;
            bool canMove = Move(xDir, yDir, out hit);

            if (hit.transform == null)
            {
                return;
            }

            T hitComponent = hit.transform.GetComponent<T>();
            if (!canMove && hitComponent != null)
            {
                OnCantMove(hitComponent);
            }
        }

        protected abstract void OnCantMove<T>(T component)
        where T : Component;



    }
}
