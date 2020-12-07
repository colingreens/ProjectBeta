using UnityEngine;

namespace MetroidVaniaTools
{
    public class CombatController : MonoBehaviour
    {
        public FloatVariable facingPosition;
        public Transform projectilePosition;
        [HideInInspector]
        public float WeaponCoolDown = .1f; //global cooldown, can be set by weapon.
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
            onMainAttack.onKeyPress += MainInputPress;
            onOffAttack.onKeyPress += OffHandInputPress;
        }

        private void MainInputPress()
        {
            if (isShooting)
                return;
            Attack(mainAttack);
        }
        private void OffHandInputPress()
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
