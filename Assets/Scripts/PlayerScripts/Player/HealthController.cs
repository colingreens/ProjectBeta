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
        private  FloatVariable HealthPoints;
        [SerializeField]
        private FloatVariable CurrentHealthPoints;
        public void Heal(int amount)
        {
            if (CurrentHealthPoints.Value < HealthPoints.Value)
            {
                CurrentHealthPoints.Value += amount;
            }
            CurrentHealthPoints = CurrentHealthPoints.Value > HealthPoints.Value ? HealthPoints : CurrentHealthPoints;
        }

        public void Damage(int amount)
        {
            print(gameObject.name + $": {amount} damage");
            CurrentHealthPoints.Value -= amount;
            if (CurrentHealthPoints.Value < 1)
            {
                KillPlayer();
            }
        }

        public void KillPlayer()
        {
            gameObject.SetActive(false);
            CurrentHealthPoints = HealthPoints;
        }
    }
}
