using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Sprint")]
    public float sprintMultiplier = 2f;
    public float doubleTapTime = 0.3f;

    [Header("Camera")]
    public Camera playerCamera;
    public float mouseSensitivity = 50f;

    [Header("Weapon (optional)")]
    public WeaponRaycast weapon;

    [Header("Menu")]
    public MenuFadeOut menuFade;

    private CharacterController controller;
    private Vector3 velocity;
    private float cameraPitch = 0f;
    private bool canMove = false;

    private bool isSprinting = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        UnlockCursor();

        if (menuFade != null)
        {
            menuFade.playButton.onClick.AddListener(OnMenuReady);
        }
    }

    void Update()
    {
        if (!canMove) return;

        MovePlayer();
        CameraLook();
        Shoot();
    }

    private void OnMenuReady()
    {
        canMove = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void MovePlayer()
    {
        float h = 0f;
        float v = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed) h = -1;
            if (Keyboard.current.dKey.isPressed) h = 1;
            if (Keyboard.current.wKey.isPressed) v = 1;
            if (Keyboard.current.sKey.isPressed) v = -1;

            isSprinting =
                Keyboard.current.wKey.isPressed &&
                (Keyboard.current.leftShiftKey.isPressed || Keyboard.current.rightShiftKey.isPressed);
        }

        float currentSpeed = speed;
        if (isSprinting)
            currentSpeed *= sprintMultiplier;

        Vector3 move = transform.right * h + transform.forward * v;
        controller.Move(move * currentSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (controller.isGrounded && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


    private void CameraLook()
    {
        if (Mouse.current == null) return;

        float mouseX = Mouse.current.delta.x.ReadValue() * mouseSensitivity * Time.deltaTime;
        float mouseY = Mouse.current.delta.y.ReadValue() * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -85f, 85f);

        if (playerCamera != null)
        {
            playerCamera.transform.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);
        }
    }

    private void Shoot()
    {
        if (weapon == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            weapon.Shoot();
        }
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

