using UnityEngine;

namespace Core.Enemy_Logic
{
    public abstract class EnemyAbstract : MonoBehaviour
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
            stateManager = GetComponent<EnemyStateManager>(); // get the current child instance of enemy
            currentHealth = MaxHealth;

            // Null-check => Ensures this GameObject has EnemyStateManager attached in Unity
             
            if (stateManager == null)
            {
                Debug.LogError($"{name} has no EnemyStateManager attached!");
            }
            
            // Check if base stats are set in children classes

            if (MaxHealth <= 0 || MoveSpeed <= 0 || AttackPower <= 0)
            {
                Debug.LogWarning(
                    $"{name} has uninitialized base stats!" +
                    $"[MaxHealth={MaxHealth}, MoveSpeed={MoveSpeed},AttackPower={AttackPower}]" +
                    $"Check child class!");
            }
            
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