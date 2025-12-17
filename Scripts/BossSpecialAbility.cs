using UnityEngine;
using System.Collections;

public class BossSpecialAbility : MonoBehaviour
{
    public float cooldown = 6f;
    public float radius = 7f;
    public float damage = 25f;

    void Start()
    {
        StartCoroutine(AbilityLoop());
    }

    IEnumerator AbilityLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldown);
            UseAbility();
        }
    }

    void UseAbility()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in hits)
        {
            PlayerHealth player = hit.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }

        Debug.Log("Boss utilise capacite speciale");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
