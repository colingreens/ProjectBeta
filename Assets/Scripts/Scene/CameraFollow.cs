using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        protected GameObject followObject;
        [SerializeField]
        protected Vector2 followOffset;
        [SerializeField]
        protected float followSpeed;

        private Vector2 threshold;
        private Rigidbody2D rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = followObject.GetComponent<Rigidbody2D>();
            threshold = CalculateThreshold();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 follow = followObject.transform.position;
            float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
            float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

            Vector3 newPosition = transform.position;
            if (Mathf.Abs(xDifference) >= threshold.x)
            {
                newPosition.x = follow.x;
            }

            if (Mathf.Abs(yDifference) >= threshold.y)
            {
                newPosition.y = follow.y;
            }
            float moveSpeed = rb.velocity.magnitude > followSpeed ? rb.velocity.magnitude : followSpeed;

            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
        }

        private Vector3 CalculateThreshold()
        {
            Rect aspect = Camera.main.pixelRect;
            Vector2 thresholdLocal = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
            thresholdLocal.x -= followOffset.x;
            thresholdLocal.y -= followOffset.y;

            return thresholdLocal;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Vector2 border = CalculateThreshold();
            Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
        }
    }
}