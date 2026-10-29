
using UnityEngine;

public class DocumentReader : MonoBehaviour
{
    public GameObject documentPanel;
    public PlayerController playerController;

    private bool documentOpen = false;
    public bool IsDocumentOpen => documentOpen;
    private float previousTimeScale = 1f;

    void Start()
    {
        if (documentPanel != null)
            documentPanel.SetActive(false);
    }

    void Update()
    {
        if (documentOpen && Input.GetKeyDown(KeyCode.Return))
        {
            CloseDocument();
        }
    }

    public void OpenDocument()
    {
        if (documentPanel == null || documentOpen)
            return;

        previousTimeScale = Time.timeScale;
        documentOpen = true;

        documentPanel.SetActive(true);

        // Zaustavi vrijeme i upravljanje igracem
        Time.timeScale = 0f;

        if (playerController != null)
            playerController.controlsEnabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseDocument()
    {
        if (!documentOpen)
            return;

        documentOpen = false;
        documentPanel.SetActive(false);

        // Vrati prethodno stanje igre
        Time.timeScale = previousTimeScale;

        if (playerController != null)
            playerController.controlsEnabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDisable()
    {
        // Sigurnosna zastita ako se komponenta iskljuci
        if (documentOpen)
            CloseDocument();
    }
}
