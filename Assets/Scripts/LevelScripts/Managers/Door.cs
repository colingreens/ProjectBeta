using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class Door : Managers
    {
        [SerializeField]
        protected string[] tagsToOpen;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (tagsToOpen.Contains(collision.gameObject.tag))  
            {
                GetComponent<Collider2D>().enabled = false;
                GetComponent<Animator>().SetBool("Open", true);
            }

        }
    }
}
