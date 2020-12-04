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
            if (health.CurrentHealthPoints.floatReference < health.HealthPoints.floatReference)
            {
                health.CurrentHealthPoints.floatReference += amount;
            }
            health.CurrentHealthPoints = health.CurrentHealthPoints.floatReference > health.HealthPoints.floatReference ? health.HealthPoints : health.CurrentHealthPoints;
        }

        public void Damage(int amount)
        {
            print(gameObject.name + $": {amount} damage");
            health.CurrentHealthPoints.floatReference -= amount;
            if (health.CurrentHealthPoints.floatReference < 1)
            {
                KillPlayer();
            }
        }

        public void KillPlayer()
        {
            gameObject.SetActive(false);
            health.CurrentHealthPoints = health.HealthPoints;
        }
    }
}
