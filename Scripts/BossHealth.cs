using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header("Vie")]
    public float maxHealth = 500f;
    private float currentHealth;

    [Header("Récompenses")]
    public int pointsOnDeath = 100;

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
