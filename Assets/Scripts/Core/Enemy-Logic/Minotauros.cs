using UnityEngine;

namespace Core.Enemy_Logic
{
    public class Minotauros : EnemyAbstract
    {
        protected override void Awake()
        {
            MoveSpeed = 3f;
            AttackPower = 25f;
            MaxHealth = 70f;
            base.Awake(); // currentHealth already declared in the EnemyAbstract
        }
    }
    }
