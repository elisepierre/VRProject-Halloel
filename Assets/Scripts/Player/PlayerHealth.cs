using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    public Slider healthSlider;
    public Image healthFill;
    public float lerpSpeed = 5f;

    [Header("Damage Settings")]
    public float contactDamage = 5f;
    public float contactInterval = 2f;

    private Dictionary<Collider, float> contactTimers = new Dictionary<Collider, float>();

    private void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }

    private void Update()
    {
        if (healthSlider != null)
            healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth, Time.deltaTime * lerpSpeed);

        List<Collider> keys = new List<Collider>(contactTimers.Keys);
        foreach (var enemyCollider in keys)
        {
            contactTimers[enemyCollider] -= Time.deltaTime;
            if (contactTimers[enemyCollider] <= 0f)
            {
                TakeDamage(contactDamage);
                contactTimers[enemyCollider] = contactInterval; 
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            TakeDamage(contactDamage);
            if (!contactTimers.ContainsKey(other))
                contactTimers.Add(other, contactInterval);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            if (contactTimers.ContainsKey(other))
                contactTimers.Remove(other);
        }
    }


    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
