using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public float stunRadius = 7f;
    public float stunDuration = 3f;
    public float stunCooldown = 10f;

    private float cooldownTimer = 0f;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (UnityEngine.Input.GetMouseButtonDown(1) && cooldownTimer <= 0f)
        {
            StunEnemies();
            cooldownTimer = stunCooldown;
        }
    }

    void StunEnemies()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, stunRadius);
        foreach (var hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Stun(stunDuration);
            }
        }

        Debug.Log("Stun appliqué aux ennemis à moins de " + stunRadius + "m !");
    }
}
