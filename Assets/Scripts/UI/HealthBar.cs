using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private Transform target;
    private float maxHealth = 100f;
    private float currentHealth = 100f;
    
    public void Init(float maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        UpdateUI();
    }
    
    public void UpdateHealth(float newHealth)
    {
        currentHealth = Mathf.Clamp(newHealth, 0, maxHealth);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (fillImage != null)
            fillImage.fillAmount = currentHealth / maxHealth;
    }
}