using UnityEngine;

namespace Core.Enemy_Logic
{
    public class Cyclops : EnemyAbstract
    {
        protected override void Awake()
        {
            MoveSpeed = 4f;
            AttackPower = 50f;
            MaxHealth = 90f;
            base.Awake(); // currentHealth already declared in the EnemyAbstract
        }
    }
    }
