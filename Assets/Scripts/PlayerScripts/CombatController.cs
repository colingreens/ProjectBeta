using UnityEngine;

namespace MetroidVaniaTools
{
    public class CombatController : MonoBehaviour
    {
        public PlayerPosition playerPosition;
        public Transform projectilePosition;        

        [HideInInspector]
        public float WeaponCoolDown = .1f;

        [SerializeField]
        private Attack attack;
        [SerializeField]
        private InputEvent onAttack;

        private void Start()
        {
            onAttack.onKeyPress += CheckInput;
        }

        private void CheckInput()
        {
            if (isShooting)
                return;
            canFire = true;
        }

        private void Attack(IAttack attack)
        {
            if (canFire)
            {
                attack.Execute(this);
                Invoke(nameof(GlobalCoolDown), WeaponCoolDown);
            }
            canFire = false;
        }
        private void GlobalCoolDown()
        {
            isShooting = false;
            canFire = true;
        }

        private bool isShooting;
        private bool canFire;
    }
}
