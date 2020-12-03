using UnityEngine;

namespace MetroidVaniaTools
{
    public class OrientationController : MonoBehaviour
    {
        [SerializeField]
        private MovementConfig movementInfo;
        [SerializeField]
        private PlayerPosition positionInfo;
        private Transform position;

        private const int FacingRight = 1;
        private const int FacingLeft = -1;

        // Start is called before the first frame update
        private void Start()
        {
            position = GetComponent<Transform>();
        }

        private void Update()
        {
            CheckDirection();
        }

        private void CheckDirection()
        {
            if (positionInfo.horizontalDirection == 0)
            {
                return;
            }
            if (positionInfo.horizontalDirection > 0)
            {
                if (position.localScale.x < 0f)
                    position.localScale = new Vector3(-position.localScale.x, position.localScale.y, position.localScale.z);
                positionInfo.facingPosition = FacingRight;
            }

            if (positionInfo.horizontalDirection < 0)
            {
                if (position.localScale.x > 0f)
                    position.localScale = new Vector3(-position.localScale.x, position.localScale.y, position.localScale.z);
                positionInfo.facingPosition = FacingLeft;
            }
        }
    }
}
