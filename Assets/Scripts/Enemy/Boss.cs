using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Boss : MonoBehaviour
{
    [Header("Activation")]
    public bool isActive = false;

    [Header("Stats")]
    public Transform player;
    public float contactDamage = 2f;

    [Header("NavMesh")]
    public float speed = 2.5f;
    public float stoppingDistance = 2f;

    [Header("Stun")]
    private bool isStunned = false;
    private float stunTimer = 0f;

    [Header("Shockwave")]
    public ShockwaveVisual shockwave;
    public float shockwaveMaxRadius = 6f;
    public float shockwaveSpeed = 4f;
    public float shockwaveCooldown = 5f;

    private float shockwaveTimer;
    private bool shockwaveActive = false;
    private float currentShockwaveRadius = 0f;

    [Header("Health UI")]
    public BossHealth bossHealth;
    public Slider healthSlider;
    public Vector3 healthOffset = new Vector3(0, 2f, 0);

    [Header("End Game UI")]
    public GameObject endGamePanel;

    private NavMeshAgent agent;
    private Rigidbody rb;
    private GhostTutorial tutorial;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        CapsuleCollider col = GetComponent<CapsuleCollider>();
        col.isTrigger = false;
        col.height = 2f;
        col.center = new Vector3(0, 1f, 0);

        agent.enabled = false;
        rb.isKinematic = true;
        gameObject.SetActive(false);

        if (healthSlider != null)
            healthSlider.gameObject.SetActive(false);
    }

    private void Start()
    {
        tutorial = FindObjectOfType<GhostTutorial>();
        shockwaveTimer = shockwaveCooldown;

        if (shockwave != null)
            shockwave.SetVisible(false);
    }

    private void Update()
    {
        if (!isActive) return;

        HandleTemporaryMessage();
        HandleStun();
        HandleMovement();
        HandleShockwave();
        UpdateHealthUI();
    }

    public void ActivateBoss()
    {
        isActive = true;
        gameObject.SetActive(true);

        rb.isKinematic = false;
        agent.enabled = true;

        if (healthSlider != null)
            healthSlider.gameObject.SetActive(true);

        Debug.Log("Boss activé !");
    }

    private void HandleTemporaryMessage()
    {
        if (tutorial != null && tutorial.temporaryMessageActive)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }
        else
        {
            agent.isStopped = false;
        }
    }

    private void HandleStun()
    {
        if (!isStunned) return;

        stunTimer -= Time.deltaTime;
        agent.isStopped = true;

        if (stunTimer <= 0f)
        {
            isStunned = false;
            agent.isStopped = false;
        }
    }

    private void HandleMovement()
    {
        if (isStunned || player == null || (tutorial != null && tutorial.temporaryMessageActive))
            return;

        agent.SetDestination(player.position);
    }

    private void HandleShockwave()
    {
        if (tutorial != null && tutorial.temporaryMessageActive)
            return;

        if (shockwaveActive)
        {
            currentShockwaveRadius += shockwaveSpeed * Time.deltaTime;
            shockwave.UpdateShockwave(currentShockwaveRadius);

            if (currentShockwaveRadius >= shockwaveMaxRadius)
            {
                shockwaveActive = false;
                shockwave.SetVisible(false);
                shockwaveTimer = shockwaveCooldown;
            }
            return;
        }

        shockwaveTimer -= Time.deltaTime;
        if (shockwaveTimer <= 0f)
            StartShockwave();
    }

    private void StartShockwave()
    {
        shockwaveActive = true;
        currentShockwaveRadius = 0f;

        if (shockwave != null)
            shockwave.SetVisible(true);
    }

    public void Stun(float duration)
    {
        isStunned = true;
        stunTimer = duration;
        agent.ResetPath();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
        if (ph != null && isActive && (tutorial == null || !tutorial.temporaryMessageActive))
        {
            ph.TakeDamage(contactDamage);
            Debug.Log("Boss touche le joueur !");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        if (tutorial != null && tutorial.temporaryMessageActive) return;

        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(contactDamage);
            Debug.Log("Boss inflige des dégâts au joueur");
        }

        WeaponRaycast pw = other.GetComponent<WeaponRaycast>();
        if (pw != null)
        {
            bossHealth.TakeDamage(pw.damage);
            Stun(0.5f);
            Debug.Log("Boss reçoit " + pw.damage + " dégâts !");
        }


    }

    private void UpdateHealthUI()
    {
        if (bossHealth != null && healthSlider != null && Camera.main != null)
        {
            healthSlider.value = bossHealth.GetHealthPercent();
            healthSlider.transform.position = transform.position + healthOffset;
            healthSlider.transform.LookAt(healthSlider.transform.position + Camera.main.transform.forward);
        }
    }
}
