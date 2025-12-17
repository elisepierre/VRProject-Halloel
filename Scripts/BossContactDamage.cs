using UnityEngine;

public class BossContactDamage : MonoBehaviour
{
    public float damage = 35f;

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
