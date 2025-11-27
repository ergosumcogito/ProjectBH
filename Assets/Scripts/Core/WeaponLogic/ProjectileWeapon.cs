using UnityEngine;

public class ProjectileWeapon : WeaponBase
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;

    protected override void Attack(Transform target)
    {
        Vector3 dir = (target.position - transform.position).normalized;

        GameObject projGO = Instantiate(
            projectilePrefab,
            transform.position,
            Quaternion.identity
        );

        Projectile proj = projGO.GetComponent<Projectile>();

        proj.Init(dir, projectileSpeed, baseDamage, baseCritChance);
    }
}