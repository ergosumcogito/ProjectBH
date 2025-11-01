namespace Core.Enemy_Logic
{
    /*
     * The abstract Enemy BaseState basically serves as abstract State of the specified States e.g. Idle, Chase, Death etc.
     */
    public abstract class EnemyBaseState
    {
        public abstract void EnterState(EnemyStateManager manager, EnemyAbstract enemy);
        public abstract void UpdateState(EnemyStateManager manager,EnemyAbstract enemy);
        public abstract void OnCollisionEnter(EnemyStateManager manager,EnemyAbstract enemy); 
    }
}
