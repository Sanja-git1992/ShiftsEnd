
using UnityEngine;

public class ShiftRegisterInteraction : MonoBehaviour
{
    public GameObject documentPanel;

    private bool playerNear = false;
    private bool documentOpen = false;

    void Start()
    {
        if (documentPanel != null)
            documentPanel.SetActive(false);
    }

    void Update()
    {
        if (playerNear && !documentOpen &&
            Input.GetKeyDown(KeyCode.E))
        {
            OpenDocument();
        }

        if (documentOpen &&
            Input.GetKeyDown(KeyCode.Return))
        {
            CloseDocument();
        }
    }

    void OpenDocument()
    {
        if (documentPanel == null)
            return;

        documentOpen = true;
        documentPanel.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CloseDocument()
    {
        documentOpen = false;
        documentPanel.SetActive(false);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
