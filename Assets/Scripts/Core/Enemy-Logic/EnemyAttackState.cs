using UnityEngine;

namespace Core.Enemy_Logic
{
    public class EnemyAttackState : EnemyBaseState
    {
        private PlayerObject _playerObject;
        private float attackCooldown = 1.2f;
        private float lastAttackTime = 0f;
        public override void EnterState(EnemyStateManager manager,EnemyAbstract enemy)
        {
           // Debug.Log("Enemy entered Attack State");
           //Debug.Log("Enemy entered Attack State");
           _playerObject = enemy.Player.GetComponent<PlayerObject>();
        }

        public override void UpdateState(EnemyStateManager manager,EnemyAbstract enemy)
        {
            if (enemy.IsDead)
            {
                manager.SwitchState(manager.EnemyDeathState);
                return;
            }
            float distance = Vector2.Distance(enemy.transform.position, enemy.Player.position);

            if (distance > 1.5f)
            {
                manager.SwitchState(manager.EnemyChaseState);
                return;
            }

            if (Time.time - lastAttackTime > attackCooldown)
            {
                lastAttackTime = Time.time;
                /*Debug.Log("Enemy attacks!");
                //healthPlayer.TakeDamage(enemy.attackPower);
                float playerHealth = _playerObject.PlayerHealth;
                Debug.Log("Start Health:"+playerHealth);
                playerHealth -= enemy.attackPower;
                _playerObject.PlayerHealth = playerHealth;
                Debug.Log("Current Health:"+playerHealth);*/
                _playerObject.TakeDamage(enemy.attackPower);
            }
        }
        

        public override void OnCollisionEnter(EnemyStateManager manager,EnemyAbstract enemy)
        {
            // not needed atm
        }
    }
}