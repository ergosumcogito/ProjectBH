namespace Core.Enemy_Logic
{
    public class Goblin : EnemyAbstract
    {
        protected override void Awake()
        {
            MoveSpeed = 2f;
            AttackPower = 10f;
            MaxHealth = 50f;
            base.Awake(); // currentHealth already declared in the EnemyAbstract
        }
    }
}