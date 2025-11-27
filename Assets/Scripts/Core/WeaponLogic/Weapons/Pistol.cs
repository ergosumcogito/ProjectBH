using UnityEngine;

public class Pistol : ProjectileWeapon
{
    private void Start()
    {
        // TODO replace with playerStats
        baseDamage = 10;
        baseAttackSpeed = 1.2f;
        baseAttackRange = 6f;
        baseCritChance = 0.15f;
        projectileSpeed = 16;
    }
}