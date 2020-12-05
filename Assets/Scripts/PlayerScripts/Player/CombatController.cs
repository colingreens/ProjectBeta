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
        private Attack mainAttack;
        [SerializeField]
        private Attack offAttack;
        [SerializeField]
        private InputEvent onMainAttack;
        [SerializeField]
        private InputEvent onOffAttack;


        private void Start()
        {
            onMainAttack.onKeyPress += onMainInputPress;
            onOffAttack.onKeyPress += onOffHandInputPress;
        }

        private void onMainInputPress()
        {
            if (isShooting)
                return;
            Attack(mainAttack);
        }
        private void onOffHandInputPress()
        {
            if (isShooting)
                return;
            Attack(offAttack);
        }

        private void Attack(IAttack attack)
        {
             attack.Execute(this);
             Invoke(nameof(GlobalCoolDown), WeaponCoolDown);
        }
        private void GlobalCoolDown()
        {
            isShooting = false;
        }

        private bool isShooting;
    }
}
