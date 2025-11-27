using UnityEngine;

public class ProjectileWeapon : WeaponBase
{
    protected override void Attack(Transform target)
    {
        Vector3 dir = (target.position - transform.position).normalized;

        GameObject projGO = Instantiate(
            weaponConfig.projectilePrefab,
            transform.position,
            Quaternion.identity
        );

        Projectile proj = projGO.GetComponent<Projectile>();

        proj.Init(
            dir,
            weaponConfig.projectileSpeed,
            baseDamage,
            baseCritChance
        );
    }
}