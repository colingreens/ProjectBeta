using UnityEngine;

namespace MetroidVaniaTools
{
    public class OrientationController : MonoBehaviour
    {
        [SerializeField]
        private MovementConfig movementInfo;    
        private Transform transform;

        private const int FacingRight = 1;
        private const int FacingLeft = -1;

        // Start is called before the first frame update
        private void Start()
        {
            transform = GetComponent<Transform>();
        }

        private void Update()
        {
            CheckDirection();
        }

        private void CheckDirection()
        {
            if (movementInfo.horizontalDirection == 0)
            {
                return;
            }
            if (movementInfo.horizontalDirection > 0)
            {
                if (transform.localScale.x < 0f)
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                movementInfo.facingPosition = FacingRight;
            }

            if (movementInfo.horizontalDirection < 0)
            {
                if (transform.localScale.x > 0f)
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                movementInfo.facingPosition = FacingLeft;
            }
        }
    }
}
