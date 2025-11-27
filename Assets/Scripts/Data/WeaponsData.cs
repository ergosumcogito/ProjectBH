using UnityEngine;

[System.Serializable]
public class WeaponStats
{
    public string weaponName;

    [Header("Scaling Multipliers")]
    public float meleeDamageScale;
    public float rangedDamageScale;
    public float attackSpeedScale;
    public float attackRangeScale;
    public float critChanceScale;

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public float projectileSpeed;
}

[CreateAssetMenu(fileName = "WeaponsData", menuName = "Scriptable Objects/WeaponsData")]
public class WeaponsData : ScriptableObject
{
    public WeaponStats[] allWeapons;
    
    public WeaponStats GetWeaponByName(string weaponName)
    {
        foreach (var w in allWeapons)
        {
            if (w.weaponName == weaponName) return w;
        }
        return null;
    }
}