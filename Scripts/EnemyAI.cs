using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float jumpForce = 5f;
    public float shockwaveSpeed = 5f;
    public float maxShockwaveRadius = 10f;
    public int damage = 10;
    public float shockwaveInterval = 5f;

    private bool isShockwaveActive = false;
    private float currentShockwaveRadius = 0f;
    private float shockwaveTimer = 0f;
    private bool isGrounded = true;
    private bool hasJumped = false;
    private Rigidbody rb;

    private ShockwaveVisual shockwaveVisual;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        shockwaveVisual = GetComponent<ShockwaveVisual>();
        if (shockwaveVisual == null)
        {
            Debug.LogWarning("ShockwaveVisual not found on Enemy.");
        }
    }

    void Update()
    {
        shockwaveTimer += Time.deltaTime;

        if (shockwaveTimer >= shockwaveInterval && isGrounded)
        {
            Jump();
            shockwaveTimer = 0f;
        }

        if (isShockwaveActive)
        {
            currentShockwaveRadius += shockwaveSpeed * Time.deltaTime;

            if (shockwaveVisual != null)
                shockwaveVisual.UpdateShockwave(currentShockwaveRadius);

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, currentShockwaveRadius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player"))
                {
                    PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(damage);
                        StopShockwave();
                        break;
                    }
                }
            }

            if (currentShockwaveRadius >= maxShockwaveRadius)
            {
                StopShockwave();
            }
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            hasJumped = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;

        if (hasJumped)
        {
            currentShockwaveRadius = 0f;
            isShockwaveActive = true;

            if (shockwaveVisual != null)
            {
                shockwaveVisual.SetVisible(true);
                shockwaveVisual.UpdateShockwave(currentShockwaveRadius);
            }

            hasJumped = false;
        }
    }

    private void StopShockwave()
    {
        isShockwaveActive = false;

        if (shockwaveVisual != null)
            shockwaveVisual.SetVisible(false);
    }
}
