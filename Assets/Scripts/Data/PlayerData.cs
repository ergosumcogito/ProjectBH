using System.Collections.Generic;
using Core.ItemLogic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private float _moveSpeed = 5f;
    public float moveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    [SerializeField] private float _attackSpeed = 1f;
    public float attackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }

    [SerializeField] private float _maxHealth = 100f;
    public float maxHealth { get { return _maxHealth; } set { _maxHealth = value; } }

    [SerializeField] private float _currentHealth;
    public float currentHealth { get { return _currentHealth; } set { if (value > maxHealth) { _currentHealth = maxHealth; } else { _currentHealth = Mathf.Max(value, 0); } } }

    [SerializeField] private int _lvl = 1;
    public int lvl { get { return _lvl; } set { _lvl = value; } }

    [SerializeField] private int _exp = 0;
<<<<<<< Updated upstream
    public int exp { get { return _exp; } set { _exp = value; } }

}
=======
    [SerializeField] private int _coins = 100;

    public List<IItem> items; //Public for now to make access easier as it is a list(or array if that fits better) and not a single value

    public float maxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    public float moveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }

    public float meleeDamage
    {
        get => _meleeDamage;
        set => _meleeDamage = value;
    }

    public float rangedDamage
    {
        get => _rangedDamage;
        set => _rangedDamage = value;
    }

    public float attackSpeed
    {
        get => _attackSpeed;
        set => _attackSpeed = value;
    }

    public float attackRange
    {
        get => _attackRange;
        set => _attackRange = value;
    }

    public float critChance
    {
        get => _critChance;
        set => _critChance = value;
    }

    public int lvl
    {
        get => _lvl;
        set => _lvl = value;
    }

    public int exp
    {
        get => _exp;
        set => _exp = value;
    }
    
    public int coins
    {
        get => _coins;
        set => _coins = value;
    }
}
>>>>>>> Stashed changes
