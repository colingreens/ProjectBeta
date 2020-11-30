using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MetroidVaniaTools
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField]
        private HealthConfig health;

        public void Heal(int amount)
        {
            if (health.CurrentHealthPoints < health.HealthPoints)
            {
                health.CurrentHealthPoints += amount;
            }
            health.CurrentHealthPoints = health.CurrentHealthPoints > health.HealthPoints ? health.HealthPoints : health.CurrentHealthPoints;
        }

        public void Damage(int amount)
        {
            print(gameObject.name + $": {amount} damage");
            health.CurrentHealthPoints -= amount;
            if (health.CurrentHealthPoints < 1)
            {
                KillPlayer();
            }
        }

        public void KillPlayer()
        {
            gameObject.SetActive(false);
        }
    }
}
