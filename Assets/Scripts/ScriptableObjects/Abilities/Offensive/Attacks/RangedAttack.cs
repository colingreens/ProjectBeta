using UnityEngine;
using System;

namespace MetroidVaniaTools
{
    public class RangedAttack : Attack
    {
        [SerializeField]
        private ProjectileConfig projectile;
        public override void Execute(CombatController combat)
        {
            projectile.Fire(combat.playerPosition.facingPosition, combat.projectilePosition.position);
            combat.WeaponCoolDown = projectile.fireRate;
        }
    }
}

