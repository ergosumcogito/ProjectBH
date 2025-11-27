using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Runtime Stats")]
    protected float baseCritChance;
    protected float baseDamage;
    protected float baseAttackRange;
    protected float baseAttackSpeed;

    protected float attackCooldown;
    protected AutoAim autoAim;

    public string weaponName;

    [Header("Config Reference")]
    public WeaponsData weaponsData;

    protected WeaponStats weaponConfig;

    // TODO Temporary player stats placeholder
    protected float playerRangedDamage = 10f;
    protected float playerAttackSpeed = 1f;
    protected float playerAttackRange = 4f;
    protected float playerCritChance = 0.05f;

    protected virtual void Awake()
    {
        autoAim = GetComponentInParent<AutoAim>();

        weaponConfig = weaponsData.GetWeaponByName(weaponName);

        if (weaponConfig == null)
        {
            Debug.LogError($"Weapon {weaponName} not found in WeaponsData!");
            return;
        }

        ApplyStats();
    }

    protected virtual void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (attackCooldown <= 0f)
            TryAttack();
    }

    protected virtual void TryAttack()
    {
        Transform target = autoAim.GetClosestEnemy();
        if (target == null) return;

        Attack(target);
        attackCooldown = 1f / baseAttackSpeed;
    }

    protected abstract void Attack(Transform target);

    protected void ApplyStats()
    {
        // Base damage derived from player stats and weapon scaling
        baseDamage = playerRangedDamage * weaponConfig.rangedDamageScale;

        // Attack speed
        baseAttackSpeed = playerAttackSpeed * weaponConfig.attackSpeedScale;

        // Range
        baseAttackRange = playerAttackRange * weaponConfig.attackRangeScale;

        // Crit chance
        baseCritChance = playerCritChance * weaponConfig.critChanceScale;
    }
}