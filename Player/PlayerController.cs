using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera cam;

    [Header("Movement")]
    [SerializeField] private float camSensitivity = 150f;
    [SerializeField] private float moveSensitivity = 7f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Inputs")]
    [SerializeField] private InputActionReference zqsd;
    [SerializeField] private InputActionReference mouseMovement;
    [SerializeField] private InputActionReference fire;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 3f;

    [Header("GroundCheck")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private LayerMask groundCheckMask;

    private CharacterController controller;
    private float rotationX = 0f;
    private bool isGrounded = false;
    private Vector3 velocity = Vector3.zero;

    [Header("Weapon")]
    public WeaponRaycast weapon;


    // INITIALISATION -------------------------------------------------------------

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        if (weapon == null)
        {
            weapon = GetComponentInChildren<WeaponRaycast>();
            if (weapon == null)
                Debug.LogWarning("⚠️ No WeaponRaycast found on player.");
        }
    }

    private void Start()
    {
        // Enable Input System actions
        if (zqsd) zqsd.action.Enable();
        if (mouseMovement) mouseMovement.action.Enable();
        if (fire)
        {
            fire.action.performed += FirePressed;
            fire.action.Enable();
        }

        Cursor.lockState = CursorLockMode.Locked;
    }


    // SHOOT ----------------------------------------------------------------------

    private void FirePressed(InputAction.CallbackContext obj)
    {
        if (weapon != null)
            weapon.Shoot();
    }


    // UPDATE ---------------------------------------------------------------------

    private void Update()
    {
        GroundCheck();
        CameraRotation();
        Movement();
        Jump();
        Gravity();
    }


    // GROUND CHECK ---------------------------------------------------------------

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(
            groundCheckPoint.position,
            groundCheckRadius,
            groundCheckMask
        );

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f; // "stick to floor"
    }


    // CAMERA + MOUSE -------------------------------------------------------------

    private void CameraRotation()
    {
        Vector2 mouse = mouseMovement.action.ReadValue<Vector2>();

        float mouseX = mouse.x * camSensitivity * Time.deltaTime;
        float mouseY = mouse.y * camSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }


    // MOVEMENT -------------------------------------------------------------------

    private void Movement()
    {
        Vector2 input = zqsd.action.ReadValue<Vector2>();

        Vector3 move = transform.TransformDirection(new Vector3(input.x, 0, input.y));
        controller.Move(move * moveSensitivity * Time.deltaTime);
    }


    // JUMP -----------------------------------------------------------------------

    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }


    // GRAVITY --------------------------------------------------------------------

    private void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


    // DEBUG (facultatif) ---------------------------------------------------------

    private void OnDrawGizmosSelected()
    {
        if (groundCheckPoint == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }
}
