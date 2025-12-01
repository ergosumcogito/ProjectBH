using UnityEngine;

namespace Core.Enemy_Logic
{
    public class Goblin : EnemyAbstract
    {
        [Header("Goblin Overrides")] 
        [SerializeField] private float goblinMoveSpeed = 1f;
        [SerializeField] private float goblinAttackPower = 10f;
        [SerializeField] private float goblinMaxHealth = 50f;
        protected override void Awake()
        {
            MoveSpeed = goblinMoveSpeed;
            AttackPower = goblinAttackPower;
            MaxHealth = goblinMaxHealth;
            base.Awake(); // currentHealth already declared in the EnemyAbstract
        }
    }
}