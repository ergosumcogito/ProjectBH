using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    protected WeaponStats weaponConfig;

    [HideInInspector] public float baseCritChance;
    [HideInInspector] public float baseDamage;
    [HideInInspector] public float baseAttackRange;
    [HideInInspector] public float baseAttackSpeed;

    protected float attackCooldown;
    protected AutoAim autoAim;

    // TODO Temporary player stats
    protected float playerRangedDamage = 10f;
    protected float playerAttackSpeed = 1f;
    protected float playerAttackRange = 4f;
    protected float playerCritChance = 0.05f;

    protected virtual void Awake()
    {
        autoAim = GetComponentInParent<AutoAim>();
    }

    public virtual void Init(WeaponStats stats)
    {
        weaponConfig = stats;
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
        baseDamage       = playerRangedDamage * weaponConfig.rangedDamageScale;
        baseAttackSpeed  = playerAttackSpeed * weaponConfig.attackSpeedScale;
        baseAttackRange  = playerAttackRange * weaponConfig.attackRangeScale;
        baseCritChance   = playerCritChance * weaponConfig.critChanceScale;
    }
    
    
    // Base method for damage calculation, can be overriden
    public virtual float CalculateDamage()
    {
        float dmg = baseDamage;
        return CalculateCrit(dmg);
    }
    
    // Base method for crit calculation, can be overriden
    public virtual float CalculateCrit(float damage)
    {
        bool isCrit = Random.value < baseCritChance;
        return isCrit ? damage * 2f : damage;
    }
}