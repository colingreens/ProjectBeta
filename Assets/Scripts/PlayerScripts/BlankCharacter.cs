using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class BlankCharacter : MonoBehaviour
    {
        [SerializeField]
        private PlayerBase playerBase; //container for RB and Collider

        private void Awake()
        {
            playerBase.transform = GetComponent<Transform>();
            playerBase.rigidBody = GetComponent<Rigidbody2D>();            
            playerBase.collider = GetComponent<BoxCollider2D>();
        }
    }
}
