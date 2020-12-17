using UnityEngine;

namespace MetroidVaniaTools
{
    public class OrientationController : MonoBehaviour
    {
        [SerializeField]
        private FloatVariable horizontalDirection;
        [SerializeField]
        private FloatVariable facingDirection;
        [SerializeField]
        private BoolVariable isGrounded;
        [SerializeField]
        private BoolVariable isWallSliding;
        [SerializeField]
        private VelocityVariable velocity;
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
            SetDirection();
        }

        private void SetDirection()
        {
            if (horizontalDirection.Value == 0)
            {
                return;
            }
            if (horizontalDirection.Value > 0)
            {
                if (position.localScale.x < 0f)
                    position.localScale = new Vector3(-position.localScale.x, position.localScale.y, position.localScale.z);
                facingDirection.Value = FacingRight;
            }

            if (horizontalDirection.Value < 0)
            {
                if (position.localScale.x > 0f)
                    position.localScale = new Vector3(-position.localScale.x, position.localScale.y, position.localScale.z);
                facingDirection.Value = FacingLeft;
            }
        }
    }
}
