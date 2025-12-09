using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Camera")]
    public Camera playerCamera;
    public float mouseSensitivity = 200f;

    [Header("Weapon (optional)")]
    public WeaponRaycast weapon;

    [Header("Special Ability")]
    public float abilityDuration = 5f;
    private bool abilityActive = false;
    private float abilityCooldown = 0f;

    [Header("Kill Ability")]
    public KeyCode killKey = KeyCode.R;
    public float killRadius = 20f;

    private CharacterController controller;
    private Vector3 velocity;
    private float cameraPitch = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
            Debug.LogError("Player doit avoir un CharacterController !");
        if (playerCamera == null)
            Debug.LogWarning("PlayerCamera n'est pas assignée !");
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovePlayer();
        CameraLook();
        Shoot();
        HandleSpecialAbility();

        if (Input.GetKeyDown(killKey))
        {
            KillEnemiesInRange();
        }
    }

    private void MovePlayer()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;
        controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void CameraLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -85f, 85f);
        if (playerCamera != null)
            playerCamera.transform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    private void Shoot()
    {
        if (weapon == null) return;
        if (Mouse.current.leftButton.wasPressedThisFrame)
            weapon.Shoot();
    }

    private void HandleSpecialAbility()
    {
        if (Input.GetKeyDown(KeyCode.E) && !abilityActive)
        {
            abilityActive = true;
            abilityCooldown = abilityDuration;
            SlowDownEnemies(true);
        }

        if (abilityActive)
        {
            abilityCooldown -= Time.deltaTime;
            if (abilityCooldown <= 0)
            {
                abilityActive = false;
                SlowDownEnemies(false);
            }
        }
    }

    private void SlowDownEnemies(bool slowDown)
    {
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
        foreach (EnemyAI enemy in enemies)
        {
            if (enemy != null)
            {
                if (slowDown) enemy.SetSpeed(0.5f);
                else enemy.ResetSpeed();
            }
        }
    }

    private void KillEnemiesInRange()
    {
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
        foreach (EnemyAI enemy in enemies)
        {
            if (enemy != null)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance <= killRadius)
                {
                    EnemyHealth health = enemy.GetComponent<EnemyHealth>();
                    if (health != null)
                        health.TakeDamage(health.maxHealth); // tue instantanément
                }
            }
        }
    }
}
