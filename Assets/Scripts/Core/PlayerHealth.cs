using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerData playerData;  

    void Start()
    {
        playerData.currentHealth = playerData.maxHealth;
    }

    public void TakeDamage(float amount)
    {
        float health = playerData.currentHealth;
        health -= amount;
        playerData.currentHealth = health;
        Debug.Log("Damage has beend taken Amount: " + amount + " Health left: " + health);
        if (health <= 0f) { Destroy(gameObject); }        
    }
}
