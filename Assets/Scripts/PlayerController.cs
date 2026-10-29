
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;

    private Rigidbody rb;
    private float rotationX = 0f;
    private Camera playerCamera;

    // Koristi se za privremeno blokiranje upravljanja
    public bool controlsEnabled = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!controlsEnabled)
            return;

        LookAround();
    }

    void FixedUpdate()
    {
        if (!controlsEnabled)
            return;

        MovePlayer();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        rb.MovePosition(
            rb.position +
            move * moveSpeed * Time.fixedDeltaTime
        );
    }

    void LookAround()
    {
        float mouseX =
            Input.GetAxis("Mouse X") *
            mouseSensitivity;

        float mouseY =
            Input.GetAxis("Mouse Y") *
            mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        rotationX -= mouseY;

        rotationX = Mathf.Clamp(
            rotationX,
            -70f,
            70f
        );

        if (playerCamera != null)
        {
            playerCamera.transform.localRotation =
                Quaternion.Euler(rotationX, 0f, 0f);
        }
    }
}
