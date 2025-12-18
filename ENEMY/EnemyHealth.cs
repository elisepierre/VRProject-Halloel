using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 1f;
    public float currentHealth;
    public int pointsOnDeath = 1;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Enemy HP = " + currentHealth);

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

        ChallengeManager cm = Object.FindFirstObjectByType<ChallengeManager>();
        if (cm != null)
        {
            cm.EnemyKilled();
        }

        Destroy(gameObject);
    }
}
