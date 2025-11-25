using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damagePerSecond = 10f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health h = other.GetComponent<Health>();
            if (h != null)
                h.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}