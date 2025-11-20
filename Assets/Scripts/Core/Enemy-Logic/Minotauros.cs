using UnityEngine;

namespace Core.Enemy_Logic
{
    public class Minotauros : EnemyAbstract
    {
        [Header("Minotauros Overrides")] 
        [SerializeField] private float minotaurosMoveSpeed = 3f;
        [SerializeField] private float minotaurosAttackPower = 25f;
        [SerializeField] private float minotaurosMaxHealth = 70f;
        protected override void Awake()
        { 
            MoveSpeed = minotaurosMoveSpeed;
            AttackPower = minotaurosAttackPower;
            MaxHealth = minotaurosMaxHealth;
            
            base.Awake(); // currentHealth already declared in the EnemyAbstract
        }
    }
    }
