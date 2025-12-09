using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public int pointsOnDeath = 10;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(pointsOnDeath);
        }

        ChallengeManager cm = FindObjectOfType<ChallengeManager>();
        if (cm != null)
        {
            cm.EnemyKilled();
        }

        Destroy(gameObject);
    }
}
