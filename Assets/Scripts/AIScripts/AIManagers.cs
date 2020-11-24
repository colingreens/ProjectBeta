using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class AIManagers : EnemyCharacter
    {
        protected EnemyCharacter enemyCharacter;

        protected override void Initilization()
        {
            base.Initilization();
            enemyCharacter = GetComponent<EnemyCharacter>();
        }
    }
}
