using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MetroidVaniaTools
{
    public abstract class AIState
    {
        public AIState(AIBrain ai)
        {
            simpleAI = ai;
        }

        protected AIBrain simpleAI;
        public abstract void Tick();

        public virtual void OnStateEnter() { }

        public virtual void OnStateExit() { }

        
    }
}
