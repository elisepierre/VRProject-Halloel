using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
    public float damage = 10f;
    private Coroutine damageCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damageCoroutine = StartCoroutine(DamageOverTime(other.GetComponent<PlayerHealth>()));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }
    }

    private IEnumerator DamageOverTime(PlayerHealth playerHealth)
    {
        while (true)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
