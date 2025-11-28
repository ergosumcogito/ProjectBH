using System.Collections;
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
    
    protected SpriteRenderer sr;
    private Color originalColor;

    // TODO Temporary player stats
    protected float playerRangedDamage = 10f;
    protected float playerAttackSpeed = 1f;
    protected float playerAttackRange = 4f;
    protected float playerCritChance = 0.05f; // 5% crit

    protected virtual void Awake()
    {
        autoAim = GetComponentInParent<AutoAim>();
        
        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
            sr = GetComponentInChildren<SpriteRenderer>();

        if (sr != null)
            originalColor = sr.color;
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

        if (isCrit)
        {
            FlashCritColor();
            return damage * 2f;
        }

        return damage;
    }
    
    private void FlashCritColor()
    {
        if (sr != null)
        {
            StopAllCoroutines();
            StartCoroutine(CritFlashCoroutine());
        }
    }
    
    private IEnumerator CritFlashCoroutine()
    {
        sr.color = Color.yellow;
        yield return new WaitForSeconds(0.45f); // crit animation duration
        sr.color = originalColor;
    }
    
}