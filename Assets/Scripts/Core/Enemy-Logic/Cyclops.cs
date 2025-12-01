using UnityEngine;

namespace Core.Enemy_Logic
{
    public class Cyclops : EnemyAbstract
    {
        [Header("Cyclops Overrides")] 
        [SerializeField] private float cyclopsMoveSpeed = 4f;
        [SerializeField] private float cyclopsAttackPower = 50f;
        [SerializeField] private float cyclopsMaxHealth = 90f;
        protected override void Awake()
        { 
            MoveSpeed = cyclopsMoveSpeed;
            AttackPower = cyclopsAttackPower;
            MaxHealth = cyclopsMaxHealth;
            
            base.Awake(); // currentHealth already declared in the EnemyAbstract
        }
    }
    }
