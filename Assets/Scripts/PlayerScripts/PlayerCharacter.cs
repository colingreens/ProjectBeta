using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{    
    public class PlayerCharacter : Character
    {
        protected Character character;
        protected override void Initilization()
        {
            base.Initilization();
            character = GetComponent<Character>();
        }
    }
}
