using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class AnimationController : MonoBehaviour, IAnimationController
    {
        [SerializeField]
        private PlayerCharacter character;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimations();
        }

        private void UpdateAnimations()
        {
            //anim.SetBool("isWalking", isWalking);
            //anim.SetBool("isGrounded", isGrounded);
            //anim.SetFloat("yVelocity", rb.velocity.y);
            //anim.SetBool("isWallSliding", isWallSliding);
            //anim.SetBool("Crouching", isCrouching);
            //anim.SetBool("Grounded", isGrounded);
            //anim.SetFloat("VerticalSpeed", rb.velocity.y);
        }
    }
}
