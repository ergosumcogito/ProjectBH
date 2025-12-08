using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private float _moveSpeed = 5f;
    public float moveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    [SerializeField] private float _attackSpeed = 1f;
    public float attackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
    
    [SerializeField] private float _attackRange = 5f;
    public float attackRange { get => _attackRange; set => _attackRange = value; }

    [SerializeField] private float _maxHealth = 100f;
    public float maxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    
    [SerializeField] private int _lvl = 1;
    public int lvl { get { return _lvl; } set { _lvl = value; } }

    [SerializeField] private int _exp = 0;
    public int exp { get { return _exp; } set { _exp = value; } }
    
}
