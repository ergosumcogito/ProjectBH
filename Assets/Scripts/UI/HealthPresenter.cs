using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthPresenter : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;

    private float currentHealth = 100f;
    private float maxHealth = 100f;

    private void Start()
    {
        healthBar.Init(maxHealth);

        StartCoroutine(FakeDamage());
    }

    // Method for testing HP change
    private IEnumerator FakeDamage()
    {
        while (currentHealth > 0)
        {
            currentHealth -= Time.deltaTime * 10f;
            healthBar.UpdateHealth(currentHealth);
            yield return null;
        }
    }
    
    // Later replace this with actual PlayerStats health updates
    // void OnHealthChanged(float newHealth) { healthBarInstance.UpdateHealth(newHealth); }    
}