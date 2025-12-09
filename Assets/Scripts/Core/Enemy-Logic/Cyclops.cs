using UnityEngine;

namespace Core.Enemy_Logic
{
    public class Cyclops : EnemyAbstract
    {
        [Header("Coin")] [SerializeField] GameObject coinPrefab;
        
        [Header("Cyclops Overrides")] 
        [SerializeField] private float cyclopsMoveSpeed = 4f;
        [SerializeField] private float cyclopsAttackPower = 50f;
        [SerializeField] private float cyclopsMaxHealth = 90f;
        [SerializeField] private int cyclopsCoinMin = 20;
        [SerializeField] private int cyclopsCoinMax = 50;
        protected override void Awake()
        { 
            MoveSpeed = cyclopsMoveSpeed;
            AttackPower = cyclopsAttackPower;
            MaxHealth = cyclopsMaxHealth;
            
            base.Awake(); // currentHealth already declared in the EnemyAbstract
        }
        public override IDropable Drop()
        {
            Debug.Log("Cyclops DROP() START");
            GameObject go = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            
            Coin coin = go.GetComponent<Coin>();
            int value = Random.Range(cyclopsCoinMin, cyclopsCoinMax + 1);
            coin.SetValue(value);
            return coin;
        }
    }
    }
    
