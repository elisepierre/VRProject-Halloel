using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public Transform player;
    public float contactDamage = 1f;

    [Header("NavMesh")]
    public float speed = 3f;
    public float stoppingDistance = 1f;

    [Header("Fall Damage")]
    public float lethalFallHeight = 5f;
    private float lastY;

    private NavMeshAgent agent;
    private bool isStunned = false;
    private float stunTimer = 0f;
    private Rigidbody rb;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.stoppingDistance = stoppingDistance;
        agent.angularSpeed = 120f;

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        lastY = transform.position.y;
    }

    private void Start()
    {
        if (!agent.isOnNavMesh)
        {
            Debug.LogError("Ennemi spawn hors NavMesh ! Vérifie ton terrain et NavMesh.");
            enabled = false;
        }
    }

    private void Update()
    {
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
                isStunned = false;
            else
                return;
        }

        if (player != null && agent.isOnNavMesh)
            agent.SetDestination(player.position);

        lastY = transform.position.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            Debug.Log("Enemy touche le joueur !");
            ph.TakeDamage(contactDamage);
        }
    }

    public void Stun(float duration)
    {
        isStunned = true;
        stunTimer = duration;
        if (agent != null && agent.isOnNavMesh)
            agent.ResetPath();

        Debug.Log(gameObject.name + " stun appliqué!");
    }
}
