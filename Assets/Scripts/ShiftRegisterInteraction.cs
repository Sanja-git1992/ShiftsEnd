
using UnityEngine;

public class ShiftRegisterInteraction : MonoBehaviour
{
    public bool HasBeenRead { get; private set; } = false;

    public GameObject documentPanel;
    public PlayerController playerController;

    private bool playerNear = false;
    private bool documentOpen = false;
    private float previousTimeScale = 1f;

    void Start()
    {
        if (documentPanel != null)
            documentPanel.SetActive(false);
    }

    void Update()
    {
        if (!documentOpen &&
            playerNear &&
            Input.GetKeyDown(KeyCode.E))
        {
            OpenDocument();
        }
        else if (documentOpen &&
                 Input.GetKeyDown(KeyCode.Return))
        {
            CloseDocument();
        }
    }

    void OpenDocument()
    {
        if (documentPanel == null || documentOpen)
            return;

        previousTimeScale = Time.timeScale;
        documentOpen = true;

        documentPanel.SetActive(true);

        if (playerController != null)
            playerController.controlsEnabled = false;

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Reception document opened.");
    }

    void CloseDocument()
    {
        if (!documentOpen)
            return;

        documentOpen = false;

        // Dokument se smatra procitanim nakon zatvaranja.
        HasBeenRead = true;

        if (documentPanel != null)
            documentPanel.SetActive(false);

        Time.timeScale = previousTimeScale;

        if (playerController != null)
            playerController.controlsEnabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Reception document read. Access granted.");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNear = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNear = false;
    }

    void OnDisable()
    {
        if (documentOpen)
            CloseDocument();
    }
}
