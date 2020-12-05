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
        private HealthConfig config;
        public void Heal(int amount)
        {
            if (config.CurrentHealthPoints.Value < config.HealthPoints.Value)
            {
                config.CurrentHealthPoints.Value += amount;
            }
            config.CurrentHealthPoints = config.CurrentHealthPoints.Value > config.HealthPoints.Value ? config.HealthPoints : config.CurrentHealthPoints;
        }

        public void Damage(int amount)
        {
            print(gameObject.name + $": {amount} damage");
            config.CurrentHealthPoints.Value -= amount;
            if (config.CurrentHealthPoints.Value < 1)
            {
                KillPlayer();
            }
        }

        public void KillPlayer()
        {
            gameObject.SetActive(false);
            config.CurrentHealthPoints = config.HealthPoints;
        }
    }
}
