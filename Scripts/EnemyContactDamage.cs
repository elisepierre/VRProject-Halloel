using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    public float damage = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
