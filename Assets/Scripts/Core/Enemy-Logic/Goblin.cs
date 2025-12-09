using UnityEngine;

namespace Core.Enemy_Logic
{
    public class Goblin : EnemyAbstract
    {
        [Header("Coin")] [SerializeField] GameObject coinPrefab;

        [Header("Goblin Overrides")] [SerializeField]
        private float goblinMoveSpeed = 1f;

        [SerializeField] private float goblinAttackPower = 10f;
        [SerializeField] private float goblinMaxHealth = 50f;
        [SerializeField] private int goblinCoinMin = 3;
        [SerializeField] private int goblinCoinMax = 7;

        protected override void Awake()
        {
            MoveSpeed = goblinMoveSpeed;
            AttackPower = goblinAttackPower;
            MaxHealth = goblinMaxHealth;

            base.Awake(); // currentHealth already declared in the EnemyAbstract
        }

        public override IDropable Drop()
        {
            Debug.Log("Goblin DROP() START");
            GameObject go = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            
            Coin coin = go.GetComponent<Coin>();
            int value = Random.Range(goblinCoinMin, goblinCoinMax + 1);
            coin.SetValue(value);
            return coin;
        }
    }
}