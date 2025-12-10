using UnityEngine;

namespace Core.Enemy_Logic
{
    public class Minotauros : EnemyAbstract
    {
        [Header("Coin")] [SerializeField] GameObject coinPrefab;
        [Header("Minotauros Overrides")] 
        [SerializeField] private float minotaurosMoveSpeed = 3f;
        [SerializeField] private float minotaurosAttackPower = 25f;
        [SerializeField] private float minotaurosMaxHealth = 70f;
        [SerializeField] private int minotaurosCoinMin = 10;
        [SerializeField] private int minotaurosCoinMax = 20;
        protected override void Awake()
        { 
            MoveSpeed = minotaurosMoveSpeed;
            AttackPower = minotaurosAttackPower;
            MaxHealth = minotaurosMaxHealth;
            
            base.Awake(); // currentHealth already declared in the EnemyAbstract
        }
        public override IDropable Drop()
        {
            Debug.Log("Minotauros DROP() START");
            GameObject go = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            
            Coin coin = go.GetComponent<Coin>();
            int value = Random.Range(minotaurosCoinMin, minotaurosCoinMax + 1);
            coin.SetValue(value);
            return coin;
        }
    }
    }
