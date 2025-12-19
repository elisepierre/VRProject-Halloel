using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 20f;
    private float currentHealth;

    public bool IsDead => currentHealth <= 0f;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        currentHealth -= damage;
        Debug.Log("Boss prend " + damage + " dégâts");

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Boss mort");

        Boss boss = GetComponent<Boss>();
        if (boss != null && boss.endGamePanel != null)
            boss.endGamePanel.SetActive(true);

        Destroy(gameObject);
    }


    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }
}

