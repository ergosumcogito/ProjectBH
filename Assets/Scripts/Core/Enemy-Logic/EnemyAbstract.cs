using UnityEngine;

namespace Core.Enemy_Logic
{
    public class EnemyAbstract : MonoBehaviour
    {
        protected EnemyStateManager stateManager;
        
        // Stats must be implemented by the children classes - attributes loaded with default values
        [Header("Base Stats")] 
        public  float MaxHealth{get; protected set; } = -1f;
        public  float MoveSpeed{get; protected set; } = -1f;
        public  float AttackPower{get; protected set; } = -1f;

        [Header("References")] 
        public Transform Player { get; protected set; } // is used by the Spawner

        protected float currentHealth;
        
        
        protected virtual void Awake()
        {
            currentHealth = MaxHealth;
            stateManager = GetComponent<EnemyStateManager>(); // get the current child instance of enemy
        }

        protected virtual void Start()
        {
            Init(Player);
        }
         
        
        
        protected virtual void Update()
        {
            stateManager?.Update();
        }

        
        public virtual void Init(Transform player)
        {
            Player = player;
        }
        
/*
 * TakeDamage is not a state itself but contributes to change of state gradually, therefore inside the Die() Method
 * The DeathState is called
 */
        public void TakeDamage(float amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0f)
            {
                Die();
            }
        }

        public void Die()
        {
            stateManager?.SwitchState(stateManager.EnemyDeathState);
        }
        
        public bool IsDead => currentHealth <= 0f;
        
    }
    
}