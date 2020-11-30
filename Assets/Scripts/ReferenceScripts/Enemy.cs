using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class Enemy : MovingObject
    {
        private Transform target;
        private bool skipMove;
        private int damage;

        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            base.Start();
        }

        protected override void AttemptMove<T>(int xDir, int yDir)
        {
            if (skipMove)
            {
                skipMove = false;
                return;
            }
            
            base.AttemptMove<T>(xDir, yDir);
            skipMove = false;
        }

        public void MoveEnemy()
        {
            int xDir = 0;
            int yDir = 0;

            if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
            {
                yDir = target.position.y > transform.position.y ? 1 : -1;
            }
            else
            {
                xDir = target.position.x > transform.position.x ? 1 : -1;
            }
            AttemptMove<HealthController>(xDir, yDir);
        }

        protected override void OnCantMove<T>(T component)
        {
            //Declare hitPlayer and set it to equal the encountered component.
            HealthController hitPlayer = component as HealthController;

            //Call the LoseFood function of hitPlayer passing it playerDamage, the amount of foodpoints to be subtracted.
            hitPlayer.Damage(damage);

           //set amimator to attack

        }
    }
}
