using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class ReturnHomeState : AIState
    {
        public ReturnHomeState(AIBrain simpleAI) :base(simpleAI)
        {

        }

        private Vector3 destination;
        
        public override void Tick()
        {
            //simpleAI.MoveToward(destination);

            if (ReachedHome())
            {
                simpleAI.SetState(new WanderState(simpleAI));
            }
        }

        public override void OnStateEnter()
        {
            simpleAI.GetComponent<SpriteRenderer>().material.color = Color.blue;
        }

        private bool ReachedHome()
        {
            return Vector2.Distance(simpleAI.transform.position, destination) < 0.5f;
        }
    }
}
