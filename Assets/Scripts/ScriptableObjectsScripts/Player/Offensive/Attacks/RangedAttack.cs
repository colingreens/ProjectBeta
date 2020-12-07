using UnityEngine;

namespace MetroidVaniaTools
{
    [CreateAssetMenu(fileName = "RangedAttack", menuName = "Metroidvania/Attacks/Ranged", order = 2)]
    public class RangedAttack : Attack
    {
        [SerializeField]
        private ProjectileConfig projectile;
        public override void Execute(CombatController combat)
        {
            projectile.Fire(combat.facingPosition.Value, combat.projectilePosition.position);
            combat.WeaponCoolDown = projectile.fireRate;
        }
    }
}

