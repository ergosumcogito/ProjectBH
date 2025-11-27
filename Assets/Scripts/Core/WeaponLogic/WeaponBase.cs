using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Base Stats")]
    public float baseDamage = 10f;
    public float baseAttackSpeed = 1f;
    public float baseAttackRange = 4f;
    public float baseCritChance = 0.1f;

    protected float attackCooldown;

    protected AutoAim autoAim;

    protected virtual void Awake()
    {
        autoAim = GetComponentInParent<AutoAim>();

        // TODO: replace test stats to actual once from PlayerStats
        // baseDamage *= playerStats.RangeDamageMultiplier;
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
}
